<template>
  <div class="full-height">
    <!-- Cytoscape window -->
    <div
      class="full-height cytoscape-window"
      ref="cytoscape"
      key="cytoscape"
    ></div>

    <!-- Context menu -->
    <q-popup-proxy v-if="!readOnly" context-menu>
      <q-list style="min-width: 100px">
        <q-item
          v-if="contextMenuVisibility.addNewLesson"
          @click="onAddLessonClick"
          clickable
          v-close-popup
        >
          <q-item-section avatar>
            <q-icon name="fas fa-plus" />
          </q-item-section>
          <q-item-section>Add new lesson</q-item-section>
        </q-item>

        <q-item
          v-if="contextMenuVisibility.editLesson"
          @click="onEditLessonClick"
          clickable
          v-close-popup
        >
          <q-item-section avatar>
            <q-icon name="fas fa-edit" />
          </q-item-section>
          <q-item-section>Edit lesson</q-item-section>
        </q-item>

        <q-item
          v-if="contextMenuVisibility.link"
          @click="onLinkClick"
          clickable
          v-close-popup
        >
          <q-item-section avatar>
            <q-icon name="fas fa-link" />
          </q-item-section>
          <q-item-section>Link</q-item-section>
        </q-item>

        <q-item
          v-if="contextMenuVisibility.setAsStarting"
          @click="onSetAsStartingClick"
          clickable
          v-close-popup
        >
          <q-item-section avatar>
            <q-icon name="fas fa-home" />
          </q-item-section>
          <q-item-section>Set as starting lesson</q-item-section>
        </q-item>

        <q-separator />

        <q-item
          v-if="contextMenuVisibility.remove"
          @click="onRemoveClick"
          clickable
          v-close-popup
        >
          <q-item-section avatar>
            <q-icon name="fas fa-times" />
          </q-item-section>
          <q-item-section>Remove</q-item-section>
        </q-item>
      </q-list>
    </q-popup-proxy>

    <transition
      enter-active-class="animated fadeInDown"
      leave-active-class="animated fadeOutUp"
    >
      <div v-if="isLessonDialogVisible && readOnly" class="lesson-popup">
        <q-card>
          <q-card-section class="row">
            <div class="row full-width q-mb-sm">
              <span class="text-lessons text-h6">
                {{ selectedLesson.name }}
              </span>
            </div>

            <div class="row full-width">
              <span class="text-subtitle2">
                {{ selectedLesson.description }}
              </span>
            </div>

            <q-separator class="full-width q-my-md" />

            <div class="row full-width">
              <q-btn
                v-if="selectedLesson.status === lessonStatusEnum.awaiting"
                @click="onBeginLessonClick(selectedLesson.id)"
                class="full-width"
                color="primary"
                icon="fas fa-book"
                label="Begin lesson"
              />
              <q-btn
                v-if="selectedLesson.status === lessonStatusEnum.locked"
                class="full-width"
                disable
                color="blue-grey-9"
                icon="fas fa-lock"
                label="Lesson locked"
              />
            </div>
          </q-card-section>
        </q-card>
      </div>
    </transition>
  </div>
</template>

<script>
import LocalStorageService from "../../../services/local-storage/LocalStorageService";

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
  selectedElementValue: null,
  name: "CourseTreeEditor",
  props: ["cytoscapeOptions", "readOnly"],
  data() {
    return {
      isNodeSelected: !!this.selectedElement,
      isLessonDialogVisible: false,
      contextMenuVisibility: {
        link: false,
        remove: false,
        setAsStarting: false,
        addNewLesson: false,
        editLesson: false
      },
      lessonsData: [],
      selectedLesson: {},
      lessonStatusEnum: lessonStatusEnum
    };
  },
  mounted() {
    this.initCytoscape();

    // Selected lesson getter/setter
    Object.defineProperty(this, "selectedElement", {
      set: val => {
        if (val && Object.entries(val).length !== 0) {
          let lessonId = val.data("id");
          this.selectedLesson = this.lessonsData.find(
            x => x.id.toString() === lessonId
          );
        } else {
          this.selectedLesson = {};
        }

        this.selectedElementValue = val;
      },
      get: () => {
        return this.selectedElementValue;
      }
    });
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
          data: this.generateLessonDataObject(lessonData, isFirstLesson),
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

    updateLesson(lessonData, lessonElement) {
      lessonElement.data(this.generateLessonDataObject(lessonData, lessonElement.data("isFirst")));
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

      // Deselect currently selected node
      if (this.selectedElement) {
        this.selectedElement.unselect();
      }

      // Select new node and recalculate options visibility
      if (target && target.data() && Object.keys(target.data()).length) {
        target.select();
      }

      this.setContextMenuVisibility();
    },

    /**
     * Handles element selection event
     * @param event: event data
     */
    onElementSelect(event) {
      this.selectedElement = event.target;

      if (this.selectedElement.isNode()) {
        this.isLessonDialogVisible = true;
      }
    },

    /**
     * Handles element deselection event
     */
    onElementUnselect() {
      this.selectedElement = null;
      this.isLessonDialogVisible = false;
    },

    /**
     * Handles tree data change event
     */
    onDataChange() {
      this.$emit("change");
    },

    onLinkClick() {
      if (this.selectedElement.isNode()) {
        this.edgehandles.enable();
        this.edgehandles.enableDrawMode();
        this.edgehandles.start(this.selectedElement);
      }
    },

    /**
     * Handles remove element button click
     */
    onRemoveClick() {
      if (this.selectedElement) {
        this.removeElement(this.selectedElement);
        this.selectedElement = null;
      }
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
    },

    onBeginLessonClick(lessonId) {
      let lockedLessons = this.getConnectedLockedLessons(this.selectedElement);
      LocalStorageService.setLockedLessonsIds(lockedLessons);

      this.$router.push(
        `/main/courses/view/${this.$route.params.id}/lessons/${lessonId}`
      );
    },

    onAddLessonClick() {
      this.$emit("addLesson");
    },

    onEditLessonClick() {
      if (this.selectedElement) {
        let lessonId = this.selectedElement.data("id");
        this.$emit("editLesson", lessonId);
      }
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
        remove: !!this.selectedElement,
        setAsStarting:
          this.selectedElement &&
          !(
            this.selectedElement.data("isFirst") ||
            this.selectedElement.isEdge()
          ),
        addNewLesson: !this.selectedElement,
        editLesson: this.selectedElement && this.selectedElement.isNode()
      };
    },

    setData(treeData, lessonsData) {
      if (treeData && treeData !== "") {
        let jsonValue = JSON.parse(treeData);
        this.cytoscape.json(jsonValue);
        this.cytoscape.style(CytoscapeStyles);

        this.lessonsData = lessonsData;
        this.updateTreeData();
      }
    },

    getData() {
      if (this.selectedElement) {
        this.selectedElement.unselect();
        this.selectedElement = null;
      }

      return JSON.stringify(this.cytoscape.json());
    },

    updateTreeData() {
      let allNodes = this.cytoscape.nodes(LessonNodeSelector);

      allNodes.forEach(node => {
        console.log(this.lessonsData);
        let lessonData = this.lessonsData.find(
          x => x.id.toString() === node.data("id")
        );

        if (lessonData) {
          this.updateLesson(lessonData, node);

          if (this.readOnly) {
            node.data("status", lessonData.status);
            this.setLessonClassByStatus(node, lessonData.status);
          }
        }
      });

      if (this.readOnly) {
        this.setEdgesClasses();
      }
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
    },

    getConnectedLockedLessons(element) {
      let connectedOutgoers = element.outgoers("node.locked-lesson");
      let connectedIncomers = element.incomers("node.locked-lesson");
      let lockedLessonsIds = [];

      connectedOutgoers.forEach(outgoer => {
        lockedLessonsIds.push(parseInt(outgoer.data("id")));
      });

      connectedIncomers.forEach(incomer => {
        lockedLessonsIds.push(parseInt(incomer.data("id")));
      });

      return lockedLessonsIds;
    },

    generateLessonDataObject (lessonData, isFirstLesson) {
      return {
        id: lessonData.id,
        name: lessonData.name,
        isFirst: isFirstLesson,
        status: lessonData.status
      }
    }
  }
};
</script>

<style lang="stylus" scoped>
.cytoscape-window {
  background-color: rgba(black, 0.1);
  border-radius: 5px;
}

.lesson-popup {
  --popup-width: 400px;
  width: var(--popup-width);
  position: absolute;
  top: 0;
  left: calc(50% - (var(--popup-width) / 2));
  z-index: 100;
}
</style>
