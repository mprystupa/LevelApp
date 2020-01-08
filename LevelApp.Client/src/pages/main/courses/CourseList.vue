<template>
  <search-component color="courses">
    <template v-slot:title>
      <div class="row full-height">
        <div class="text-courses q-pa-md">
          <span class="text-h4">Courses</span>
        </div>
      </div>
    </template>

    <template v-slot:tabs>
      <q-tabs align="right" no-caps shrink v-model="tab" class="text-courses">
        <q-tab
          :name="coursesTabs.created()"
          icon="fas fa-user-edit"
          :label="coursesTabs.created()"
        />
        <q-tab
          :name="coursesTabs.attended()"
          icon="fas fa-chalkboard-teacher"
          :label="coursesTabs.attended()"
        />
        <q-tab
          :name="coursesTabs.favourite()"
          icon="fas fa-star"
          :label="coursesTabs.favourite()"
        />
        <q-tab
          :name="coursesTabs.popular()"
          icon="fas fa-fire-alt"
          :label="coursesTabs.popular()"
        />
        <q-tab
          :name="coursesTabs.new()"
          icon="fas fa-calendar-plus"
          :label="coursesTabs.new()"
        />
      </q-tabs>
    </template>

    <template v-slot:filters>
      <div class="row q-mb-sm">
        <span class="text-courses text-subtitle2">
          <q-icon name="fas fa-filter q-mr-sm" />Filter by
        </span>
      </div>
      <div class="row q-mb-xl">
        <q-btn-group flat spread class="full-width">
          <q-btn-dropdown
            rounded
            outline
            color="courses"
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
            color="courses"
            label="Ascending"
            icon="fas fa-arrow-up"
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
        <span class="text-courses text-subtitle2">
          <q-icon name="fas fa-search q-mr-sm" />Search by
        </span>
      </div>
      <div class="row q-mb-sm">
        <q-input
          rounded
          outlined
          v-model="searchData.searchName"
          dense
          class="full-width"
          color="courses"
          hint="Name"
        />
      </div>
      <div class="row q-mb-sm">
        <q-input
          rounded
          outlined
          v-model="searchData.searchDescription"
          dense
          class="full-width"
          color="courses"
          hint="Description"
        />
      </div>
      <div class="row q-mb-sm">
        <q-input
          rounded
          outlined
          v-model="searchData.searchCategory"
          dense
          class="full-width"
          color="courses"
          hint="Category"
        />
      </div>
    </template>

    <template v-slot:tabsContent>
      <q-tab-panels v-model="tab" animated>
        <q-tab-panel :name="coursesTabs.created()">
          <!-- Courses tabs -->
          <course-list-component
            :courses="courses"
            :current-page="searchData.currentPage"
            :cards-per-page="searchData.cardsPerPage"
            :total-pages="totalPages"
          />

          <!-- Add new course -->
          <div class="row q-ma-md">
            <empty-course-card
              class="cursor-pointer"
              @click.native="onAddNewCourseClick"
              color="courses"
            />
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
</template>

<script>
import EmptyCourseCard from "../../../components/main/courses/AddCourseCard";
import SearchComponent from "../../../components/main/SearchComponent";
import CourseListComponent from "../../../components/main/courses/CourseListComponent";

import { ServiceFactory } from "../../../services/ServiceFactory";
const CoursesService = ServiceFactory.get("courses");

const coursesTabs = {
  search: () => "Search",
  created: () => "Created",
  favourite: () => "Favourite",
  attended: () => "Attended",
  popular: () => "Popular",
  new: () => "New"
};

export default {
  name: "CourseList",
  components: {
    CourseListComponent,
    SearchComponent,
    EmptyCourseCard
  },
  data() {
    return {
      coursesTabs: coursesTabs,
      tab: coursesTabs.created(),
      searchData: {
        currentPage: 1,
        cardsPerPage: 3,
        searchName: "",
        searchDescription: "",
        searchCategory: ""
      },
      totalPages: 1,
      courses: []
    };
  },
  created() {
    this.getCourses();
  },
  methods: {
    getCourses() {
      this.courses = [];

      switch (this.tab) {
        case coursesTabs.search():
          this.getAllCourses();
          break;

        case coursesTabs.created():
          this.getAllCreatedCourses();
          break;
      }
    },
    getAllCreatedCourses() {
      CoursesService.searchCreated(this.searchData).then(response => {
        this.courses = response.data.searchResults;
        this.totalPages = response.data.totalPages;
      });
    },
    getAllCourses() {
      CoursesService.searchAll(this.searchData).then(response => {
        this.courses = response.data.searchResults;
        this.totalPages = response.data.totalPages;
      });
    },
    getCardClass(index) {
      return index % 2 === 0
        ? "course-card-entry-light"
        : "course-card-entry-dark";
    },
    onAddNewCourseClick() {
      this.$router.push("courses/add");
    }
  }
};
</script>

<style lang="stylus"></style>
