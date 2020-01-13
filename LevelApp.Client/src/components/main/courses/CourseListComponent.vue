<template>
  <div>
    <div v-if="courses && courses.length > 0">
      <div
        class="row q-ma-md"
        v-for="(course, index) in courses"
        :key="course.id"
      >
        <course-card
          :course-data="course"
          :card-class="getCardClass(index)"
          button-class="course-card-entry"
          @edit="$emit('edit', course.id)"
          @delete="$emit('delete', course.id)"
          @view="$emit('view', course.id)"
          @attend="$emit('attend', course.id)"
          @continue="$emit('continue', course.id)"
          @favourite="
            $emit('favourite', {
              lessonId: course.id,
              favouriteLoaderHandler: $event
            })
          "
        />
      </div>
    </div>

    <div
      class="row q-ma-md"
      v-for="index in cardsPerPage - courses.length >= 0
        ? cardsPerPage - courses.length
        : 0"
      :key="index"
    >
      <course-card :is-empty="true" />
    </div>

    <!-- Pagination -->
    <div class="row q-mx-lg q-mt-lg flex flex-center">
      <q-pagination
        v-model="currentPage"
        :max="totalPages"
        color="courses"
        @input="$emit('pageChange', currentPage)"
      />
    </div>
  </div>
</template>

<script>
import CourseCard from "./CourseCard";

export default {
  props: ["courses", "cardsPerPage", "totalPages"],
  name: "CourseListComponent",
  components: {
    CourseCard
  },
  data() {
    return {
      currentPage: 1
    }
  },
  mounted() {
    console.log(this.courses)
  },
  methods: {
    getCardClass(index) {
      return index % 2 === 0
        ? "course-card-entry-light"
        : "course-card-entry-dark";
    }
  }
};
</script>

<style lang="stylus" scoped>
@import "../../../css/quasar.variables.styl";

>>> .course-card-entry-light {
  background-color: $courses-item-light;
}

>>> .course-card-entry-dark {
  background-color: $courses-item-dark;
}
</style>
