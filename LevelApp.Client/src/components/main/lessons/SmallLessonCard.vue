<template>
  <q-card
    class="edit-course-lesson-card q-mb-sm cursor-pointer"
    :data-lesson-id="lessonData.id"
  >
    <q-card-section class="full-height">
      <div class="row full-width full-height">
        <!-- Course icon -->
        <div class="icon-col q-pa-xs">
          <div class="full-width full-height relative-position clip-hex bg-courses">
            <img v-if="lessonData.iconUrl" class="icon-img" :src="lessonData.iconUrl" />
          </div>
        </div>

        <!-- Course text -->
        <div class="col q-pl-sm">
          <!-- Course title -->
          <div class="row">
            <span class="text-h6">{{ lessonData.name }}</span>
          </div>

          <!-- Course description -->
          <div class="row">
            <span class="text-body2">{{ lessonData.description }}</span>
          </div>
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  props: ["lessonData"],
  name: "SmallLessonCard"
};
</script>

<style scoped>
.icon-col {
  flex: 0 0 5em;
  height: 100%;
}
</style>
