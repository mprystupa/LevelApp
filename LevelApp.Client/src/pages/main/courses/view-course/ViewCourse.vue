<template>
  <div class="row card-edit-wrapper q-ma-lg">
    <!-- Course edit card -->
    <div class="card-edit col-3">
      <q-card class="flex" style="min-height: 800px;">
        <q-card-section class="flex flex-dir-col full-width">
          <!-- Buttons -->
          <div class="row flex-center full-width q-my-lg">
            <div class="course-icon bg-courses clip-hex"></div>
          </div>

          <div class="row flex-center full-width q-mb-lg">
            <span class="text-h6 text-courses">{{ course.name }}</span>
          </div>

          <div class="row flex-center full-width q-mb-lg">
            <span class="text-subtitle2">{{ course.description }}</span>
          </div>

          <div class="row q-mt-auto full-width">
            <q-separator class="full-width q-my-md" />

            <div class="col-6">
              <q-btn
                flat
                color="accent"
                label="Go back"
                @click="onBackClick"
                icon="fas fa-arrow-left"
              />
            </div>
            <div class="col-6 flex justify-end"></div>
          </div>
        </q-card-section>
      </q-card>
    </div>

    <!-- Course tree edit -->
    <div class="tree-edit col-9">
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
import CourseTreeEditor from "../../../../components/main/courses/CourseTreeEditor";

import { ServiceFactory } from "../../../../services/ServiceFactory";
const CoursesService = ServiceFactory.get("courses");

export default {
  name: "ViewCourse",
  components: { CourseTreeEditor },
  data() {
    return {
      course: {
        id: 0,
        name: "",
        description: "",
        tagList: [],
        treeData: "",
        lessons: []
      }
    };
  },
  created() {
    if (this.$route.params.id) {
      this.getCourseData(this.$route.params.id);
    }
  },
  methods: {
    getCourseData(id) {
      CoursesService.getCourse(id).then(response => {
        this.course = response.data;
        this.$refs.treeEditor.setData(this.course.treeData, this.course.lessons);
      });
    },
    onBackClick() {
      this.$router.push(`/main/courses`);
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
