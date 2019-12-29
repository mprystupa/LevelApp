<template>
  <q-card flat bordered class="full-width add-lessons-card" :class="color">
    <q-card-section class="full-height">
      <div class="row full-width full-height flex flex-center">
        <div class="row full-width flex flex-center">
          <i class="fas fa-plus-square fa-3x"></i>
        </div>
        <div class="row full-width flex flex-center">
          <span class="text-h5">Add Lesson</span>
        </div>
      </div>
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  props: ["color"],
  name: "AddLessonCard"
};
</script>

<style lang="stylus"></style>
