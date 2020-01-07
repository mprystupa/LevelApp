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
      <div v-if="isContextMenuVisible">
        <q-btn-group push>
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

Cytoscape.use(EdgeHandles);
Cytoscape.use(GridGuide);
Cytoscape.use(Popper);

export default {
  cytoscape: {},
  edgehandles: {},
  selectedNode: null,
  name: "CourseTreeEditor",
  props: ["value", "cytoscapeOptions", "readOnly"],
  data() {
    return {
      isNodeSelected: !!this.selectedNode,
      isContextMenuVisible: false,
      contextMenuVisibility: {
        remove: true,
        setAsStarting: false
      },
      treeValue: {},
      lessonNodeSelector: 'node[name]'
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
        style: [
          // the stylesheet for the graph
          {
            selector: this.lessonNodeSelector,
            style: {
              "background-color": "#666",
              height: "90",
              width: "90",
              shape: "polygon",
              "shape-polygon-points":
                "0, 1, 0.866, 0.5, 0.866, -0.5, 0, -1, -0.866, -0.5, -0.866, 0.5",
              "border-width": "4",
              "border-color": "#fff",
              label: "data(name)"
            }
          },
          {
            selector: "node:selected",
            style: {
              "border-color": "#0fb"
            }
          },
          {
            selector: "node:unselected.first-lesson",
            style: {
              "border-color": "#f2ff23"
            }
          },
          {
            selector: ".label-background",
            style: {
              "font-family": "'Istok Web', sans-serif",
              "text-background-padding": 4,
              "text-background-opacity": 1,
              color: "#000",
              "text-background-color": "#fff",
              "text-background-shape": "roundrectangle"
            }
          },
          {
            selector: ".label-bottom",
            style: {
              "text-valign": "bottom",
              "text-halign": "center"
            }
          },
          {
            selector: "edge",
            style: {
              width: 4,
              "line-color": "#fff",
              "line-style": "dashed",
              "target-arrow-color": "#ccc",
              "target-arrow-shape": "triangle"
            }
          },
          {
            selector: ".eh-handle",
            style: {
              "background-color": "red",
              width: 12,
              height: 12,
              shape: "ellipse",
              "overlay-opacity": 0,
              "border-width": 12, // makes the handle easier to hit
              "border-opacity": 0,
              label: ""
            }
          },
          {
            selector: ".eh-hover",
            style: {
              "background-color": "red"
            }
          },
          {
            selector: ".eh-source",
            style: {
              "border-width": 2,
              "border-color": "red"
            }
          },
          {
            selector: ".eh-target",
            style: {
              "border-width": 2,
              "border-color": "red"
            }
          },
          {
            selector: ".eh-preview, .eh-ghost-edge",
            style: {
              "background-color": "red",
              "line-color": "red",
              "target-arrow-color": "red",
              "source-arrow-color": "red"
            }
          },
          {
            selector: ".eh-ghost-edge.eh-preview-active",
            style: {
              opacity: 0
            }
          }
        ],
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

      if (this.value) {
        cy.json(this.value);
        this.treeValue = this.value;
      }

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
        }
      });
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
            isFirst: isFirstLesson
          },
          classes: `label-background label-bottom ${isFirstLesson ? 'first-lesson' : ''}`,
          renderedPosition: {
            x: position.x,
            y: position.y
          }
        });
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
      let allNodes = this.cytoscape.nodes(this.lessonNodeSelector).toArray().filter(x => x.isNode());
      let visitedNodeIds = [];
      let nodesToVisit = [];

      if (allNodes.length > 0) {
        nodesToVisit.push(allNodes[0]);
      }

      while (nodesToVisit.length > 0) {
        let currentNode = nodesToVisit.pop();

        if (visitedNodeIds.includes(currentNode.data('id'))) {
          return true;
        }

        visitedNodeIds.push(currentNode.data('id'));

        currentNode.outgoers(this.lessonNodeSelector).forEach(node => {
          if (!visitedNodeIds.includes(node.data('id'))) {
            nodesToVisit.push(node);
          }
        });

        currentNode.incomers(this.lessonNodeSelector).forEach(node => {
          if (!visitedNodeIds.includes(node.data('id'))) {
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
      if (this.selectedNode) {
        this.selectedNode.unselect();
      }

      // Select new node and recalculate options visibility
      if (target && target.data() && Object.keys(target.data()).length) {
        target.select();
        this.setContextMenuVisibility();
        this.isContextMenuVisible = true;
      }
    },

    /**
     * Handles element selection event
     * @param event: event data
     */
    onElementSelect(event) {
      if (this.selectedNode) {
        this.isContextMenuVisible = false;
      }

      this.selectedNode = event.target;
    },

    /**
     * Handles element deselection event
     */
    onElementUnselect() {
      this.selectedNode = null;
      this.isContextMenuVisible = false;
    },

    /**
     * Handles tree data change event
     */
    onDataChange() {
      this.$emit("input", this.cytoscape.json());
    },

    /**
     * Handles remove element button click
     */
    onRemoveClick() {
      if (this.selectedNode) {
        this.removeElement(this.selectedNode);
        this.selectedNode = null;
      }

      this.isContextMenuVisible = false;
    },

    onSetAsStartingClick() {
      if (this.selectedNode) {
        let currentStarting = this.cytoscape.elements("node[isFirst = true]");

        currentStarting.forEach(element => {
          this.setElementAsIsFirst(element, false);
        })

        this.setElementAsIsFirst(this.selectedNode, true);
        this.selectedNode.unselect();
      }

      this.isContextMenuVisible = false;
    },

    setElementAsIsFirst(element, value) {
      element.data("isFirst", value);
      value ? element.addClass("first-lesson") : element.removeClass("first-lesson");
    },

    setContextMenuVisibility() {
      this.contextMenuVisibility = {
        remove: true,
        setAsStarting: !(this.selectedNode && this.selectedNode.data("isFirst"))
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
</style>
