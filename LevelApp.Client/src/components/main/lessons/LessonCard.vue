<template>
  <div class="full-width">
    <q-card
      v-if="isEmpty"
      class="full-width empty-lessons-card"
      :class="cardClass"
    >
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

          <!-- Lesson buttons (edit) -->
          <div
            v-if="lessonData.permissions.CanEdit"
            class="button-col flex flex-center"
          >
            <!-- Edit lesson -->
            <div class="row full-width">
              <q-btn
                flat
                rounded
                class="full-width"
                align="left"
                label="Edit lesson"
                icon="fas fa-edit"
                @click="$emit('edit')"
              />
            </div>

            <!-- Delete lesson -->
            <div class="row full-width">
              <q-btn
                flat
                rounded
                class="full-width"
                :class="buttonClass"
                align="left"
                label="Delete lesson"
                icon="fas fa-times"
                @click="$emit('delete')"
              />
            </div>
          </div>

          <!-- Author data and lesson buttons (view) -->
          <div v-else class="button-col flex flex-center">
            <div class="row full-width">
              <div class="q-mb-sm" v-if="lessonData.author">
                <div class="text-subtitle2">
                  <strong>Created by:</strong> <br/> {{ lessonData.author.displayName }}
                </div>
              </div>
              <q-btn
                flat
                rounded
                class="full-width"
                :class="buttonClass"
                align="left"
                label="View lesson"
                icon="fas fa-book-open"
                @click="$emit('view')"
              />
            </div>
          </div>
        </div>

        <!-- Lesson statistics -->
        <q-card class="lesson-statistics-card">
          <q-card-section>
            <div class="flex flex-center justify-around">
              <div v-if="lessonData.course">
                <span>Assigned to </span><span @click="onAssignedCourseClick(lessonData.course.id)" class="text-courses text-bold cursor-pointer">{{ lessonData.course.name }}</span>
              </div>
              <div v-else>
                <span>Not assigned to any course.</span>
              </div>
              <div v-if="!lessonData.permissions.CanEdit">
                <!-- Not favourite -->
                <q-btn
                  v-if="!lessonData.isFavourite"
                  flat
                  :loading="isFavouriteButtonLoading"
                  icon="fas fa-star"
                  rounded
                  color="favourite"
                  label="Amazing?"
                  @click="onFavouriteClick"
                />

                <!-- Favourite -->
                <q-btn
                  v-else
                  :loading="isFavouriteButtonLoading"
                  icon="fas fa-star"
                  rounded
                  color="favourite"
                  label="Amazing!"
                  @click="onFavouriteClick"
                />
              </div>
            </div>
          </q-card-section>
        </q-card>
      </q-card-section>
    </q-card>
  </div>
</template>

<script>
export default {
  props: ["lessonData", "cardClass", "buttonClass", "isEmpty"],
  name: "LessonCard",
  data() {
    return {
      isFavouriteButtonLoading: false
    };
  },
  methods: {
    onFavouriteClick() {
      this.isFavouriteButtonLoading = true;
      this.$emit("favourite", this.finishLoading);
    },
    onAssignedCourseClick(courseId) {
      console.log(courseId);
    },
    finishLoading() {
      setTimeout(() => {
        this.isFavouriteButtonLoading = false;
      }, 400);
    }
  }
};
</script>

<style lang="stylus" scoped>
.icon-col {
  flex: 0 0 6em;
  height: 100%;
}

.button-col {
  flex: 0 0 15em;
  height: 100%;
}

.lesson-statistics-card {
  min-height: 46px;
  width: 45%;
  margin-left: auto;
  margin-right: auto;
  position: relative;
  top: -10px;
  border-radius: 15px;

  & .q-card__section {
    padding: 5px;
  }
}
</style>
