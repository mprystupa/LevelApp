<template>
  <div class="full-width">
    <q-card v-if="isEmpty" class="full-width empty-lessons-card" :class="cardClass">
      <q-card-section></q-card-section>
    </q-card>
    <q-card v-else class="full-width lessons-card" :class="cardClass">
      <q-card-section class="full-height">
        <div class="row full-width full-height">
          <!-- Course icon -->
          <div class="icon-col q-pa-xs">
            <div class="full-width full-height clip-hex bg-courses" />
          </div>

          <!-- Course text -->
          <div class="col q-pl-sm">
            <!-- Course title -->
            <div class="row">
              <span class="text-h5">{{ lessonData.name }}</span>
            </div>

            <!-- Course description -->
            <div class="row">
              <span class="text-body2">{{ lessonData.description }}</span>
            </div>
          </div>

          <!-- Course buttons -->
          <div class="button-col flex flex-center">
            <!-- Edit course -->
            <div class="row full-width">
              <q-btn
                flat
                rounded
                class="full-width"
                align="left"
                label="Edit course"
                icon="fas fa-edit"
                @click="$emit('edit')"
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
                @click="$emit('delete')"
              />
            </div>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </div>
</template>

<script>
export default {
  props: ["lessonData", "cardClass", "buttonClass", "isEmpty"],
  name: "LessonCard"
};
</script>

<style scoped>
.icon-col {
  flex: 0 0 5em;
  height: 100%;
}

.button-col {
  flex: 0 0 15em;
  height: 100%;
}
</style>
