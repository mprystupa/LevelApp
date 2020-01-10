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
        <div class="ql-editor" v-html="htmlContent" />
      </div>
    </div>
  </div>
</template>

<script>
import { QuillDeltaToHtmlConverter } from "quill-delta-to-html";

export default {
  props: ["lessonData"],
  name: "LessonContent",
  data() {
    return {
      htmlContent: ""
    };
  },
  mounted() {
    if (this.lessonData && this.lessonData.content) {
      this.convertDelta(this.lessonData.content);
    }
  },
  methods: {
    convertDelta(delta) {
      var converter = new QuillDeltaToHtmlConverter(
        JSON.parse(this.lessonData.content).ops
      );

      this.htmlContent = converter.convert();
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
