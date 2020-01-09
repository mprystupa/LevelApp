<template>
  <div class="row card-edit-wrapper q-ma-lg">
    <!-- Course edit card -->
    <div
      class="card-edit"
      class="col-3"
    >
      <q-card>
        <q-card-section>
          <div class="row">
            <div
              class="col-12"
            >
              <div class="text-h5">Add Course</div>
            </div>
          </div>

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
      class="col-9"
    >
      <div class="q-px-md full-height relative-position">
        <!-- Cytoscape window for tree management -->
        <course-tree-editor
          ref="treeEditor"
          :read-only="true"
          key="editor"
        />
      </div>
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
  const CoursesService = ServiceFactory.get("courses");
  const LessonsService = ServiceFactory.get("lessons");

  export default {
    name: "ViewCourse",
    components: { TagListComponent, Draggable, CourseTreeEditor },
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
          tagList: [],
          treeData: "",
          lessons: []
        },
        availableLessons: [],
        currentTab: "metadata"
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
        CoursesService.getCourse(id).then(response => {
          this.course = response.data;
          this.$refs.treeEditor.setData(this.course.treeData);
        });
      },
      getUnassignedLessons() {
        LessonsService.getUnassigned().then(response => {
          this.availableLessons = response.data;
        })
      },
      initializeForm() {
        this.formValidator = new FormValidator(this.$refs.name);
      },
      onSaveClick() {
        this.formValidator.validateForm();

        if (this.formValidator.isFormValid()) {
          this.course.treeData = this.$refs.treeEditor.getData();

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
      onLessonDrop(event) {
        if (
          this.draggedLessonIndex &&
          this.availableLessons[this.draggedLessonIndex]
        ) {
          let droppedLesson = this.availableLessons[this.draggedLessonIndex];

          try {
            this.$refs.treeEditor.addLesson(droppedLesson, {
              x: event.x,
              y: event.y
            });
            this.course.lessons.push(droppedLesson);
            this.availableLessons.splice(this.draggedLessonIndex, 1);
          } catch {
            console.error("Error on creating new lesson node.");
          }
        }
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
      onAddLessonClick() {
        this.$router.push("/main/lessons/add");
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
