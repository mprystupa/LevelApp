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
        <q-tab name="created" icon="fas fa-user-edit" label="Created" />
        <q-tab name="attended" icon="fas fa-chalkboard-teacher" label="Attended" />
        <q-tab name="favourite" icon="fas fa-star" label="Favourite" />
        <q-tab name="popular" icon="fas fa-fire-alt" label="Popular" />
        <q-tab name="new" icon="fas fa-calendar-plus" label="New" />
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
          <q-btn-dropdown rounded outline color="courses" label="Name" icon="fas fa-sort-alpha-up">
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
          <q-btn-dropdown rounded outline color="courses" label="Ascending" icon="fas fa-arrow-up">
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
          v-model="searchName"
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
          v-model="searchDescription"
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
          v-model="searchCategory"
          dense
          class="full-width"
          color="courses"
          hint="Category"
        />
      </div>
    </template>

    <template v-slot:tabsContent>
      <q-tab-panels v-model="tab" animated>
        <q-tab-panel name="created">
          <!-- Courses tabs -->
          <div class="row q-ma-sm" v-for="index in [1, 2, 3]" :key="index">
            <course-card
              :course-data="{
                title: `Test course ${index}`,
                description: `Description of test course ${index}`
              }"
              :card-class="getCardClass(index)"
              button-class="course-card-entry"
            ></course-card>
          </div>

          <!-- Pagination -->
          <div class="row q-ma-lg flex flex-center">
            <q-pagination v-model="currentPage" :max="5" color="courses"></q-pagination>
          </div>

          <!-- Add new course -->
          <div class="row q-ma-sm">
            <empty-course-card class="cursor-pointer" color="courses"></empty-course-card>
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
import CourseCard from "../../../components/main/courses/CourseCard";
import EmptyCourseCard from "../../../components/main/courses/AddCourseCard";
import SearchComponent from "../../../components/main/SearchComponent";
export default {
  name: "CourseList",
  components: { SearchComponent, EmptyCourseCard, CourseCard },
  data() {
    return {
      tab: "created",
      currentPage: 1,
      searchName: "",
      searchDescription: "",
      searchCategory: ""
    };
  },
  methods: {
    getCardClass(index) {
      return index % 2 === 0
        ? "course-card-entry-light"
        : "course-card-entry-dark";
    }
  }
};
</script>

<style lang="stylus">
@import '../../../css/quasar.variables.styl';

.course-card-entry {
  color: $secondary;
}

.course-card-entry-light {
  @extend .course-card-entry;
  background-color: $courses-item-light;
}

.course-card-entry-dark {
  @extend .course-card-entry;
  background-color: $courses-item-dark;
}
</style>
