<template>
  <q-layout view="lHh Lpr lFf">
    <q-page-container>
      <router-view />
    </q-page-container>

    <!-- Header -->
    <q-page-scroller expand position="top" :scroll-offset="700" :offset="[0, -50]">
      <q-header elevated>
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
    <q-page-scroller position="bottom-right" :scroll-offset="700" :offset="[18, 18]">
      <q-btn fab icon="keyboard_arrow_up" color="accent" />
    </q-page-scroller>
  </q-layout>
</template>

<script>
import { openURL } from 'quasar'

export default {
  name: 'HomeLayout',
  data () {
    return {
      leftDrawerOpen: false
    }
  },
  methods: {
    openURL
  }
}
</script>

<style>
</style>
