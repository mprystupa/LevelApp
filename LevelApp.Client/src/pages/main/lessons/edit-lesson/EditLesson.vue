<template>
  <q-card class="q-ma-lg">
    <q-card-section>
      <div class="row">
        <div class="col-12 col-md-6">
          <div class="text-h5">Add Lesson</div>
        </div>

        <div class="col-12 col-md-6">
          <q-tabs v-model="currentTab" align="right" class="text-lessons">
            <q-tab label="Edit" @click="onEditCardClick" name="edit" />
            <q-tab label="Preview" name="preview" />
          </q-tabs>
        </div>
      </div>

      <q-separator />

      <q-tab-panels v-model="currentTab" animated>
        <!-- Edit tab -->
        <q-tab-panel name="edit">
          <q-expansion-item default-opened class="q-mb-lg">
            <template v-slot:header>
              <q-item-section avatar>
                <q-icon name="fas fa-tags" color="lessons" />
              </q-item-section>

              <q-item-section class="text-lessons text-h6"
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
                        class="full-width q-mb-lg"
                        filled
                        v-model="lesson.name"
                        ref="name"
                        label="Name"
                        :rules="[
                          val =>
                            inputValidators.Required(val) || 'Name is required',
                          val =>
                            inputValidators.MaxLength(val, 50) ||
                            'Name is too long'
                        ]"
                      />

                      <!-- Description -->
                      <q-input
                        class="full-width q-mb-lg"
                        filled
                        v-model="lesson.description"
                        type="textarea"
                        rows="15"
                        label="Description"
                      />

                      <!-- Tags -->
                      <div class="row full-width q-mb-md">
                        <span class="text-h6 text-courses">Tags</span>
                      </div>

                      <div class="row full-width">
                        <tag-list-component v-model="lesson.tagList" />
                      </div>
                    </div>
                  </div>

                  <!-- Icon uploader -->
                  <div class="col-4">
                    <div class="row flex-center q-ma-sm">
                      <div class="row flex-center full-width q-mb-xl">
                        <span class="text-h6 text-lessons">Lesson icon</span>
                      </div>
                      <icon-loader
                        icon-color="bg-lessons"
                        :icon-url="currentIconUrl"
                        @change="onIconChange($event)"
                      />
                    </div>
                  </div>
                </div>
              </q-card-section>
            </q-card>
          </q-expansion-item>

          <q-expansion-item default-opened class="q-mb-lg">
            <template v-slot:header>
              <q-item-section avatar>
                <q-icon name="fas fa-book-open" color="lessons" />
              </q-item-section>

              <q-item-section class="text-lessons text-h6"
                >Content</q-item-section
              >
            </template>
            <q-card>
              <q-card-section>
                <!-- Editable lesson -->
                <editable-content
                  :value="editableContent"
                  ref="editor"
                  @input="onEditableContentInput($event)"
                />
              </q-card-section>
            </q-card>
          </q-expansion-item>
        </q-tab-panel>

        <!-- Preview card -->
        <q-tab-panel name="preview">
          <q-expansion-item default-opened class="q-mb-lg">
            <template v-slot:header>
              <q-item-section avatar>
                <q-icon name="fas fa-book-open" color="lessons" />
              </q-item-section>

              <q-item-section class="text-lessons text-h6"
                >Lesson preview</q-item-section
              >
            </template>
            <q-card>
              <q-card-section>
                <!-- Lesson preview -->
                <lesson-content :lessonData="lesson" />
              </q-card-section>
            </q-card>
          </q-expansion-item>
        </q-tab-panel>
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
</template>

<script>
import { InputValidators } from "../../../../validators/InputValidators";
import FormValidator from "../../../../validators/FormValidator";
import EditableContent from "../../../../components/main/EditableContent";
import LessonContent from "../../../../components/main/lessons/LessonContent";
import { ServiceFactory } from "../../../../services/ServiceFactory";
import IconLoader from "../../../../components/main/IconLoader";
import TagListComponent from "../../../../components/main/TagListComponent";
const LessonsService = ServiceFactory.get("lessons");

export default {
  name: "EditLesson",
  components: { TagListComponent, IconLoader, EditableContent, LessonContent },
  data() {
    return {
      inputValidators: InputValidators,
      formValidator: null,
      lesson: {
        id: 0,
        name: "",
        description: "",
        content: "",
        tagList: [],
        iconFile: null
      },
      currentIconUrl: "",
      editableContent: {},
      htmlContent: "",
      currentTab: "edit",
      courseId: null
    };
  },
  created() {
    if (this.$route.params.id) {
      this.getLessonData(this.$route.params.id);
    }

    if (this.$route.params.courseId) {
      this.courseId = this.$route.params.courseId;
    }
  },
  mounted() {
    this.initializeForm();
  },
  methods: {
    initializeForm() {
      this.formValidator = new FormValidator(this.$refs.name);
    },
    getLessonData(id) {
      LessonsService.getLesson(id).then(response => {
        this.lesson = response.data;

        // We use additional value to load content just the first time and then do not update it,
        // instead updating directly lesson.content value
        this.editableContent = this.lesson.content;
      });
    },
    onSaveClick() {
      this.formValidator.validateForm();

      if (this.formValidator.isFormValid()) {
        let returnRoute = this.getReturnRoute();

        if (this.$route.params.id) {
          LessonsService.updateLesson(this.lesson).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Lesson has been updated!"
            });

            this.$router.push(returnRoute);
          });
        } else {
          LessonsService.createLesson(this.lesson).then(response => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Lesson has been added!"
            });

            this.$router.push(`${returnRoute}?lessonId=${response.data}`);
          });
        }
      }
    },
    onEditCardClick() {
      // Fix for disappearing Quill content after card change
      setTimeout(() => {
        this.$refs.editor.reloadData(this.lesson.content);
        this.initializeForm();
      }, 20);
    },
    onBackClick() {
      this.$router.go(-1);
    },
    onEditableContentInput($event) {
      this.lesson.content = $event.stringContent;
      this.htmlContent = $event.htmlContent;
    },
    onIconChange(event) {
      this.lesson.iconFile = event;
      console.log(this.lesson.iconFile);
    },
    getReturnRoute() {
      if (this.$route.meta && this.$route.meta.fromCourse) {
        if (this.$route.meta.newCourse) {
          return "/main/courses/add";
        }

        return `/main/courses/edit/${this.$route.params.courseId}`;
      }

      return "/main/lessons";
    }
  }
};
</script>

<style scoped lang="stylus">
@import "../../../../css/levelapp.variables.styl"

.lesson-icon {
  width: 200px;
  height: 200px;
  cursor: pointer;
}

.icon-input {
  visibility: hidden;
}

.icon-wrapper {
  position: relative;
  transition: filter ease-in-out 0.2s;
}

.icon-wrapper:hover {
  filter: drop-shadow(2px 2px 6px black);
}

.icon-overlay {
  position: absolute;
  background-color: rgba(white, 0);
  transition: background-color ease-in-out 0.2s;
  pointer-events: none;

  span {
    transition: color ease-in-out 0.2s;
    color: rgba(white, 0);
  }
}

.icon-wrapper:hover .icon-overlay {
  background-color: rgba(white, 0.1);

  span {
    color: rgba(white, 1);
  }
}
</style>
