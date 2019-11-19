<template>
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
        <q-tab name="completed" icon="fas fa-check-square" label="Completed" />
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
          <q-btn-dropdown rounded outline color="lessons" label="Name" icon="fas fa-sort-alpha-up">
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
          <div class="row q-ma-sm" v-for="index in [1, 2, 3, 4, 5]" :key="index">
            <lesson-card
              :lesson-data="{
                title: `Test course ${index}`,
                description: `Description of test course ${index}`
              }"
              :card-class="getCardClass(index)"
              button-class="course-card-entry"
            ></lesson-card>
          </div>

          <!-- Pagination -->
          <div class="row q-ma-lg flex flex-center">
            <q-pagination v-model="currentPage" :max="5" color="lessons"></q-pagination>
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
</template>

<script>
import LessonCard from "../../../components/main/LessonCard";
import EmptyLessonCard from "../../../components/main/EmptyLessonCard";
import SearchComponent from "../../../components/main/SearchComponent";

export default {
  name: "LessonList",
  components: { EmptyLessonCard, LessonCard, SearchComponent },
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
        ? "lesson-card-entry-light"
        : "lesson-card-entry-dark";
    },
    onAddLessonClick() {
      this.$router.push("lessons/add");
    }
  }
};
</script>

<style scoped lang="stylus">
@import '../../../css/quasar.variables.styl';

.lesson-card-entry {
  color: $secondary;
}

.lesson-card-entry-light {
  @extend .lesson-card-entry;
  background-color: $lessons-item-light;
}

.lesson-card-entry-dark {
  @extend .lesson-card-entry;
  background-color: $lessons-item-dark;
}
</style>
