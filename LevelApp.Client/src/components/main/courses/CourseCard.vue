<template>
  <div class="full-width">
    <q-card v-if="isEmpty" class="full-width empty-courses-card">
      <q-card-section></q-card-section>
    </q-card>
    <q-card v-else class="courses-card" :class="cardClass">
      <q-card-section class="full-height">
        <div class="row full-width full-height">
          <!-- Course icon -->
          <div class="icon-col q-pa-xs">
            <div class="full-width full-height clip-hex bg-courses" />
          </div>

          <!-- Course text -->
          <div class="col q-pl-md">
            <!-- Course title -->
            <div class="row">
              <span class="text-h5">{{ courseData.name }}</span>
            </div>

            <!-- Course description -->
            <div class="row">
              <span class="text-body2">{{ courseData.description }}</span>
            </div>
          </div>

          <!-- Course buttons -->
          <div class="button-col flex flex-center">
            <!-- Edit buttons -->
            <div v-if="courseData.permissions.CanEdit">
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

            <!-- View buttons -->
            <div v-else>
              <q-btn
                flat
                rounded
                class="full-width"
                align="left"
                label="Attend course"
                icon="fas fa-chalkboard-teacher"
                @click="$emit('attend')"
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
  props: ["courseData", "cardClass", "buttonClass", "isEmpty"],
  name: "CourseCard"
};
</script>

<style scoped>
.icon-col {
  flex: 0 0 7em;
  height: 100%;
}

.button-col {
  flex: 0 0 15em;
  height: 100%;
}
</style>
