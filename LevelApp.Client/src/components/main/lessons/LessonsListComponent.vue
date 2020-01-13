<template>
  <div>
    <div v-if="lessons && lessons.length > 0">
      <div
        class="row q-ma-lg"
        v-for="(lesson, index) in lessons"
        :key="lesson.id"
      >
        <lesson-card
          :lesson-data="lesson"
          :card-class="getCardClass(index)"
          button-class="course-card-entry"
          @edit="$emit('edit', lesson.id)"
          @delete="$emit('delete', lesson.id)"
          @view="$emit('view', lesson.id)"
          @favourite="
            $emit('favourite', {
              lessonId: lesson.id,
              favouriteLoaderHandler: $event
            })
          "
        />
      </div>
    </div>
    <div
      class="row q-ma-lg"
      v-for="index in cardsPerPage - lessons.length >= 0
        ? cardsPerPage - lessons.length
        : 0"
      :key="index"
    >
      <lesson-card :is-empty="true"></lesson-card>
    </div>

    <!-- Pagination -->
    <div class="row q-mx-lg q-mt-lg flex flex-center">
      <q-pagination
        v-model="currentPage"
        :max="totalPages"
        color="lessons"
        @input="$emit('pageChange', currentPage)"
      />
    </div>
  </div>
</template>

<script>
import LessonCard from "./LessonCard";

export default {
  props: ["lessons", "cardsPerPage", "totalPages"],
  name: "LessonsListComponent",
  components: {
    LessonCard
  },
  data() {
    return {
      currentPage: 1
    }
  },
  methods: {
    getCardClass(index) {
      return index % 2 === 0
        ? "lesson-card-entry-light"
        : "lesson-card-entry-dark";
    }
  }
};
</script>

<style lang="stylus" scoped>
@import '../../../css/quasar.variables.styl';

.lesson-card-entry {
  color: $secondary;
}

>>> .lesson-card-entry-light {
  @extend .lesson-card-entry;
  background-color: $lessons-item-light;
}

>>> .lesson-card-entry-dark {
  @extend .lesson-card-entry;
  background-color: $lessons-item-dark;
}
</style>
