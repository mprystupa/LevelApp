<template>
  <q-card class="q-ma-lg">
    <q-card-section class="q-pa-lg">
      <lesson-content :lessonData="lesson"></lesson-content>

      <q-separator class="q-mb-lg" />

      <!-- Buttons -->
      <div class="row full-width">
        <div class="col-6">
          <q-btn flat color="accent" label="Go back" @click="onBackClick" icon="fas fa-arrow-left" />
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script>
import LessonContent from "../../../../components/main/lessons/LessonContent";
import { ServiceFactory } from "../../../../services/ServiceFactory";
const LessonsService = ServiceFactory.get("lessons");

export default {
  name: "ViewLesson",
  components: { LessonContent },
  data() {
    return {
      lesson: {
        id: 0,
        name: "",
        description: "",
        content: ""
      }
    };
  },
  created() {
    if (this.$route.params.id) {
      this.getLessonData(this.$route.params.id);
    }
  },
  methods: {
    getLessonData(id) {
      LessonsService.getLesson(id).then(response => {
        this.lesson = response.data;

        // We use additional value to load content just the first time and then do not update it,
        // instead updating directly lesson.content value
        this.editableContent = this.lesson.content;
      });
    },
    onBackClick() {
      this.$router.push("/main/lessons");
    }
  }
};
</script>

<style scoped lang="stylus">
.lesson-icon {
  width: 200px;
  height: 200px;
}
</style>
