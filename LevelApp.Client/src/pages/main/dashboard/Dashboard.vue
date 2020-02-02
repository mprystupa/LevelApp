<template>
  <div>
    <div class="row full-width">
      <div class="col-9 q-pa-lg">
        <!-- Continue row -->
        <div class="row full-width">
          <div class="col dashboard-box q-pa-lg q-ma-md bg-accent">
            <div class="text-h4 full-width q-mb-md text-white">
              Continue where you left off...
            </div>

            <div class="row full-width">
              <div class="hex-icon"></div>
              <div class="col">
                <div class="text-h6 full-width text-blue-2">
                  Test course
                </div>
                <div class="text-subtitle2 full-width text-blue-2">
                  Finished 4/20
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Courses and history row -->
        <div class="row full-width" style="height: 500px;">
          <div class="col-4 text-align-center">
            <div class="dashboard-box full-height bg-white q-pa-lg q-ma-md">
              <div class="text-h4">
                Your courses
              </div>

              <q-separator class="q-my-md" />
            </div>
          </div>

          <div class="col-8">
            <div class="dashboard-box full-height bg-green-1 q-pa-lg q-ma-md">
              <q-timeline color="secondary">
                <q-timeline-entry
                  title="Event Title"
                  subtitle="February 22, 1986"
                >
                  <div>
                    Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                    sed do eiusmod tempor incididunt ut labore et dolore magna
                    aliqua. Ut enim ad minim veniam, quis nostrud exercitation
                    ullamco laboris nisi ut aliquip ex ea commodo consequat.
                    Duis aute irure dolor in reprehenderit in voluptate velit
                    esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
                    occaecat cupidatat non proident, sunt in culpa qui officia
                    deserunt mollit anim id est laborum.
                  </div>
                </q-timeline-entry>

                <q-timeline-entry
                  title="Event Title"
                  subtitle="February 21, 1986"
                  icon="delete"
                >
                  <div>
                    Lorem ipsum dolor sit amet, consectetur adipisicing elit,
                    sed do eiusmod tempor incididunt ut labore et dolore magna
                    aliqua. Ut enim ad minim veniam, quis nostrud exercitation
                    ullamco laboris nisi ut aliquip ex ea commodo consequat.
                    Duis aute irure dolor in reprehenderit in voluptate velit
                    esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
                    occaecat cupidatat non proident, sunt in culpa qui officia
                    deserunt mollit anim id est laborum.
                  </div>
                </q-timeline-entry>
              </q-timeline>
            </div>
          </div>
        </div>
      </div>

      <q-separator vertical class="q-my-md q-mx-lg bg-grey-7" />

      <div class="col-3 q-pa-lg"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Dashboard"
};
</script>

<style scoped lang="scss">
.dashboard-box {
  border-radius: 5px;
  -webkit-box-shadow: 0px 7px 12px 1px rgba(0, 0, 0, 0.3);
  -moz-box-shadow: 0px 7px 12px 1px rgba(0, 0, 0, 0.3);
  box-shadow: 0px 7px 12px 1px rgba(0, 0, 0, 0.3);
}
</style>
