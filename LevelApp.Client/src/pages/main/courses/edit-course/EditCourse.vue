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
            <q-tab-panel style="min-height: 600px;" name="courseTree">
              <div v-show="availableLessons.length > 0">
                <div class="q-pa-sm">
                  <span class="text-h6 text-lessons">
                    Select lessons to include in your course:
                  </span>
                </div>
                <q-scroll-area style="height: 500px;">
                  <div class="q-pa-sm">
                    <draggable @start="onDragStart($event)" @end="onDragEnd">
                      <transition-group
                        enter-active-class="animated heightAnimation fadeIn"
                        leave-active-class="animated heightAnimation fadeOut"
                      >
                        <q-card
                          class="edit-course-lesson-card q-mb-sm"
                          v-for="(val, index) in availableLessons"
                          :key="val.id"
                          :data-lesson-index="index"
                        >
                          <q-card-section> {{ val.name }} </q-card-section>
                        </q-card>
                      </transition-group>
                    </draggable>
                  </div>
                </q-scroll-area>
              </div>

              <div v-if="availableLessons.length <= 0">
                <div class="q-pa-sm flex flex-center">
                  <div class="text-subtitle2 text-align-center q-mb-md">
                    It seems like you do not have any unassigned lessons.<br />
                    Maybe you should create some?
                  </div>
                  <q-btn
                    color="lessons"
                    label="Add lesson"
                    @click="onAddLesson(false)"
                  />
                </div>
              </div>
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
      v-show="currentTab === 'courseTree'"
      :class="{
        'col-0': currentTab !== 'courseTree',
        'col-9': currentTab === 'courseTree'
      }"
    >
      <div class="q-px-md full-height relative-position">
        <transition-group
          appear
          enter-active-class="animated fadeIn"
          leave-active-class="animated fadeOut"
        >
          <!-- Dragged lesson info -->
          <div
            draggable="true"
            v-if="isDragging"
            @dragover="onDragOver($event)"
            @dragleave="onDragLeave"
            @drop="onLessonDrop($event)"
            :class="{
              'tree-edit-dragged-over': isDraggedOverDropZone
            }"
            class="flex flex-center flex-dir-col full-height tree-edit-dragged"
            key="dropzone"
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

          <!-- Cytoscape window for tree management -->
          <course-tree-editor
            @addLesson="onAddLesson(true, $event)"
            @editLesson="onEditLesson($event)"
            @removeLesson="onLessonRemovedFromTree($event)"
            @setStartingLesson="onSetStartingLesson($event)"
            @change="onEditorChange"
            ref="treeEditor"
            :read-only="false"
            key="editor"
          />
        </transition-group>
      </div>

      <!-- Drag overlay -->
      <transition
        appear
        enter-active-class="animated fadeIn"
        leave-active-class="animated fadeOut"
      >
        <div v-if="isDragging" class="drag-overlay"></div>
      </transition>
    </div>
  </div>
</template>

<script>
import Draggable from "vuedraggable";

import { InputValidators } from "../../../../validators/InputValidators";

import FormValidator from "../../../../validators/FormValidator";
import TagListComponent from "../../../../components/main/TagListComponent";
import CourseTreeEditor from "../../../../components/main/courses/CourseTreeEditor";

import { ServiceFactory } from "../../../../services/ServiceFactory";
import LocalStorageService from "../../../../services/local-storage/LocalStorageService";
const CoursesService = ServiceFactory.get("courses");
const LessonsService = ServiceFactory.get("lessons");

export default {
  name: "EditCourse",
  components: { TagListComponent, Draggable, CourseTreeEditor },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      vm.afterDataLoadCallback = function() {
        vm.restoreFormData();
        vm.clearStoredFormData();
      };
    });
  },
  beforeRouteLeave(to, from, next) {
    if (to.meta && to.meta.fromCourse) {
      this.storeFormData();
    } else {
      this.clearStoredFormData();
    }

    next();
  },
  data() {
    return {
      inputValidators: InputValidators,
      formValidator: null,
      isDragging: false,
      draggedLessonIndex: null,
      isDraggedOverDropZone: false,
      isDataFromStorage: false,
      isAddLessonFromTree: false,
      newLessonPosition: null,
      course: {
        id: 0,
        name: "",
        description: "",
        tagList: [],
        treeData: "",
        lessons: []
      },
      availableLessons: [],
      currentTab: "metadata",
      loadingCourseData: false,
      loadingLessonsData: false,
      afterDataLoadCallback: () => {}
    };
  },
  created() {
    if (this.$route.params.id) {
      this.getCourseData(this.$route.params.id);
    }
    this.getUnassignedLessons();
  },
  mounted() {
    this.initializeForm();
  },
  methods: {
    getCourseData(id) {
      this.loadingCourseData = true;
      CoursesService.getCourse(id).then(response => {
        if (!this.isDataFromStorage) {
          this.course = response.data;
        } else {
          // Update lessons data
          this.updateTreeAssignedLessonsData(response.data.lessons);
        }

        this.$refs.treeEditor.setData(
          this.course.treeData,
          this.course.lessons
        );

        this.loadingCourseData = false;
      });
    },
    getUnassignedLessons() {
      this.loadingLessonsData = true;
      LessonsService.getUnassigned().then(response => {
        this.availableLessons = this.availableLessons.concat(response.data);

        // When creating new course and data is loaded from storage
        // update data of the lessons already placed on the tree
        if (this.isDataFromStorage) {
          this.updateTreeUnassignedLessonsData();
          this.$refs.treeEditor.setData(
            this.course.treeData,
            this.course.lessons
          );
        }

        this.loadingLessonsData = false;
      });
    },
    afterDataLoad() {
      if (this.loadingCourseData || this.loadingLessonsData) {
        this.afterDataLoadCallback();
      }
    },
    initializeForm() {
      this.formValidator = new FormValidator(this.$refs.name);
    },
    storeFormData() {
      let formData = {
        course: this.course,
        currentTab: this.currentTab,
        newLessonPosition: this.newLessonPosition,
        availableLessons: this.availableLessons,
        isAddLessonFromTree: this.isAddLessonFromTree
      };

      LocalStorageService.setEditCourseData(formData);
    },
    restoreFormData() {
      let formData = LocalStorageService.getEditCourseData();

      if (formData && formData.course) {
        this.course = formData.course;
        this.$refs.treeEditor.setData(
          this.course.treeData,
          this.course.lessons
        );
        this.isDataFromStorage = true;
      }

      if (formData && formData.currentTab) {
        this.currentTab = formData.currentTab;
      }

      if (formData && formData.availableLessons) {
        this.availableLessons = this.availableLessons.concat(
          formData.availableLessons
        );
        console.log(this.availableLessons);
      }

      if (
        formData &&
        formData.isAddLessonFromTree &&
        formData.newLessonPosition
      ) {
        this.isAddLessonFromTree = false;
        this.newLessonPosition = null;

        let newLessonId = this.$route.query.lessonId;
        let newLessonIndex = this.availableLessons.findIndex(
          x => x.id === newLessonId
        );

        this.addLessonToTree(newLessonIndex, formData.newLessonPosition);
      }
    },
    addLessonToTree(availableLessonIndex, position) {
      if (availableLessonIndex && this.availableLessons[availableLessonIndex]) {
        let newLesson = this.availableLessons[availableLessonIndex];

        try {
          this.$refs.treeEditor.addLesson(newLesson, {
            x: position.x,
            y: position.y
          });
          this.course.lessons.push(newLesson);
          this.availableLessons.splice(availableLessonIndex, 1);
        } catch {
          console.error("Error on creating new lesson node.");
        }
      }
    },
    updateTreeUnassignedLessonsData() {
      this.course.lessons.forEach((lesson, index) => {
        let lessonDataIndex = this.availableLessons.findIndex(
          x => x.id === lesson.id
        );
        let lessonData = this.availableLessons.splice(lessonDataIndex, 1)[0];

        if (lessonData) {
          this.course.lessons[index] = lessonData;
        }
      });

      this.$refs.treeEditor.setData(this.course.treeData, this.course.lessons);
    },
    updateTreeAssignedLessonsData(assignedLessonsData) {
      assignedLessonsData.forEach((lessonData, index) => {
        let currentLessonDataIndex = this.course.lessons.findIndex(
          x => x.id === lessonData.id
        );

        if (currentLessonDataIndex) {
          this.course.lessons[currentLessonDataIndex] = lessonData;
        }
      });
    },
    clearStoredFormData() {
      LocalStorageService.clearEditCourseData();
    },
    onSaveClick() {
      this.formValidator.validateForm();

      if (this.formValidator.isFormValid()) {
        if (this.$route.params.id) {
          CoursesService.updateCourse(this.course).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Course has been updated!"
            });

            this.$router.push("/main/courses");
          });
        } else {
          CoursesService.createCourse(this.course).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Course has been added!"
            });

            this.$router.push("/main/courses");
          });
        }
      }
    },
    onBackClick() {
      this.clearStoredFormData();
      this.$router.push("/main/courses");
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
    onLessonDrop(event) {
      this.addLessonToTree(this.draggedLessonIndex, { x: event.x, y: event.y });
    },
    onLessonRemovedFromTree(event) {
      let lessonIndex = this.course.lessons.findIndex(
        x => x.id.toString() === event
      );

      if (lessonIndex >= 0) {
        let removedLesson = this.course.lessons.splice(lessonIndex, 1)[0];
        this.availableLessons.unshift(removedLesson);
      }
    },
    onSetStartingLesson(event) {
      this.$nextTick(() => {
        let currentStartingLesson = this.course.lessons.find(
          x => x.isFirst === true
        );
        let newStartingLesson = this.course.lessons.find(
          x => x.id.toString() === event
        );

        if (currentStartingLesson) {
          currentStartingLesson.isFirst = false;
        }

        if (newStartingLesson) {
          newStartingLesson.isFirst = true;
        } else {
          throw Error("Starting lesson is not in the lesson array.");
        }
      });
    },
    onAddLesson(isFromTree, event = null) {
      // Store new lesson position on tree
      if (event) {
        this.isAddLessonFromTree = true;
        this.newLessonPosition = event;
      }

      this.storeFormData();

      let addLessonRoute = this.$route.params.id
        ? `${this.$route.params.id}/lessons/add`
        : "add/lessons/add";

      this.$router.push(addLessonRoute);
    },
    onEditLesson(lessonId) {
      this.storeFormData();

      let editLessonRoute = this.$route.params.id
        ? `${this.$route.params.id}/lessons/edit/${lessonId}`
        : `add/lessons/edit/${lessonId}`;

      this.$router.push(editLessonRoute);
    },
    onEditorChange() {
      this.course.treeData = this.$refs.treeEditor.getData();
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
  border-radius: 5px;
  background-color: rgba(black, 0.2);
  position: absolute;
  top: 0;
  // Margin of wrapping element
  width: calc(100% - 32px);
  transition: background-color ease-in-out 0.2s;
  z-index: 10001;
}

.tree-edit-dragged-over {
  background-color: rgba(black, 0.05);
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

.drag-overlay {
  position: fixed;
  height: 100vh;
  width: 100vw;
  top: 0;
  left: 0;
  background-color: rgba(black, 0.5);
  z-index: 10000;
}

.heightAnimation {
  height: 0px;
}
</style>
