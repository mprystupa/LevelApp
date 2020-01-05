<template>
  <div class="row card-edit-wrapper q-ma-lg">
    <!-- Course edit card -->
    <div
      class="card-edit"
      :class="{
        'col-12': currentTab !== 'courseTree',
        'col-3': currentTab === 'courseTree'
      }"
    >
      <q-card>
        <q-card-section>
          <div class="row">
            <div
              :class="{
                'col-12 col-md-6': currentTab !== 'courseTree',
                'col-12': currentTab === 'courseTree'
              }"
            >
              <div class="text-h5">Add Course</div>
            </div>

            <div
              :class="{
                'col-12 col-md-6': currentTab !== 'courseTree',
                'col-12': currentTab === 'courseTree'
              }"
            >
              <q-tabs v-model="currentTab" align="right" class="text-courses">
                <q-tab label="Metadata" name="metadata" />
                <q-tab label="Course Tree" name="courseTree" />
              </q-tabs>
            </div>
          </div>

          <q-separator />

          <q-tab-panels
            v-model="currentTab"
            animated
            transition-prev="fade"
            transition-next="fade"
          >
            <!-- Metadata tab -->
            <q-tab-panel name="metadata">
              <q-expansion-item default-opened class="q-mb-lg">
                <template v-slot:header>
                  <q-item-section avatar>
                    <q-icon name="fas fa-tags" color="courses" />
                  </q-item-section>

                  <q-item-section class="text-courses text-h6"
                    >Metadata</q-item-section
                  >
                </template>
                <q-card>
                  <q-card-section>
                    <div class="row full-width">
                      <!-- Metadata form -->
                      <div class="col-8">
                        <div class="row full-width">
                          <!-- Name -->
                          <q-input
                            class="full-width q-mb-sm"
                            filled
                            v-model="course.name"
                            ref="name"
                            label="Name"
                            :rules="[
                              val =>
                                inputValidators.Required(val) ||
                                'Name is required',
                              val =>
                                inputValidators.MaxLength(val, 50) ||
                                'Name is too long'
                            ]"
                          />

                          <!-- Description -->
                          <q-input
                            class="full-width q-mb-lg"
                            filled
                            v-model="course.description"
                            type="textarea"
                            rows="10"
                            label="Description"
                          />

                          <!-- Tags -->
                          <div class="row full-width q-mb-md">
                            <span class="text-h6 text-courses">Tags</span>
                          </div>

                          <div class="row full-width">
                            <tag-list-component v-model="course.tagList" />
                          </div>
                        </div>
                      </div>

                      <!-- Icon uploader -->
                      <div class="col-4">
                        <div class="row q-ma-sm">
                          <div class="row flex-center full-width q-mb-md">
                            <span class="text-h6 text-courses"
                              >Course icon</span
                            >
                          </div>
                          <div class="row flex-center full-width q-mb-lg">
                            <div class="course-icon bg-courses clip-hex"></div>
                          </div>
                          <div class="row flex-center full-width">
                            <q-uploader
                              color="courses"
                              url="http://localhost:4444/upload"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </q-card-section>
                </q-card>
              </q-expansion-item>
            </q-tab-panel>

            <!-- Course tree tab -->
            <q-tab-panel name="courseTree">
              <div class="q-pa-sm">
                <span class="text-h6 text-lessons">
                  Select lessons to include in your course:
                </span>
              </div>
              <q-scroll-area style="height: 500px;">
                <div class="q-pa-sm">
                  <draggable
                    @start="onDragStart($event)"
                    @end="onDragEnd"
                  >
                    <transition-group
                      appear
                      leave-active-class="animated heightAnimation fadeOut"
                      class="q-gutter-sm q-ma-none"
                    >
                      <q-card
                        class="edit-course-lesson-card"
                        v-for="(val, index) in availableLessons"
                        :key="val.name"
                        :data-lesson-index="index"
                      >
                        <q-card-section> {{ val.name }} </q-card-section>
                      </q-card>
                    </transition-group>
                  </draggable>
                </div>
              </q-scroll-area>
            </q-tab-panel>

            <!-- Achievements tab -->
            <q-tab-panel name="achievements"> </q-tab-panel>
          </q-tab-panels>

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
                label="Save"
                @click="onSaveClick"
                icon-right="fas fa-check"
              />
            </div>
          </div>
        </q-card-section>
      </q-card>
    </div>

    <!-- Course tree edit -->
    <div
      class="tree-edit"
      :class="{
        'col-0': currentTab !== 'courseTree',
        'col-9': currentTab === 'courseTree'
      }"
    >
      <div class="q-pa-md full-height">
        <!-- Dragged lesson info -->
        <transition
          appear
          enter-active-class="animated fadeIn"
          leave-active-class="animated fadeOut"
        >
          <div
            draggable="true"
            v-if="isDragging"
            @dragover="onDragOver($event)"
            @dragleave="onDragLeave"
            @drop="onLessonDrop"
            :class="{
              'tree-edit-dragged-over': isDraggedOverDropZone
            }"
            class="flex flex-center flex-dir-col full-height tree-edit-dropzone tree-edit-dragged"
          >
            <div class="row relative-position q-mb-md">
              <img
                src="../../../../assets/main/drag-drop-icon.svg"
                class="drag-drop-icon"
              />
              <div class="pulse"></div>
            </div>
            <div class="row">
              <span class="text-white text-h6"
                >Drop lesson here to add it to your course tree</span
              >
            </div>
          </div>
        </transition>
      </div>
    </div>
  </div>
</template>

<script>
import Draggable from "vuedraggable";
import { InputValidators } from "../../../../validators/InputValidators";

import FormValidator from "../../../../validators/FormValidator";
import TagListComponent from "../../../../components/main/TagListComponent";

export default {
  name: "EdiCourse",
  components: { TagListComponent, Draggable },
  data() {
    return {
      inputValidators: InputValidators,
      formValidator: null,
      isDragging: false,
      draggedLessonIndex: null,
      isDraggedOverDropZone: false,
      course: {
        id: 0,
        name: "",
        description: "",
        tagList: []
      },
      availableLessons: [],
      lessonsOnTree: [],
      currentTab: "metadata"
    };
  },
  created() {
    for (let i = 0; i < 25; i++) {
      this.availableLessons.push({
        name: `Test ${i}`
      });
    }
  },
  mounted() {
    this.initializeForm();
  },
  methods: {
    initializeForm() {
      this.formValidator = new FormValidator(this.$refs.name);
    },
    onSaveClick() {
      this.formValidator.validateForm();

      if (this.formValidator.isFormValid()) {
        /* if (this.$route.params.id) {
          LessonsService.updateLesson(this.lesson).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Lesson has been updated!"
            });

            this.$router.push("/main/lessons");
          });
        } else {
          LessonsService.createLesson(this.lesson).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Lesson has been added!"
            });

            this.$router.push("/main/lessons");
          });
        } */
      }
    },
    onBackClick() {
      this.$router.go(-1);
    },
    onDragStart(event) {
      this.draggedLessonIndex = event.item.getAttribute("data-lesson-index");
      this.isDragging = true;
    },
    onDragEnd() {
      this.draggedLessonIndex = null;
      this.isDragging = false;
    },
    onDragOver(event) {
      this.isDraggedOverDropZone = true;
      event.preventDefault();
    },
    onDragLeave() {
      this.isDraggedOverDropZone = false;
    },
    onLessonDrop() {
      if (this.draggedLessonIndex) {
        let droppedLesson = this.availableLessons.splice(
          this.draggedLessonIndex,
          1
        );
        this.lessonsOnTree.push(droppedLesson);

        console.log(this.availableLessons);
        console.log(this.lessonsOnTree);
      }
    }
  }
};
</script>

<style scoped lang="stylus">
@import '../../../../css/quasar.variables.styl';
.course-icon {
  width: 200px;
  height: 200px;
}

.tree-edit {
  transition: width ease-in-out 0.2s;
}

.card-edit {
  transition: width ease-in-out 0.2s;
  align-self: flex-end;
}

.card-edit-wrapper {
  flex-direction: row-reverse;
}

.edit-course-lesson-card {
  background-color: $lessons-item-light;
  height: 100px;
  transition: height ease-in-out 0.2s;
}

.tree-edit-dragged {
  border: white dashed 4px;
  background-color: rgba(black, 0.1);
  transition: background-color ease-in-out 0.2s;
}

.tree-edit-dragged-over {
  background-color: rgba(white, 0.05);
}

.drag-drop-icon {
  width: 150px;
  height: 150px;
}

.pulse {
  width: 300px;
  height: 300px;
  border-radius: 50%;
  position: absolute;
  top: -50%;
  left: -50%;
  animation: pulse 2s infinite;
}
@keyframes pulse {
  from {
    transform: scale(0);
    background-color: rgba(white, 0.7);
  }
  to {
    transform: scale(1);
    background-color: rgba(white, 0);
  }
}

.heightAnimation {
  height: 0px;
}
</style>
