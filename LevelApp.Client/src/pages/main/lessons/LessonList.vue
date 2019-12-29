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
        <q-tabs
          align="right"
          no-caps
          shrink
          v-model="tab"
          @input="getLessons"
          class="text-lessons"
        >
          <q-tab
            :name="lessonCards.search()"
            icon="fas fa-search"
            :label="lessonCards.search()"
          />
          <q-tab
            :name="lessonCards.created()"
            icon="fas fa-user-edit"
            :label="lessonCards.created()"
          />
          <q-tab
            :name="lessonCards.completed()"
            icon="fas fa-check-square"
            :label="lessonCards.completed()"
          />
          <q-tab
            :name="lessonCards.awaiting()"
            icon="fas fa-clock"
            :label="lessonCards.awaiting()"
          />
          <q-tab
            :name="lessonCards.favourite()"
            icon="fas fa-star"
            :label="lessonCards.favourite()"
          />
        </q-tabs>
      </template>

      <template v-slot:filters>
        <div class="row q-mb-sm">
          <span class="text-lessons text-subtitle2">
            <q-icon name="fas fa-sort-amount-up-alt q-mr-sm" />Sort by
          </span>
        </div>
        <div class="row q-mb-xl">
          <q-btn-group flat spread class="full-width">
            <q-btn-dropdown
              rounded
              outline
              color="lessons"
              :label="lessonOrderBySelected.text"
              :icon="lessonOrderBySelected.icon"
            >
              <q-list>
                <q-item
                  v-for="(orderBy, index) in lessonOrderBy"
                  clickable
                  v-close-popup
                  @click="onOrderByClick(orderBy)"
                  :key="index"
                >
                  <q-item-section>
                    <q-item-label>{{ orderBy.text }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-btn-dropdown>
            <q-btn-dropdown
              rounded
              outline
              color="lessons"
              :label="lessonOrderByDirectionSelected.text"
              :icon="lessonOrderByDirectionSelected.icon"
            >
              <q-list>
                <q-item
                  v-for="(orderByDirection, index) in lessonOrderByDirection"
                  clickable
                  v-close-popup
                  @click="onOrderByDirectionClick(orderByDirection)"
                  :key="index"
                >
                  <q-item-section>
                    <q-item-label>{{ orderByDirection.text }}</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-btn-dropdown>
          </q-btn-group>
        </div>
      </template>

      <template v-if="tab !== lessonCards.search()" v-slot:search>
        <div class="row q-mb-sm">
          <span class="text-lessons text-subtitle2">
            <q-icon name="fas fa-search q-mr-sm" />Search by
          </span>
        </div>
        <div class="row q-mb-sm">
          <q-input
            rounded
            outlined
            clearable
            debounce="500"
            v-model="searchData.searchName"
            dense
            class="full-width"
            color="lessons"
            hint="Name"
            @input="getLessons"
          />
        </div>
        <div class="row q-mb-sm">
          <q-input
            rounded
            outlined
            clearable
            debounce="500"
            v-model="searchData.searchDescription"
            dense
            class="full-width"
            color="lessons"
            hint="Description"
            @input="getLessons"
          />
        </div>
        <div class="row q-mb-sm">
          <q-input
            rounded
            outlined
            clearable
            debounce="500"
            v-model="searchData.searchCategory"
            dense
            class="full-width"
            color="lessons"
            hint="Category"
            @input="getLessons"
          />
        </div>
      </template>

      <template v-slot:tabsContent>
        <q-tab-panels v-model="tab" animated>
          <!-- Search tab -->
          <q-tab-panel :name="lessonCards.search()">
            <div class="row q-mb-lg">
              <div class="col-12">
                <q-input
                  rounded
                  debounce="500"
                  @input="getAllLessons"
                  standout="bg-lessons text-white"
                  v-model="searchData.searchLessonText"
                  class="full-width"
                  placeholder="Search for courses by name, description or author"
                >
                  <template v-slot:prepend>
                    <q-icon
                      v-if="searchData.searchLessonText === ''"
                      name="fas fa-search"
                      size="1.2rem"
                    />
                    <q-icon
                      v-else
                      name="fas fa-times"
                      size="1.2rem"
                      class="cursor-pointer"
                      @click="searchData.searchLessonText = ''"
                    />
                  </template>
                </q-input>
              </div>
            </div>

            <div class="row q-mb-lg">
              <q-separator inset color="lessons" />
            </div>

            <lessons-list-component
              :lessons="lessons"
              :cardsPerPage.sync="searchData.cardsPerPage"
              :currentPage.sync="searchData.currentPage"
              :totalPages="totalPages"
              @edit="onEditClick($event)"
              @delete="onDeleteClick($event)"
              @view="onViewClick($event)"
              @favourite="
                onFavouriteClick($event.lessonId, $event.favouriteLoaderHandler)
              "
              @pageChange="onPageChange"
            ></lessons-list-component>
          </q-tab-panel>

          <!-- Created tab -->
          <q-tab-panel :name="lessonCards.created()">
            <lessons-list-component
              :lessons="lessons"
              :cardsPerPage.sync="searchData.cardsPerPage"
              :currentPage.sync="searchData.currentPage"
              :totalPages="totalPages"
              @edit="onEditClick($event)"
              @delete="onDeleteClick($event)"
              @view="onViewClick($event)"
              @favourite="
                onFavouriteClick($event.lessonId, $event.favouriteLoaderHandler)
              "
              @pageChange="onPageChange"
            ></lessons-list-component>

            <!-- Add new lesson -->
            <div class="row q-ma-md">
              <add-lesson-card
                class="cursor-pointer"
                color="courses"
                @click.native="onAddLessonClick()"
              />
            </div>
          </q-tab-panel>

          <!-- Completed tab -->
          <q-tab-panel :name="lessonCards.completed()">
            <lessons-list-component
              :lessons="lessons"
              :cardsPerPage.sync="searchData.cardsPerPage"
              :currentPage.sync="searchData.currentPage"
              :totalPages="totalPages"
              @edit="onEditClick($event)"
              @delete="onDeleteClick($event)"
              @view="onViewClick($event)"
              @favourite="
                onFavouriteClick($event.lessonId, $event.favouriteLoaderHandler)
              "
              @pageChange="onPageChange"
            ></lessons-list-component>
          </q-tab-panel>

          <!-- Awaiting tab -->
          <q-tab-panel :name="lessonCards.awaiting()">
            <lessons-list-component
              :lessons="lessons"
              :cardsPerPage.sync="searchData.cardsPerPage"
              :currentPage.sync="searchData.currentPage"
              :totalPages="totalPages"
              @edit="onEditClick($event)"
              @delete="onDeleteClick($event)"
              @view="onViewClick($event)"
              @favourite="
                onFavouriteClick($event.lessonId, $event.favouriteLoaderHandler)
              "
              @pageChange="onPageChange"
            ></lessons-list-component>
          </q-tab-panel>

          <!-- Favourite tab -->
          <q-tab-panel :name="lessonCards.favourite()">
            <lessons-list-component
              :lessons="lessons"
              :cardsPerPage.sync="searchData.cardsPerPage"
              :currentPage.sync="searchData.currentPage"
              :totalPages="totalPages"
              @edit="onEditClick($event)"
              @delete="onDeleteClick($event)"
              @view="onViewClick($event)"
              @favourite="
                onFavouriteClick($event.lessonId, $event.favouriteLoaderHandler)
              "
              @pageChange="onPageChange"
            ></lessons-list-component>
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
import AddLessonCard from "../../../components/main/lessons/AddLessonCard";
import SearchComponent from "../../../components/main/SearchComponent";
import LessonsListComponent from "../../../components/main/lessons/LessonsListComponent";
import { ServiceFactory } from "../../../services/ServiceFactory";
const LessonsService = ServiceFactory.get("lessons");

const lessonTabs = {
  search: () => "Search",
  created: () => "Created",
  completed: () => "Completed",
  awaiting: () => "Awaiting",
  favourite: () => "Favourite"
};

const lessonOrderBy = [
  {
    text: "Name",
    parameter: "name",
    icon: "fas fa-sort-alpha-up"
  },
  {
    text: "Author",
    parameter: "author",
    icon: "fas fa-user-circle"
  }
];

const lessonOrderByDirection = [
  {
    text: "Ascending",
    parameter: "asc",
    icon: "fas fa-arrow-up fa-xs"
  },
  {
    text: "Descending",
    parameter: "desc",
    icon: "fas fa-arrow-down fa-xs"
  }
];

export default {
  name: "LessonList",
  components: {
    AddLessonCard,
    SearchComponent,
    LessonsListComponent
  },
  data() {
    return {
      lessonCards: lessonTabs,
      lessonOrderBy: lessonOrderBy,
      lessonOrderBySelected: lessonOrderBy[0],
      lessonOrderByDirection: lessonOrderByDirection,
      lessonOrderByDirectionSelected: lessonOrderByDirection[0],
      tab: lessonTabs.created(),
      searchData: {
        currentPage: 1,
        cardsPerPage: 4,
        searchName: "",
        searchDescription: "",
        searchCategory: "",
        searchLessonText: "",
        orderBy: lessonOrderBy[0].parameter,
        orderDirection: lessonOrderByDirection[0].parameter
      },
      totalPages: 1,
      lessons: [],
      isDeleteDialogVisible: false,
      resolveDeleteDialog: null
    };
  },
  created() {
    this.getLessons();
  },
  methods: {
    getCardClass(index) {
      return index % 2 === 0
        ? "lesson-card-entry-light"
        : "lesson-card-entry-dark";
    },
    getLessons() {
      this.lessons = [];

      switch (this.tab) {
        case lessonTabs.search():
          this.getAllLessons();
          break;

        case lessonTabs.created():
          this.getAllCreatedLessons();
          break;
      }
    },
    getAllCreatedLessons() {
      LessonsService.searchCreated(this.searchData).then(response => {
        this.lessons = response.data.searchResults;
        this.totalPages = response.data.totalPages;
      });
    },
    getAllLessons() {
      LessonsService.searchAll(this.searchData).then(response => {
        this.lessons = response.data.searchResults;
        this.totalPages = response.data.totalPages;
      });
    },
    onPageChange() {
      this.getAllCreatedLessons();
    },
    onAddLessonClick() {
      this.$router.push("lessons/add");
    },
    onEditClick(lessonId) {
      this.$router.push(`lessons/edit/${lessonId}`);
    },
    onViewClick(lessonId) {
      this.$router.push(`lessons/view/${lessonId}`);
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
    onFavouriteClick(lessonId, loadingHandler) {
      let lesson = this.lessons.find(x => x.id === lessonId);
      console.log(loadingHandler);

      if (lesson) {
        if (!lesson.isFavourite) {
          LessonsService.addFavouriteLesson(lessonId).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Lesson added to favourites!"
            });

            this.getAllLessons(this.currentPage);
            loadingHandler();
          });
        } else {
          LessonsService.removeFavouriteLesson(lessonId).then(() => {
            this.$q.notify({
              color: "positive",
              icon: "fa fas-check",
              message: "Lesson removed from favourites!"
            });

            this.getAllLessons(this.currentPage);
            loadingHandler();
          });
        }
      }
    },
    onOrderByClick(orderByItem) {
      this.lessonOrderBySelected = orderByItem;
      this.searchData.orderBy = orderByItem.parameter;

      this.getLessons();
    },
    onOrderByDirectionClick(orderByDirectionItem) {
      this.lessonOrderByDirectionSelected = orderByDirectionItem;
      this.searchData.orderDirection = orderByDirectionItem.parameter;

      this.getLessons();
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

<style scoped lang="stylus"></style>
