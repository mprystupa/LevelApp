<template>
  <div>
    <search-component color="courses">
      <template v-slot:title>
        <div class="row full-height">
          <div class="text-courses q-pa-md">
            <div class="course-icon q-mr-md">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                focusable="false"
                viewBox="0 0 448 512"
              >
                <path
                  class="st0"
                  d="M430.7 292.7l-90.5-52.2c-9.4-5.4-20.9-5.4-30.3 0l-10.2 5.9c-6.4 3.7-14.6 1.8-18.6-4.4l-30.6-46.6c-4.4-6.7-2.3-15.7 4.7-19.7l12.7-7.3c7-4.1 11.4-11.6 11.4-19.7V72.9c0-8.1-4.3-15.6-11.4-19.7l-65.5-37.8c-7-4.1-15.7-4.1-22.7 0l-65.5 37.8c-7 4.1-11.4 11.6-11.4 19.7v75.6c0 8.1 4.3 15.6 11.4 19.7l0 0c7.5 4.3 9.7 14 5 21.2l-36.3 54.5c-4.9 7.3-14.6 9.6-22.2 5.2l0 0c-1.9-1.1-4-1.6-6.1-1.6s-4.2 0.6-6.1 1.6L6.1 273.5C2.3 275.6 0 279.7 0 284.1v48.6c0 4.4 2.3 8.4 6.1 10.6l42.1 24.3c1.9 1.1 4 1.6 6.1 1.6s4.2-0.5 6.1-1.6l42.1-24.3c3.8-2.2 6.1-6.2 6.1-10.6v-48.6c0-4.4-2.3-8.4-6.1-10.6h0c-8.5-4.9-11.2-16.1-5.7-24.3l32.8-49.2c6.1-9.2 18.4-12.1 28-6.6l21.9 12.6c7 4.1 15.7 4.1 22.7 0l15.9-9.1c6.4-3.7 14.6-1.8 18.7 4.4l30.6 46.6c4.4 6.7 2.3 15.7-4.7 19.8l-43.2 25c-9.4 5.4-15.1 15.4-15.1 26.2v104.5c0 10.8 5.8 20.8 15.1 26.2l90.5 52.2c9.4 5.4 20.9 5.4 30.3 0l90.5-52.2c9.4-5.4 15.1-15.4 15.1-26.2V318.9C445.8 308.1 440 298.1 430.7 292.7zM97.7 284.1v48.6c0 0.4-0.2 0.9-0.6 1.1l-42.1 24.3c-0.2 0.1-0.4 0.2-0.6 0.2s-0.4-0.1-0.6-0.2l-42.1-24.3c-0.4-0.2-0.6-0.6-0.6-1.1v-48.6c0-0.4 0.2-0.8 0.6-1.1l42.1-24.3c0.2-0.1 0.4-0.2 0.6-0.2 0.2 0 0.4 0.1 0.6 0.2l42.1 24.3C97.4 283.2 97.7 283.6 97.7 284.1z"
                />
              </svg>
            </div>
            <span class="text-h4">Courses</span>
          </div>
        </div>
      </template>

      <template v-slot:tabs>
        <q-tabs
          align="right"
          no-caps
          shrink
          v-model="tab"
          @input="getCourses"
          class="text-courses"
        >
          <q-tab
            :name="coursesTabs.search()"
            icon="fas fa-search"
            :label="coursesTabs.search()"
          />
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
            v-if="showNotImplementedFeatures"
            :name="coursesTabs.favourite()"
            icon="fas fa-star"
            :label="coursesTabs.favourite()"
          />
          <q-tab
            v-if="showNotImplementedFeatures"
            :name="coursesTabs.popular()"
            icon="fas fa-fire-alt"
            :label="coursesTabs.popular()"
          />
          <q-tab
            v-if="showNotImplementedFeatures"
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
          <!-- Search tab -->
          <q-tab-panel :name="coursesTabs.search()">
            <div class="row q-mb-lg">
              <div class="col-12">
                <q-input
                  rounded
                  debounce="500"
                  @input="getAllCourses"
                  standout="bg-courses text-white"
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
              <q-separator inset color="courses" />
            </div>

            <course-list-component
              :courses="courses"
              :cards-per-page="searchData.cardsPerPage"
              :total-pages="totalPages"
              @edit="onEditClick($event)"
              @delete="onDeleteClick($event)"
              @attend="onAttendClick($event)"
              @continue="onContinueClick($event)"
              @pageChange="onPageChange($event)"
            />
          </q-tab-panel>

          <!-- Created tab -->
          <q-tab-panel :name="coursesTabs.created()">
            <course-list-component
              :courses="courses"
              :cards-per-page="searchData.cardsPerPage"
              :total-pages="totalPages"
              @edit="onEditClick($event)"
              @delete="onDeleteClick($event)"
              @attend="onAttendClick($event)"
              @continue="onContinueClick($event)"
              @pageChange="onPageChange($event)"
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

    <!-- Delete dialog -->
    <q-dialog v-model="isDeleteDialogVisible">
      <q-card class="bg-negative text-white">
        <q-card-section>
          <div class="text-h6">Watch out!</div>
        </q-card-section>

        <q-card-section>
          You are about to delete your course. After deleted, it cannot be
          brought back. Lessons assigned to the course will be untouched. Are
          you sure?
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
import EmptyCourseCard from "../../../components/main/courses/AddCourseCard";
import SearchComponent from "../../../components/main/SearchComponent";
import CourseListComponent from "../../../components/main/courses/CourseListComponent";

import { ServiceFactory } from "../../../services/ServiceFactory";
import { showNotImplementedFeatures } from "../../../helpers/globalSettings";
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
      showNotImplementedFeatures: showNotImplementedFeatures,
      coursesTabs: coursesTabs,
      tab: coursesTabs.created(),
      searchData: {
        currentPage: 1,
        cardsPerPage: 3,
        searchName: "",
        searchDescription: "",
        searchCategory: "",
        searchCourseText: ""
      },
      totalPages: 1,
      courses: [],
      isDeleteDialogVisible: false,
      resolveDeleteDialog: null
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
    },
    onEditClick(courseId) {
      this.$router.push(`courses/edit/${courseId}`);
    },
    onAttendClick(courseId) {
      CoursesService.addAttendingCourse(courseId).then(() => {
        this.$router.push(`courses/view/${courseId}`);
      });
    },
    onContinueClick(courseId) {
      this.$router.push(`courses/view/${courseId}`);
    },
    onDeleteClick(courseId) {
      this.isDeleteDialogVisible = true;

      this.resolveDeleteDialog = () => {
        CoursesService.deleteCourse(courseId).then(() => {
          this.$q.notify({
            color: "positive",
            icon: "fa fas-check",
            message: "Course has been deleted!"
          });
          this.getCourses();
        });
      };
    },
    onPageChange(currentPage) {
      this.searchData.currentPage = currentPage;
      this.getCourses();
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

<style lang="stylus">
.course-icon {
  width: 24px;
  display: inline-block;
  fill: $courses
}
</style>
