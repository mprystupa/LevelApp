<template>
  <div id="q-app">
    <transition appear
                enter-active-class="animated fadeIn"
                leave-active-class="animated fadeOut">
      <router-view />
    </transition>
  </div>
</template>

<script>
export default {
  name: "App"
};
</script>

<style></style>
