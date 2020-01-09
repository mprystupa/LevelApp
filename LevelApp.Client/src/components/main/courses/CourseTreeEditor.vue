<template>
  <div class="full-height">
    <!-- Cytoscape window -->
    <div
      class="full-height cytoscape-window"
      ref="cytoscape"
      key="cytoscape"
    ></div>

    <!-- Context menu -->
    <q-popup-proxy context-menu>
      <div v-if="isContextMenuVisible && !readOnly">
        <q-btn-group push>
          <q-btn
            push
            v-if="contextMenuVisibility.link"
            label="Link"
            icon="fas fa-link"
            @click="onLinkClick"
          />
          <q-btn
            push
            v-if="contextMenuVisibility.remove"
            label="Remove"
            icon="fas fa-times"
            @click="onRemoveClick"
          />
          <q-btn
            push
            label="Set as starting lesson"
            v-if="contextMenuVisibility.setAsStarting"
            icon="fas fa-home"
            @click="onSetAsStartingClick"
          />
        </q-btn-group>
      </div>
    </q-popup-proxy>
  </div>
</template>

<script>
import Cytoscape from "cytoscape";
import GridGuide from "cytoscape-grid-guide";
import EdgeHandles from "cytoscape-edgehandles";
import Popper from "cytoscape-popper";
import {
  CytoscapeStyles,
  LessonNodeSelector
} from "../../../helpers/constants";

Cytoscape.use(EdgeHandles);
Cytoscape.use(GridGuide);
Cytoscape.use(Popper);

const lessonStatusEnum = Object.freeze({
  created: 1,
  notStarted: 2,
  locked: 3,
  awaiting: 4,
  completed: 5
});

export default {
  cytoscape: {},
  edgehandles: {},
  selectedElement: null,
  name: "CourseTreeEditor",
  props: ["cytoscapeOptions", "readOnly"],
  data() {
    return {
      isNodeSelected: !!this.selectedElement,
      isContextMenuVisible: false,
      contextMenuVisibility: {
        link: true,
        remove: true,
        setAsStarting: false
      },
      lessonsData: []
    };
  },
  mounted() {
    this.initCytoscape();
  },
  methods: {
    /**
     * Initializes Cytoscape library with options
     */
    initCytoscape() {
      let options = {
        container: this.$refs.cytoscape,
        style: CytoscapeStyles,
        autoungrabify: true
      };

      if (this.cytoscapeOptions) {
        options = this.cytoscapeOptions;
      }

      let cy = Cytoscape(options);

      cy.gridGuide({
        snapToGridDuringDrag: true,
        drawGrid: true,
        panGrid: true,
        distributionGuidelines: true,
        gridSpacing: 40,
        gridColor: "#fff",
        guidelinesStyle: {
          strokeStyle: "black",
          horizontalDistColor: "#ff0000",
          verticalDistColor: "green",
          initPosAlignmentColor: "#0000ff"
        }
      });

      if (!this.readOnly) {
        this.initEditMode(cy);
      }

      cy.minZoom(0.5);
      cy.maxZoom(2);

      this.registerEventHandlers(cy);

      this.cytoscape = cy;
    },

    /**
     * Initializes Cytoscape tree edit mode
     * @param cy: Cytoscape Core object
     */
    initEditMode(cy) {
      cy.autoungrabify(false);
      this.edgehandles = cy.edgehandles({
        // Completed edge handler
        complete: (sourceNode, targetNode, addedEles) => {
          try {
            this.validateEdge(sourceNode, targetNode, addedEles);
          } catch (error) {
            this.cytoscape.remove(addedEles);
            console.error(error);
          }

          this.edgehandles.disableDrawMode();
          this.edgehandles.disable();
        },
        cancel: (sourceNode, targetNode, addedEles) => {
          this.edgehandles.disableDrawMode();
          this.edgehandles.disable();
        }
      });

      this.edgehandles.disableDrawMode();
      this.edgehandles.disable();
    },

    /**
     * Registers all handlers for events appearing on Cytoscape editor
     * @param cy: Cytoscape Core object
     */
    registerEventHandlers(cy) {
      this.registerDataChangeEvents(["add", "move", "remove", "free"], cy);
      this.registerElementSelectionEvents(cy);
    },

    /**
     * Registers all event handlers that are executed by any data change event
     * @param eventList: list of events causing data change (passed as strings array)
     * @param cy: Cytoscape Core object
     */
    registerDataChangeEvents(eventList, cy) {
      eventList.forEach(event => {
        cy.on(event, () => {
          this.onDataChange();
        });
      });
    },

    /**
     * Registers all event handlers that are executed by user input
     * @param cy: Cytoscape Core object
     */
    registerElementSelectionEvents(cy) {
      // Right click context menu implementation
      cy.on("cxttap", event => {
        this.onContextTap(event);
      });

      cy.on("select", event => {
        this.onElementSelect(event);
      });

      cy.on("unselect", event => {
        this.onElementUnselect();
      });
    },

    /**
     * Adds new lesson node to existing tree; throws error when executed in read-only mode
     * @param lessonData: data of added lesson
     * @param position: position of added lesson (in window coordinates)
     */
    addLesson(lessonData, position) {
      if (!this.readOnly) {
        let isFirstLesson = this.cytoscape.nodes().empty();

        this.cytoscape.add({
          data: {
            id: lessonData.id,
            name: lessonData.name,
            isFirst: isFirstLesson,
            status: lessonData.status
          },
          classes: `label-background label-bottom ${
            isFirstLesson ? "first-lesson" : ""
          }`,
          renderedPosition: {
            x: position.x,
            y: position.y
          }
        });

        if (isFirstLesson) {
          this.$emit("setStartingLesson", lessonData.id.toString());
        }
      } else {
        throw new Error("Cannot add lessons in read-only mode!");
      }
    },

    /**
     * Removes tree element; if lesson node is deleted, proper event is emitted as well
     * @param element: element to remove from tree
     */
    removeElement(element) {
      this.cytoscape.remove(element);

      // Emit lesson deleted event
      if (element.isNode()) {
        this.$emit("removeLesson", element.data("id"));
      }
    },

    /**
     * Validates if edge is not breaking any tree rules
     */
    validateEdge(sourceNode, targetNode, addedEdge) {
      // No parallel edges
      if (this.detectParallelEdge(targetNode)) {
        throw new Error("Parallel edges are not allowed");
      }

      // No loops
      if (this.detectLoop()) {
        throw new Error("Loops are not allowed");
      }
    },

    /**
     * Detects parallel edge on element
     */
    detectParallelEdge(element) {
      let elementEdges = element.connectedEdges();
      return elementEdges.toArray().find(x => x.parallelEdges().length > 1);
    },

    /**
     * Detects loop in whole tree
     */
    detectLoop() {
      let allNodes = this.cytoscape.nodes(LessonNodeSelector).toArray();

      let visitedNodeIds = [];
      let nodesToVisit = [];

      if (allNodes.length > 0) {
        nodesToVisit.push(allNodes[0]);
      }

      while (nodesToVisit.length > 0) {
        let currentNode = nodesToVisit.pop();

        if (visitedNodeIds.includes(currentNode.data("id"))) {
          return true;
        }

        visitedNodeIds.push(currentNode.data("id"));

        currentNode.outgoers(LessonNodeSelector).forEach(node => {
          if (!visitedNodeIds.includes(node.data("id"))) {
            nodesToVisit.push(node);
          }
        });

        currentNode.incomers(LessonNodeSelector).forEach(node => {
          if (!visitedNodeIds.includes(node.data("id"))) {
            nodesToVisit.push(node);
          }
        });
      }

      return false;
    },

    /**
     * Handles context tap (right click) event
     * @param event: event data
     */
    onContextTap(event) {
      let target = event.target;
      this.isContextMenuVisible = false;

      // Deselect currently selected node
      if (this.selectedElement) {
        this.selectedElement.unselect();
      }

      // Select new node and recalculate options visibility
      if (target && target.data() && Object.keys(target.data()).length) {
        target.select();
        this.setContextMenuVisibility();
        this.isContextMenuVisible = true;

        console.log(target);
      }
    },

    /**
     * Handles element selection event
     * @param event: event data
     */
    onElementSelect(event) {
      if (this.selectedElement) {
        this.isContextMenuVisible = false;
      }

      this.selectedElement = event.target;
    },

    /**
     * Handles element deselection event
     */
    onElementUnselect() {
      this.selectedElement = null;
      this.isContextMenuVisible = false;
    },

    /**
     * Handles tree data change event
     */
    onDataChange() {
      this.$emit("input", JSON.stringify(this.cytoscape.json()));
    },

    onLinkClick() {
      if (this.selectedElement.isNode()) {
        this.edgehandles.enable();
        this.edgehandles.enableDrawMode();
        this.edgehandles.start(this.selectedElement);
      }

      this.isContextMenuVisible = false;
    },

    /**
     * Handles remove element button click
     */
    onRemoveClick() {
      if (this.selectedElement) {
        this.removeElement(this.selectedElement);
        this.selectedElement = null;
      }

      this.isContextMenuVisible = false;
    },

    onSetAsStartingClick() {
      if (this.selectedElement) {
        let currentStarting = this.cytoscape.elements("node[isFirst = true]");

        currentStarting.forEach(element => {
          this.setElementAsIsFirst(element, false);
        });

        this.setElementAsIsFirst(this.selectedElement, true);

        if (currentStarting.data("id") !== this.selectedElement.data("id")) {
          this.$emit("setStartingLesson", this.selectedElement.data("id"));
        }

        this.selectedElement.unselect();
      }

      this.isContextMenuVisible = false;
    },

    setElementAsIsFirst(element, value) {
      element.data("isFirst", value);
      value
        ? element.addClass("first-lesson")
        : element.removeClass("first-lesson");
    },

    setContextMenuVisibility() {
      this.contextMenuVisibility = {
        link: this.selectedElement && this.selectedElement.isNode(),
        remove: true,
        setAsStarting: !(
          this.selectedElement &&
          (this.selectedElement.data("isFirst") ||
            this.selectedElement.isEdge())
        )
      };
    },

    setData(treeData, lessonsData = []) {
      if (treeData && treeData !== "") {
        let jsonValue = JSON.parse(treeData);
        this.cytoscape.json(jsonValue);

        this.cytoscape.style(CytoscapeStyles);

        if (this.readOnly) {
          this.lessonsData = lessonsData;
          this.setLessonsStatus();
          this.setEdgesClasses();
        }
      }
    },

    getData() {
      if (this.selectedElement) {
        this.selectedElement.unselect();
        this.selectedElement = null;
      }

      return JSON.stringify(this.cytoscape.json());
    },

    setLessonsStatus() {
      let allNodes = this.cytoscape.nodes(LessonNodeSelector);

      allNodes.forEach(node => {
        let lessonData = this.lessonsData.find(
          x => x.id.toString() === node.data("id")
        );

        if (lessonData && this.readOnly) {
          node.data("status", lessonData.status);
          this.setLessonClassByStatus(node, lessonData.status);
        }
      });
    },

    setLessonClassByStatus(node, status) {
      switch (status) {
        case lessonStatusEnum.awaiting:
          node.addClass("awaiting-lesson");
          break;

        case lessonStatusEnum.completed:
          node.addClass("completed-lesson");
          break;

        case lessonStatusEnum.locked:
          node.addClass("locked-lesson");
          break;
      }
    },

    setEdgesClasses() {
      let allEdges = this.cytoscape.edges();

      allEdges.forEach(edge => {
        let sourceStatus = edge.source().data("status");
        let targetStatus = edge.target().data("status");

        if (
          sourceStatus === lessonStatusEnum.locked ||
          targetStatus === lessonStatusEnum.locked
        ) {
          edge.addClass("locked-edge");
        } else if (
          sourceStatus === lessonStatusEnum.completed &&
          targetStatus === lessonStatusEnum.completed
        ) {
          edge.addClass("completed-edge");
        } else {
          edge.addClass("available-edge");
        }
      });
    }
  }
};
</script>

<style lang="stylus" scoped>
.cytoscape-window {
  background-color: rgba(black, 0.1);
  border-radius: 5px;
}
</style>
