<template>
  <q-card class="q-ma-lg">
    <q-card-section>
      <div class="text-h5 q-mb-lg">Add Lesson</div>

      <q-separator class="q-mb-lg" />

      <q-expansion-item default-opened class="q-mb-lg">
        <template v-slot:header>
          <q-item-section avatar>
            <q-icon name="fas fa-tags" color="lessons" />
          </q-item-section>

          <q-item-section class="text-lessons text-h6">
            Metadata
          </q-item-section>
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
                        inputValidators.MaxLength(val, 50) || 'Name is too long'
                    ]"
                  />

                  <!-- Description -->
                  <q-input
                    class="full-width"
                    filled
                    v-model="lesson.description"
                    type="textarea"
                    rows="15"
                    label="Description"
                  />
                </div>
              </div>

              <!-- Icon uploader -->
              <div class="col-4">
                <div class="row q-ma-sm">
                  <div class="row flex-center full-width q-mb-md">
                    <span class="text-h6 text-lessons">Lesson icon</span>
                  </div>
                  <div class="row flex-center full-width q-mb-lg">
                    <div class="lesson-icon bg-secondary clip-hex"></div>
                  </div>
                  <div class="row flex-center full-width">
                    <q-uploader url="http://localhost:4444/upload" />
                  </div>
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

          <q-item-section class="text-lessons text-h6">
            Content
          </q-item-section>
        </template>
        <q-card>
          <q-card-section>
            <!-- Editable lesson -->
            <editable-content :value="lesson.content" />
          </q-card-section>
        </q-card>
      </q-expansion-item>

      <q-separator class="q-mb-lg" />

      <!-- Buttons -->
      <div class="row full-width">
        <div class="col-6">
          <q-btn flat color="accent" label="Go back" icon="fas fa-arrow-left" />
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
import { ServiceFactory } from "../../../../services/ServiceFactory";
const LessonsService = ServiceFactory.get("lessons");

export default {
  name: "EditLesson",
  components: { EditableContent },
  data() {
    return {
      inputValidators: InputValidators,
      formValidator: null,
      lesson: {
        name: "",
        description: "",
        content: ""
      }
    };
  },
  created() {
    if (this.$route.params.id) {
      this.getLessonData(this.$route.params.id);
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
      });
    },
    onSaveClick() {
      this.formValidator.validateForm();

      if (this.formValidator.isFormValid()) {
        if (this.$route.params.id) {
          LessonsService.updateLesson(this.lesson).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Lesson has been updated!"
            });

            this.$router.push("/main/lessons");
          });
        } else {
          LessonsService.createLesson(this.lesson)
            .then(() => {
              this.$q.notify({
                color: "positive",
                icon: "fa fas-check",
                message: "Lesson has been added!"
              });

              this.$router.push("/main/lessons");
            })
            .catch(error => {
              let errorMessage = "Something went wrong.";
              if (error.response && error.response.data) {
                errorMessage = error.response.data.Message;
              }

              this.$q.notify({
                color: "negative",
                icon: "fas fa-times",
                message: errorMessage,
                position: "top"
              });
            });
        }
      }
    }
  }
};
</script>

<style scoped lang="stylus">
.lesson-icon {
  width: 200px;
  height: 200px;
}
</style>
