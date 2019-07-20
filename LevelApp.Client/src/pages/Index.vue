<template>
  <q-page class="flex flex-center">
    <full-page ref="fullpage" :options="options" id="fullpage" class="full-width">
      <!-- Background -->
      <div id="background" class="background bg-transition fixed full-width full-height" :class="backgroundClass"></div>

      <div class="full-width section">
        <!-- Logo -->
        <div class="row justify-center" key="logo">
          <div>
            <img class="mb-1" alt="LevelApp logo" width="300px" src="~assets/levelapp-logo-full.svg">
          </div>
        </div>
        <!-- Text -->
        <div class="row justify-center text-align-center">
          <div>
            <q-separator color="white" spaced inset />
            <div class="text-h5 text-white text-weight-thin mt-1">
              Learn whatever you want, however you want, whenever you want!
            </div>
          </div>
        </div>
        <!-- Buttons -->
        <div class="row justify-center">
          <div class="mt-2">
            <q-btn color="white" class="mr-2 btn-fixed-width" size="lg" outline rounded label="Sign up" />
            <q-btn color="white" class="ml-2 btn-fixed-width" size="lg" outline rounded label="Log in" />
          </div>
        </div>
      </div>
      <div class="full-width section text-white text-align-center">
        <div class="row justify-center">
          <div class="text-h3 mb-1">Learning and teaching new things <strong>your way</strong></div>
          <div class="text-h5 text-weight-thin mr-1 ml-1 mb-1">LevelApp is all about you - whether you want to share your knowledge or let others teach you.
            With use of RPG skill trees and achievements, you will soon find out that exchange of knowledge was never so easy and efficient.</div>
          <div class="text-h5 text-weight-thin mr-1 ml-1 mb-2">Learn, progress, create and share - everything is in reach of your hand, absolutely free.
          </div>
        </div>
      </div>
      <div class="full-width section text-white text-align-center">
        <div class="row justify-center mt-2">
          <div class="col-6">
            <div class="text-h5">Learn</div>
          </div>
          <div class="col-6">
            <div class="text-h5">Create</div>
          </div>
        </div>
      </div>
    </full-page>
    <q-page-sticky v-if="isScrolled" position="bottom-right" :offset="[25, 25]">
      <q-btn fab icon="keyboard_arrow_up" color="primary" @click="$refs.fullpage.api.moveTo(1)" />
    </q-page-sticky>
  </q-page>
</template>

<script>
export default {
  name: 'PageIndex',
  sectionBackgroundClasses: {},

  data () {
    return {
      options: {
        navigation: true,
        onLeave: this.onLeave
      },
      backgroundClass: 'bg-primary',
      isScrolled: false
    }
  },

  mounted () {
    this.sectionBackgroundClasses = {
      '0': 'bg-primary',
      '1': 'bg-blue-4',
      '2': 'bg-purple-5'
    }
  },

  methods: {
    onLeave (origin, destination, direction) {
      this.backgroundClass = this.sectionBackgroundClasses[destination.index]

      this.isScrolled = destination.index !== 0
    }
  }
}
</script>

<style>
  .bg-transition {
    -webkit-transition: background-color 500ms linear;
    -ms-transition: background-color 500ms linear;
    transition: background-color 500ms linear;
  }

  .background {
    z-index: -1
  }

  .btn-fixed-width {
    width: 200px;
  }
</style>
