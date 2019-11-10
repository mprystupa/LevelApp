<template>
  <q-layout view="lHh Lpr lFf">
    <q-page-container>
      <!-- Background -->
      <background :background-class="backgroundClass" />
      <transition
        appear
        enter-active-class="animated fadeIn"
        leave-active-class="animated fadeOut"
        mode="out-in"
      >
        <router-view @backgroundClassChange="onBackgroundClassChange" />
      </transition>
    </q-page-container>

    <!-- Header -->
    <q-page-scroller
      expand
      position="top"
      :scroll-offset="700"
      :offset="[0, -50]"
    >
      <q-header class="bg-secondary" elevated>
        <q-toolbar>
          <q-toolbar-title class="text-center">
            <q-avatar class="">
              <img src="../statics/icons/icon-128x128.png" />
            </q-avatar>
            LevelApp
          </q-toolbar-title>
        </q-toolbar>
      </q-header>
    </q-page-scroller>

    <!-- Go back button -->
    <q-page-scroller
      position="bottom-right"
      :scroll-offset="0"
      :offset="[18, 18]"
    >
      <q-btn fab icon="keyboard_arrow_up" color="accent" />
    </q-page-scroller>
  </q-layout>
</template>

<script>
import { openURL } from "quasar";
import Background from "../components/Background";

export default {
  name: "LandingLayout",
  components: { Background },
  data() {
    return {
      backgroundClass: "bg-primary",
      leftDrawerOpen: false
    };
  },
  methods: {
    onBackgroundClassChange(value) {
      this.backgroundClass = value;
    },
    openURL
  }
};
</script>

<style scoped>
</style>
