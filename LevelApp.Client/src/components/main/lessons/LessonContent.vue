<template>
  <div class="lesson-wrapper">
    <!-- Header -->
    <div class="row lesson-header q-mb-md">
      <div class="full-width text-h3 text-lessons q-mb-sm">
        {{ lessonData.name }}
      </div>
      <div class="full-width text-subtitle1 text-lessons q-mb-xs">
        {{ lessonData.description }}
      </div>
    </div>

    <!-- Content -->
    <div class="row">
      <div class="ql-snow">
        <div class="ql-editor" v-bind:is="htmlContent" />
      </div>
    </div>
  </div>
</template>

<script>
import { QuillDeltaToHtmlConverter } from "quill-delta-to-html";

const formulaClass = "ql-formula";

export default {
  props: ["lessonData"],
  name: "LessonContent",
  data() {
    return {
      htmlContent: "",
      isFormulaPopupVisible: false,
      selectedFormula: undefined
    };
  },
  mounted() {
    if (this.lessonData && this.lessonData.content) {
      this.convertDelta(this.lessonData.content);
    }
  },
  methods: {
    convertDelta(delta) {
      var converter = new QuillDeltaToHtmlConverter(JSON.parse(delta).ops);

      let tempElement = document.createElement("div");
      tempElement.innerHTML = converter.convert();
      tempElement = this.generateFormulas(tempElement);

      this.htmlContent = {
        template: tempElement.outerHTML
      };
    },
    generateFormulas(container) {
      const formulas = container.getElementsByClassName(formulaClass);

      // For is reverse, because we replace the array elements
      for (let i = formulas.length - 1; i >= 0; i--) {
        let formula = formulas[i];
        let formulaCode = formula.innerText;
        let formulaComponent = document.createElement("formula-component");

        formulaComponent.setAttribute("formula-code", `${formulaCode}`);
        formula.replaceWith(formulaComponent);
      }

      return container;
    }
  },
  watch: {
    lessonData(val) {
      this.lessonData = val;

      if (this.lessonData && this.lessonData.content) {
        this.convertDelta(this.lessonData.content);
      }
    }
  }
};
</script>

<style lang="stylus" scoped>
@import '../../../css/quasar.variables.styl';

.lesson-header {
  border-bottom: 1px solid $lessons;
}
</style>
