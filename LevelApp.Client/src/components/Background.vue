<template>
  <div class="full-width full-height">
    <div
      id="background"
      class="background bg-transition bg-hex-overlay fixed full-width full-height"
      :class="backgroundClass"
    ></div>
    <div class="bg-hex-overlay fixed full-width full-height"></div>
  </div>
</template>

<script>
export default {
  props: ["backgroundClass"],
  name: "Background"
};
</script>

<style scoped>
.bg-hex-overlay {
  background: url("../assets/landing-page/background.png");
  -webkit-animation: slide 20s linear infinite;
  z-index: -1;
}

@-webkit-keyframes slide {
  from {
    background-position: 0 0;
  }
  to {
    background-position: -400px -400px;
  }
}

.bg-transition {
  -webkit-transition: background-color 500ms linear;
  -ms-transition: background-color 500ms linear;
  transition: background-color 500ms linear;
}

.background {
  z-index: -2;
}
</style>
