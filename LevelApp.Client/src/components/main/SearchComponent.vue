<template>
  <q-card>
    <!-- Card header -->
    <div class="row flex justify-start full-width">
      <!-- Title -->
      <div class="col-6">
        <slot name="title"></slot>
      </div>

      <!-- Tabs -->
      <div class="col-6">
        <slot name="tabs"></slot>
      </div>
    </div>

    <!-- Header separator -->
    <q-separator :color="color" />

    <!-- Main card content -->
    <div class="row full-height full-width">
      <!-- Filters and search -->
      <div class="col-3 q-pa-lg">
        <!-- Filters -->
        <slot name="filters"></slot>

        <!-- Search -->
        <slot name="search"></slot>
      </div>

      <!-- Search results -->
      <div class="col-9 q-pa-lg">
        <slot name="tabsContent"></slot>
      </div>
    </div>
  </q-card>
</template>

<script>
export default {
  props: ["color"],
  name: "SearchComponent"
};
</script>

<style scoped></style>
