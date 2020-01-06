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
          <q-btn push label="Remove" icon="fas fa-times" @click="onRemoveClick" />
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
  selectedNode: null,
  name: "CourseTreeEditor",
  props: ["value", "cytoscapeOptions", "readOnly"],
  data() {
    return {
      isNodeSelected: !!this.selectedNode,
      isContextMenuVisible: false,
      treeValue: {}
    };
  },
  mounted() {
    let options = {
      container: this.$refs.cytoscape,
      style: [
        // the stylesheet for the graph
        {
          selector: "node",
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

    this.registerDataChangeEvents(["add", "move", "remove", "free"], cy);
    this.registerElementSelectionEvents(cy);

    this.cytoscape = cy;
  },
  methods: {
    initEditMode(cy) {
      cy.autoungrabify(false);
      cy.edgehandles();
    },
    registerDataChangeEvents(eventList, cy) {
      eventList.forEach(event => {
        cy.on(event, () => {
          this.onDataChange();
        });
      });
    },
    registerElementSelectionEvents(cy) {
      // Right click context menu implementation
      cy.on("cxttap", event => {
        let target = event.target;

        // Deselect currently selected node
        if (this.selectedNode) {
          this.selectedNode.unselect();
        }

        // Select new node
        if (target && target.data() && Object.keys(target.data()).length) {
          target.select();
          this.isContextMenuVisible = true;
        }
      });

      cy.on("select", event => {
        if (this.selectedNode) {
          this.isContextMenuVisible = false;
        }

        this.selectedNode = event.target;
      });

      cy.on("unselect", event => {
        this.selectedNode = null;
        this.isContextMenuVisible = false;
      });
    },
    addLesson(lessonData, position) {
      if (!this.readOnly) {
        this.cytoscape.add({
          data: {
            id: lessonData.id,
            name: lessonData.name
          },
          classes: "label-background label-bottom",
          renderedPosition: {
            x: position.x,
            y: position.y
          }
        });
      } else {
        throw new Error("Cannot add lessons in read-only mode!");
      }
    },
    removeElement(element) {
      this.cytoscape.remove(element);

      // Emit lesson deleted event
      if (element.isNode()) {
        console.log(element);
        this.$emit('removeLesson', element.data("id"));
      }
    },
    onDataChange() {
      this.$emit("input", this.cytoscape.json());
    },
    onRemoveClick() {
      if (this.selectedNode) {
        this.removeElement(this.selectedNode);
        this.isContextMenuVisible = false;
        this.selectedNode = null;
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
