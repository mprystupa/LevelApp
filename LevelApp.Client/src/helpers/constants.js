export const Constants = {
  coursesCardHeight: () => 500
}

export const LessonNodeSelector = "node[name]";

export const CytoscapeStyles = [
  // the stylesheet for the graph
  {
    selector: LessonNodeSelector,
    style: {
      "background-color": "#666",
      "background-image": function(ele) { return ele.data('iconUrl'); },
      "background-width": "65%",
      "background-height": "65%",
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
    selector: "node.awaiting-lesson",
    style: {
      "background-color": "#aaa"
    }
  },
  {
    selector: "node.locked-lesson",
    style: {
      "border-color": "#666"
    }
  },
  {
    selector: "node.completed-lesson",
    style: {
      "background-color": "#2e9e33"
    }
  },
  {
    selector: "node:unselected.first-lesson",
    style: {
      "border-color": "#f2ff23"
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
    selector: "edge.available-edge",
    style: {
      width: 4,
      "line-color": "#fff",
      "line-style": "dashed",
      "target-arrow-color": "#ccc",
      "target-arrow-shape": "triangle"
    }
  },
  {
    selector: "edge.completed-edge",
    style: {
      width: 4,
      "line-color": "#4bcf2d",
      "line-style": "solid",
      "target-arrow-color": "#ccc",
      "target-arrow-shape": "triangle"
    }
  },
  {
    selector: "edge.locked-edge",
    style: {
      width: 4,
      "line-color": "#666",
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
];
