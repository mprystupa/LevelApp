<template>
  <div>
    <q-layout view="hHh LpR fFf">
      <q-header reveal elevated class="bg-primary text-white">
        <q-toolbar class="pt-1 pb-1 toolbar">
          <div class="col-4">
            <q-input
              v-if="showNotImplementedFeatures"
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
            <q-btn-dropdown rounded flat icon="fas fa-cogs fa-sm">
              <q-list>
                <q-item clickable v-close-popup @click="onLogoutClick">
                  <q-item-section>
                    <q-item-label>Logout</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-btn-dropdown>
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
                <div class="menu-course-icon">
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

            <q-item v-if="showNotImplementedFeatures" clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-medal" />
              </q-item-section>

              <q-item-section>
                Achievements
              </q-item-section>
            </q-item>

            <q-separator v-if="showNotImplementedFeatures" />

            <q-item v-if="showNotImplementedFeatures" clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-trophy" />
              </q-item-section>

              <q-item-section>
                Leaderboards
              </q-item-section>
            </q-item>

            <q-item v-if="showNotImplementedFeatures" clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="send" />
              </q-item-section>

              <q-item-section>
                Leagues
              </q-item-section>
            </q-item>

            <q-item v-if="showNotImplementedFeatures" clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="fas fa-chart-bar" />
              </q-item-section>

              <q-item-section>
                Stats
              </q-item-section>
            </q-item>

            <q-separator v-if="showNotImplementedFeatures" />

            <q-item v-if="showNotImplementedFeatures" clickable v-ripple>
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
import { showNotImplementedFeatures } from '../helpers/globalSettings';

export default {
  components: { Background },
  data() {
    return {
      showNotImplementedFeatures: showNotImplementedFeatures,
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
    },
    onLogoutClick() {
      this.$router.push("/");
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

.menu-course-icon {
  width: 24px;
  height 24px;
  display: inline-block;
  fill: black;
}

.q-router-link--active .menu-course-icon {
  fill: $primary;
}
</style>
