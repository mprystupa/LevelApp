<template>
  <q-card class="full-width lessons-card" :class="cardClass">
    <q-card-section class="full-height">
      <div class="row full-width full-height">
        <!-- Course icon -->
        <div class="col-1 q-pa-md">
          <div class="full-width full-height clip-hex bg-courses" />
        </div>

        <!-- Course text -->
        <div class="col-9">
          <!-- Course title -->
          <div class="row">
            <span class="text-h5">{{ lessonData.title }}</span>
          </div>

          <!-- Course description -->
          <div class="row">
            <span class="text-body2">{{ lessonData.description }}</span>
          </div>
        </div>

        <!-- Course buttons -->
        <div class="col-2 flex flex-center">
          <!-- Edit course -->
          <div class="row full-width">
            <q-btn
              flat
              rounded
              class="full-width"
              align="left"
              label="Edit course"
              icon="fas fa-edit"
            />
          </div>
          <!-- Delete course -->
          <div class="row full-width">
            <q-btn
              flat
              rounded
              class="full-width"
              :class="buttonClass"
              align="left"
              label="Delete course"
              icon="fas fa-times"
            />
          </div>
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  props: ["lessonData", "cardClass", "buttonClass"],
  name: "LessonCard"
};
</script>

<style scoped></style>
