<template>
  <q-card>
    <!-- Card header -->
    <div class="row flex justify-start full-width">
      <!-- Title -->
      <div class="col-12 col-sm-6">
        <slot name="title"></slot>
      </div>

      <!-- Tabs -->
      <div class="col-12 col-sm-6">
        <slot name="tabs"></slot>
      </div>
    </div>

    <!-- Header separator -->
    <q-separator :color="color" />

    <!-- Main card content -->
    <div class="row full-height full-width">
      <!-- Filters and search -->
      <div class="col-12 col-lg-3 q-pt-lg q-px-lg q-pa-lg-lg">
        <!-- No drawer -->
        <div class="gt-md">
          <!-- Filters -->
          <div class="col-12 col-md-6">
            <slot name="filters"></slot>
          </div>

          <!-- Search -->
          <div class="col-12 col-md-6">
            <slot name="search"></slot>
          </div>
        </div>

        <!-- Drawer -->
        <div class="lt-lg">
          <q-expansion-item class="q-mb-lg">
            <template v-slot:header>
              <q-item-section avatar>
                <q-icon name="fas fa-book-open" :color="color" />
              </q-item-section>

              <q-item-section :class="`text-${color}`" class="text-h6">Filters and search</q-item-section>
            </template>
            <q-card>
              <q-card-section>
                <!-- Filters -->
                <div class="col-12 col-md-6">
                  <slot name="filters"></slot>
                </div>

                <!-- Search -->
                <div class="col-12 col-md-6">
                  <slot name="search"></slot>
                </div>
              </q-card-section>
            </q-card>
          </q-expansion-item>
        </div>
      </div>

      <!-- Search results -->
      <div class="col-12 col-lg-9 q-pa-lg">
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

<style lang="stylus"></style>
