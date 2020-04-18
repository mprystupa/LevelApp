<template>
  <div>
    <div class="row full-width">
      <div class="col-9 q-pa-lg">
        <!-- Continue row -->
        <div class="row full-width">
          <div class="col dashboard-box q-pa-lg q-ma-md bg-accent">
            <div class="text-h4 full-width q-mb-md text-white">
              No started courses yet
            </div>

            <div class="row full-width">
              <div class="hex-icon"></div>
              <div class="col">
                <div class="text-h6 full-width q-mb-md text-blue-2">
                  Take a look at our courses
                </div>

                <q-btn
                  outline
                  rounded
                  color="white"
                  label="Go to courses"
                />
              </div>
            </div>
          </div>
        </div>

        <!-- Courses and history row -->
        <div class="row full-width" style="height: 500px;">
          <div class="col-4 text-align-center">
            <div class="dashboard-box full-height relative-position bg-white q-pa-lg q-ma-md">
              <div class="text-h4">
                Your courses
              </div>

              <q-separator class="q-my-md" />

              <div class="text-h6 full-width">
                No created courses yet
              </div>

              <div class="absolute-bottom q-pb-lg">
                <q-btn
                  outline
                  rounded
                  color="black"
                  label="Add new course"
                />
              </div>
            </div>
          </div>

          <div class="col-8">
            <div class="dashboard-box full-height bg-green-1 q-pa-lg q-ma-md">
              <q-timeline color="secondary">
                <q-timeline-entry
                  title="Signed up"
                  subtitle="January 22, 2020"
                >
                  <div>
                    Created an account on LevelApp.
                  </div>
                </q-timeline-entry>
              </q-timeline>
            </div>
          </div>
        </div>
      </div>

      <q-separator vertical class="q-my-md q-mx-lg bg-grey-7" />

      <div class="col-2 flex flex-center q-pa-lg">
        <div class="row flex-center">
          <div class="relative-position" style="width: 200px; height: 200px;">
            <div class="full-width full-height clip-hex bg-secondary" />
            <i class="absolute-center fas fa-user fa-8x text-white"></i>
          </div>

          <div class="row q-pt-lg">
            <span class="text-h4">Użytkownik</span>
          </div>

          <div class="row q-pt-lg">
            <q-btn
              rounded
              color="black"
              size="lg"
              label="Log out"
            />
          </div>
        </div>
      </div>
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
