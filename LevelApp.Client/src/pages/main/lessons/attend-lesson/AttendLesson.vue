<template>
  <q-card class="q-ma-lg">
    <q-card-section class="q-pa-lg">
      <lesson-content :lessonData="lesson"></lesson-content>

      <q-separator class="q-mb-lg" />

      <!-- Buttons -->
      <div class="row full-width">
        <div class="col-6">
          <q-btn
            flat
            color="accent"
            label="Go back"
            @click="onBackClick"
            icon="fas fa-arrow-left"
          />
        </div>

        <div class="col-6 flex justify-end">
          <q-btn
            color="primary"
            label="Finish"
            @click="onFinishClick"
            icon="fas fa-check"
          />
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script>
import LocalStorageService from "../../../../services/local-storage/LocalStorageService";

import LessonContent from "../../../../components/main/lessons/LessonContent";
import { ServiceFactory } from "../../../../services/ServiceFactory";
const LessonsService = ServiceFactory.get("lessons");

export default {
  name: "AttendLesson",
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
    if (this.$route.params.lessonId) {
      this.getLessonData(this.$route.params.lessonId);
    }
  },
  methods: {
    getLessonData(id) {
      LessonsService.getLesson(id).then(response => {
        this.lesson = response.data;
      });
    },
    onBackClick() {
      LocalStorageService.clearLockedLessonsIds();
      this.$router.go(-1);
    },
    onFinishClick() {
      let lessonId = this.$route.params.lessonId;
      let courseId = this.$route.params.courseId;

      let lockedLessonsIds = LocalStorageService.getLockedLessonsIds();

      LessonsService.finishLesson({
        id: lessonId,
        lockedLessonsIds: lockedLessonsIds
      }).then(() => {
        LocalStorageService.clearLockedLessonsIds();
        this.$router.push(`/main/courses/view/${courseId}`);
      });
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
