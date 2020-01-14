<template>
  <div ref="editor"></div>
</template>

<script>
import Quill from "quill";

export default {
  name: "EditableContent",
  props: ["value"],
  data() {
    return {
      editor: null
    };
  },
  mounted() {
    this.editor = new Quill(this.$refs.editor, {
      placeholder: "Enter content of your lesson here...",
      theme: "snow"
    });

    this.editor.on("text-change", () => {
      this.update();
    });
  },
  methods: {
    update() {
      let stringContent = JSON.stringify(this.editor.getContents());
      let htmlContent = this.editor.root.innerHTML;
      this.$emit("input", {
        stringContent: stringContent,
        htmlContent: htmlContent
      });
    },
    reloadData(data) {
      if (data && data !== "") {
        this.editor.setContents(JSON.parse(data));
      }
    }
  },
  watch: {
    value: {
      handler(newVal) {
        if (this.editor && newVal !== "") {
          this.value = newVal;
          this.editor.setContents(JSON.parse(newVal));
        }
      }
    }
  }
};
</script>

<style scoped></style>
