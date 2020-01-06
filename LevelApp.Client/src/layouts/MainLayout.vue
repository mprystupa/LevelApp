<template>
  <div>
    <q-layout view="hHh LpR fFf">
      <q-header reveal elevated class="bg-primary text-white">
        <q-toolbar class="pt-1 pb-1 toolbar">
          <div class="col-4">
            <q-input
              rounded
              dark
              dense
              standout
              v-model="searchText"
              class="search-bar"
              placeholder="Search for courses, categories etc."
            >
              <template v-slot:prepend>
                <q-icon
                  v-if="searchText === ''"
                  name="fas fa-search"
                  size="1.2rem"
                />
                <q-icon
                  v-else
                  name="fas fa-times"
                  size="1.2rem"
                  class="cursor-pointer"
                  @click="searchText = ''"
                />
              </template>
            </q-input>
          </div>
          <div class="col-4 flex justify-center">
            <div class="">
              <img
                class="toolbar-logo"
                src="../assets/levelapp-logo-horizontal.svg"
              />
            </div>
          </div>
          <div class="col-4 flex justify-end">
            <q-btn flat round>
              <i class="fas fa-cogs fa-lg"></i>
            </q-btn>
          </div>
        </q-toolbar>
      </q-header>

      <q-drawer
        v-model="drawer"
        show-if-above
        :mini="miniState"
        @mouseover="miniState = false"
        @mouseout="miniState = true"
        :width="250"
        :breakpoint="500"
        class="drawer-transparent"
      >
        <q-scroll-area class="fit">
          <q-list padding>
            <!-- Home -->
            <q-item to="/main" exact clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-home" />
              </q-item-section>

              <q-item-section>
                Home
              </q-item-section>
            </q-item>

            <q-separator />

            <!-- Courses -->
            <q-item to="/main/courses" clickable v-ripple>
              <q-item-section avatar>
                <img
                  style="width: 24px; height: 24px;"
                  src="../assets/main/course-icon.svg"
                />
              </q-item-section>

              <q-item-section>
                Courses
              </q-item-section>
            </q-item>

            <!-- Lessons -->
            <q-item to="/main/lessons" clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-book" />
              </q-item-section>

              <q-item-section>
                Lessons
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-medal" />
              </q-item-section>

              <q-item-section>
                Achievements
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-trophy" />
              </q-item-section>

              <q-item-section>
                Leaderboards
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="send" />
              </q-item-section>

              <q-item-section>
                Leagues
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-chart-bar" />
              </q-item-section>

              <q-item-section>
                Stats
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-question" />
              </q-item-section>

              <q-item-section>
                Help
              </q-item-section>
            </q-item>
          </q-list>
        </q-scroll-area>
      </q-drawer>

      <q-page-container>
        <q-page>
          <transition
            appear
            enter-active-class="animated fadeIn"
            leave-active-class="animated fadeOut"
            mode="out-in"
          >
            <router-view class="q-ma-lg" />
          </transition>
        </q-page>
      </q-page-container>
    </q-layout>
    <!-- Background -->
    <background background-class="bg-main" />
  </div>
</template>

<script>
import Background from "../components/Background";
export default {
  components: { Background },
  data() {
    return {
      drawer: false,
      miniState: true,
      searchText: ""
    };
  },

  methods: {
    drawerClick(e) {
      // if in "mini" state and user
      // click on drawer, we switch it to "normal" mode
      if (this.miniState) {
        this.miniState = false;

        // notice we have registered an event with capture flag;
        // we need to stop further propagation as this click is
        // intended for switching drawer to "normal" mode only
        e.stopPropagation();
      }
    }
  }
};
</script>

<style lang="stylus">
.bg-main {
  background: rgb(189,237,199);
  background: linear-gradient(130deg, rgba(189,237,199,1) 0%, rgba(128,233,227,1) 100%);
}

.menu-list .q-item {
  border-radius: 0 32px 32px 0
}

.drawer-transparent {
  .q-drawer {
    background-color: rgba(255, 255, 255, 0.4);
    border: none;
  }
}

.toolbar {
  justify-content: space-between;
}

.toolbar-logo {
  height: 35px;
}

.search-bar {
  width: 300px;
}

.input-icon {
  transition: color 0.36s;
}
.q-field--focused {
  & .input-icon {
    color: white;
  }
}
</style>
