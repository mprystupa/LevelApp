<template>
  <q-dialog v-model="isVisibleModel">
    <q-card style="max-width: 900px; min-height: 500px;">
      <q-card-section class="p-0">
        <div class="row full-width">
          <!-- <div class="col-0 col-sm-4 flex flex-center bg-secondary">
            <div>
              <img
                class=""
                alt="LevelApp logo"
                width="100%"
                src="../../../assets/levelapp-logo-full.svg"
              />
            </div>

            <div
              class="text-subtitle1 text-white text-center flex-align-self-end p-2"
            >
              We're glad you want to join us!
            </div>
          </div> -->

          <div class="col-12 p-2">
            <div class="text-h4 pt-1 pb-1">
              Sign up
            </div>

            <!-- <q-separator class="mt-2 mb-2" />

            Login form
            <div class="p-1 pt-0 pb-0">
              <q-input
                class="mb-1"
                rounded
                outlined
                dense
                v-model="email"
                placeholder="E-mail address"
              />

              <q-input
                class="mb-1"
                rounded
                outlined
                dense
                type="password"
                v-model="password"
                placeholder="Password"
              />

              <q-input
                class="mb-2"
                rounded
                outlined
                dense
                type="password"
                v-model="password"
                placeholder="Repeat password"
              />

              <q-btn
                class="full-width mb-2"
                unelevated
                rounded
                color="primary"
                label="Log in"
              />

              <div class="text-subtitle1 text-center">
                Don't have an account yet?
                <a class="text-primary text-no-underline text-bold" href="/"
                  >Sign up!</a
                >
              </div>
            </div>

            <q-separator class="mt-2 mb-2" />

            <div class="p-1 pt-0">
              <q-btn
                class="full-width mb-1"
                unelevated
                rounded
                outline
                color="blue-10"
              >
                <q-icon class="mr-1" name="fab fa-facebook-square" />
                <div>Sign up with Facebook</div>
              </q-btn>
              <q-btn class="full-width" unelevated rounded outline color="red">
                <q-icon class="mr-1" name="fab fa-google" />
                <div>Sign up with Google</div>
              </q-btn>
            </div> -->
          </div>
        </div>

        <q-stepper v-model="step" ref="stepper" color="primary" animated>
          <q-step
            :name="1"
            title="Give us your e-mail address"
            icon="mail"
            :done="step > 1"
          >
            For each ad campaign that you create, you can control how much
            you're willing to spend on clicks and conversions, which networks
            and geographical locations you want your ads to show on, and more.
          </q-step>

          <q-step
            :name="2"
            title="Decide your password"
            icon="create_new_folder"
            :done="step > 2"
          >
            An ad group contains one or more ads which target a shared set of
            keywords.
          </q-step>

          <q-step :name="3" title="You are ready to go" icon="assignment">
            This step won't show up because it is disabled.
          </q-step>

          <template v-slot:navigation>
            <q-stepper-navigation>
              <q-btn
                @click="$refs.stepper.next()"
                color="primary"
                :label="step === 4 ? 'Finish' : 'Continue'"
              />
              <q-btn
                v-if="step > 1"
                flat
                color="primary"
                @click="$refs.stepper.previous()"
                label="Back"
                class="q-ml-sm"
              />
            </q-stepper-navigation>
          </template>
        </q-stepper>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script>
export default {
  name: "SignUp",
  props: ["isVisible"],
  data() {
    return {
      isVisibleModel: this.isVisible,
      email: "",
      password: "",
      step: 1
    };
  },
  created() {
    this.$emit("backgroundClassChange", "bg-primary");
  },
  watch: {
    isVisibleModel(val) {
      this.$emit("visibilityChange", val);
    },
    isVisible(val) {
      this.isVisibleModel = val;
    }
  }
};
</script>

<style scoped></style>
