<template>
  <div>
    <div id="toolbar-container">
      <span class="ql-formats">
        <select class="ql-font"></select>
        <select class="ql-size"></select>
      </span>
      <span class="ql-formats">
        <button class="ql-bold"></button>
        <button class="ql-italic"></button>
        <button class="ql-underline"></button>
        <button class="ql-strike"></button>
      </span>
      <span class="ql-formats">
        <select class="ql-color"></select>
        <select class="ql-background"></select>
      </span>
      <span class="ql-formats">
        <button class="ql-script" value="sub"></button>
        <button class="ql-script" value="super"></button>
      </span>
      <span class="ql-formats">
        <button class="ql-header" value="1"></button>
        <button class="ql-header" value="2"></button>
        <button class="ql-blockquote"></button>
        <button class="ql-code-block"></button>
      </span>
      <span class="ql-formats">
        <button class="ql-list" value="ordered"></button>
        <button class="ql-list" value="bullet"></button>
        <button class="ql-indent" value="-1"></button>
        <button class="ql-indent" value="+1"></button>
      </span>
      <span class="ql-formats">
        <button class="ql-direction" value="rtl"></button>
        <select class="ql-align"></select>
      </span>
      <span class="ql-formats">
        <button class="ql-link"></button>
        <button class="ql-image"></button>
        <button class="ql-video"></button>
        <button class="ql-formula"></button>
      </span>
      <span class="ql-formats">
        <button class="ql-clean"></button>
      </span>
    </div>
    <div ref="editor"></div>
  </div>
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
      modules: { toolbar: "#toolbar-container" },
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
        if (this.editor) {
          this.value = newVal;
          this.editor.setContents(JSON.parse(newVal));
        }
      }
    }
  }
};
</script>

<style scoped></style>
