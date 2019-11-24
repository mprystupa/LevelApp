<template>
  <div>
    <search-component color="lessons">
      <template v-slot:title>
        <div class="text-lessons q-pa-md">
          <i class="fas fa-book fa-2x q-mr-md"></i>
          <span class="text-h4">Lessons</span>
        </div>
      </template>

      <template v-slot:tabs>
        <q-tabs align="right" no-caps shrink v-model="tab" class="text-lessons">
          <q-tab name="created" icon="fas fa-user-edit" label="Created" />
          <q-tab
            name="completed"
            icon="fas fa-check-square"
            label="Completed"
          />
          <q-tab name="awaiting" icon="fas fa-clock" label="Awaiting" />
          <q-tab name="favourite" icon="fas fa-star" label="Favourite" />
        </q-tabs>
      </template>

      <template v-slot:filters>
        <div class="row q-mb-sm">
          <span class="text-lessons text-subtitle2">
            <q-icon name="fas fa-filter q-mr-sm" />Filter by
          </span>
        </div>
        <div class="row q-mb-xl">
          <q-btn-group flat spread class="full-width">
            <q-btn-dropdown
              rounded
              outline
              color="lessons"
              label="Name"
              icon="fas fa-sort-alpha-up"
            >
              <q-list>
                <q-item clickable v-close-popup>
                  <q-item-section>
                    <q-item-label>Photos</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item clickable v-close-popup>
                  <q-item-section>
                    <q-item-label>Videos</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item clickable v-close-popup>
                  <q-item-section>
                    <q-item-label>Articles</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-btn-dropdown>
            <q-btn-dropdown
              rounded
              outline
              color="lessons"
              label="Ascending"
              icon="fas fa-arrow-up fa-xs"
            >
              <q-list>
                <q-item clickable v-close-popup>
                  <q-item-section>
                    <q-item-label>Photos</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item clickable v-close-popup>
                  <q-item-section>
                    <q-item-label>Videos</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item clickable v-close-popup>
                  <q-item-section>
                    <q-item-label>Articles</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-btn-dropdown>
          </q-btn-group>
        </div>
      </template>

      <template v-slot:search>
        <div class="row q-mb-sm">
          <span class="text-lessons text-subtitle2">
            <q-icon name="fas fa-search q-mr-sm" />Search by
          </span>
        </div>
        <div class="row q-mb-sm">
          <q-input
            rounded
            outlined
            v-model="searchName"
            dense
            class="full-width"
            color="lessons"
            hint="Name"
          />
        </div>
        <div class="row q-mb-sm">
          <q-input
            rounded
            outlined
            v-model="searchDescription"
            dense
            class="full-width"
            color="lessons"
            hint="Description"
          />
        </div>
        <div class="row q-mb-sm">
          <q-input
            rounded
            outlined
            v-model="searchCategory"
            dense
            class="full-width"
            color="lessons"
            hint="Category"
          />
        </div>
      </template>

      <template v-slot:tabsContent>
        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="created">
            <!-- Lessons tabs -->
            <div v-if="lessons && lessons.length > 0">
              <div
                class="row q-ma-sm"
                v-for="(lesson, index) in lessons"
                :key="lesson.id"
              >
                <lesson-card
                  :lesson-data="lesson"
                  :card-class="getCardClass(index)"
                  button-class="course-card-entry"
                  @edit="onEditClick(lesson.id)"
                  @delete="onDeleteClick(lesson)"
                ></lesson-card>
              </div>
            </div>
            <div
              class="row q-ma-sm"
              v-for="index in cardsPerPage - lessons.length >= 0
                ? cardsPerPage - lessons.length
                : 0"
              :key="index"
            >
              <lesson-card :is-empty="true"> </lesson-card>
            </div>

            <!-- Pagination -->
            <div class="row q-ma-lg flex flex-center">
              <q-pagination
                v-model="currentPage"
                :max="totalPages"
                color="lessons"
                @input="onPageChange"
              ></q-pagination>
            </div>

            <!-- Add new lesson -->
            <div class="row q-ma-sm">
              <empty-lesson-card
                class="cursor-pointer"
                color="courses"
                @click.native="onAddLessonClick()"
              ></empty-lesson-card>
            </div>
          </q-tab-panel>
          <q-tab-panel name="attended">
            With so much content to display at once, and often so little screen
            real-estate, Cards have fast become the design pattern of choice for
            many companies, including the likes of Google and Twitter.
          </q-tab-panel>
        </q-tab-panels>
      </template>
    </search-component>

    <!-- Delete dialog -->
    <q-dialog v-model="isDeleteDialogVisible">
      <q-card class="bg-negative text-white">
        <q-card-section>
          <div class="text-h6">Watch out!</div>
        </q-card-section>

        <q-card-section>
          You are about to delete your lesson. After deleted, it cannot be
          brought back. Are you sure?
        </q-card-section>

        <q-card-actions align="right">
          <q-btn
            flat
            label="I changed my mind!"
            color="white"
            @click="deleteModalHandler(false)"
            v-close-popup
          />
          <q-btn
            flat
            label="Yeah, I'm sure!"
            color="white"
            @click="deleteModalHandler(true)"
            v-close-popup
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import LessonCard from "../../../components/main/LessonCard";
import EmptyLessonCard from "../../../components/main/AddLessonCard";
import SearchComponent from "../../../components/main/SearchComponent";
import { ServiceFactory } from "../../../services/ServiceFactory";
const LessonsService = ServiceFactory.get("lessons");

export default {
  name: "LessonList",
  components: { EmptyLessonCard, LessonCard, SearchComponent },
  data() {
    return {
      tab: "created",
      currentPage: 1,
      totalPages: 1,
      searchName: "",
      searchDescription: "",
      searchCategory: "",
      lessons: [],
      cardsPerPage: 4,
      isDeleteDialogVisible: false,
      resolveDeleteDialog: null
    };
  },
  created() {
    this.getAllCreatedLessons(this.currentPage);
  },
  methods: {
    getCardClass(index) {
      return index % 2 === 0
        ? "lesson-card-entry-light"
        : "lesson-card-entry-dark";
    },
    getAllCreatedLessons(pageIndex) {
      LessonsService.search(pageIndex).then(response => {
        this.lessons = response.data.searchResults;
        this.totalPages = response.data.totalPages;
      });
    },
    onPageChange() {
      this.getAllLessons(this.currentPage);
    },
    onAddLessonClick() {
      this.$router.push("lessons/add");
    },
    onEditClick(id) {
      this.$router.push(`lessons/edit/${id}`);
    },
    onDeleteClick(lesson) {
      this.isDeleteDialogVisible = true;

      this.resolveDeleteDialog = () => {
        LessonsService.deleteLesson(lesson).then(() => {
          this.$q.notify({
            color: "positive",
            icon: "fa fas-check",
            message: "Lesson has been deleted!"
          });
          this.getAllLessons(this.currentPage);
        });
      };
    },
    deleteModalHandler(shouldDelete) {
      if (shouldDelete) {
        this.resolveDeleteDialog();
      }

      this.isDeleteDialogVisible = false;
    }
  }
};
</script>

<style scoped lang="stylus">
@import '../../../css/quasar.variables.styl';

.lesson-card-entry {
  color: $secondary;
}

>>> .lesson-card-entry-light {
  @extend .lesson-card-entry;
  background-color: $lessons-item-light;
}

>>> .lesson-card-entry-dark {
  @extend .lesson-card-entry;
  background-color: $lessons-item-dark;
}
</style>
