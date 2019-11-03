<template>
  <q-dialog v-model="isVisibleModel">
    <q-card style="max-width: 900px;">
      <q-card-section class="p-0">
        <div class="row full-width">
          <div class="col-0 col-sm-4 flex flex-center bg-secondary">
            <div>
              <img
                class
                alt="LevelApp logo"
                width="100%"
                src="../../../assets/levelapp-logo-full.svg"
              />
            </div>

            <div
              class="text-subtitle1 text-white text-center flex-align-self-end p-2"
            >We're glad you want to join us!</div>
          </div>

          <div class="col-12 col-sm-8">
            <q-stepper v-model="step" vertical flat color="primary" animated>
              <q-step
                :name="1"
                title="Insert your e-mail address"
                icon="fas fa-envelope"
                :done="step > 1"
              >
                <div class="mb-2">
                  First, we will need an e-mail address that will be used for
                  your identification and contact. Please insert in the field
                  below your valid e-mail address.
                </div>

                <q-input
                  class="mb-1"
                  rounded
                  outlined
                  dense
                  v-model="email"
                  ref="email"
                  placeholder="E-mail address"
                  :rules="[
                    val =>
                      inputValidators.Required(val) ||
                      'E-mail address is required',
                    val =>
                      inputValidators.CorrectEmail(val) ||
                      'E-mail address is incorrect'
                  ]"
                >
                  <template v-slot:append>
                    <q-icon size="16px" name="fas fa-envelope" />
                  </template>
                </q-input>

                <q-stepper-navigation>
                  <q-btn
                    :disable="!isStepValid(1)"
                    @click="step = 2"
                    color="primary"
                    label="Continue"
                  />
                </q-stepper-navigation>
              </q-step>

              <q-step :name="2" title="Set your password" icon="fas fa-key" :done="step > 2">
                <div class="mb-2">
                  You'll need a strong password that will protect your account
                  from unauthorized access. Remember: you should never share
                  your password with anyone!
                </div>

                <q-input
                  rounded
                  outlined
                  dense
                  class="mb-1"
                  type="password"
                  ref="password"
                  v-model="password"
                  placeholder="Password"
                  :rules="[
                    val =>
                      inputValidators.Required(val) ||
                      'Password is required',
                    val =>
                      inputValidators.MinLength(8) ||
                      'Password must be at least 8 characters long'
                  ]"
                >
                  <template v-slot:append>
                    <q-icon size="16px" name="fas fa-key" />
                  </template>
                </q-input>

                <q-input
                  rounded
                  outlined
                  dense
                  type="password"
                  ref="repeatPassword"
                  v-model="repeatPassword"
                  placeholder="Repeat password"
                  :rules="[
                    val =>
                      inputValidators.Equals(val, password) ||
                      'Entered passwords must match'
                  ]"
                >
                  <template v-slot:append>
                    <q-icon size="16px" name="fas fa-key" />
                  </template>
                </q-input>

                <q-stepper-navigation>
                  <q-btn flat @click="step = 1" color="primary" label="Back" class="q-ml-sm" />
                  <q-btn
                    :disable="!isStepValid(2)"
                    @click="step = 3"
                    color="primary"
                    label="Continue"
                  />
                </q-stepper-navigation>
              </q-step>

              <q-step :name="3" title="Choose your avatar" caption="Optional" icon="fas fa-user">
                <div class="mb-2"></div>

                <q-uploader
                  url="http://localhost:4444/upload"
                  label="Restricted to images"
                  multiple
                  accept=".jpg, image/*"
                  style="max-width: 300px"
                />

                <q-stepper-navigation>
                  <q-btn flat @click="step = 2" color="primary" label="Back" class="q-ml-sm" />
                  <q-btn @click="step = 4" color="primary" label="Continue" />
                </q-stepper-navigation>
              </q-step>

              <q-step :name="4" title="You are ready to go!" icon="fas fa-thumbs-up">
                Try out different ad text to see what brings in the most
                customers, and learn how to enhance your ads using features like
                ad extensions. If you run into any problems with your ads, find
                out how to tell if they're running and how to resolve approval
                issues.
                <q-stepper-navigation>
                  <q-btn color="primary" label="Finish" />
                  <q-btn flat @click="step = 3" color="primary" label="Back" class="q-ml-sm" />
                </q-stepper-navigation>
              </q-step>
            </q-stepper>

            <div class="text-subtitle1 text-center p-2">
              Already have an account?
              <a
                class="text-primary text-no-underline text-bold"
                href="javascript:void(0);"
                @click="onLoginClick"
              >Log in!</a>
            </div>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script>
import { InputValidators } from "../../../validators/InputValidators";

export default {
  name: "SignUp",
  props: ["isVisible"],
  data() {
    return {
      isVisibleModel: this.isVisible,
      email: "",
      password: "",
      repeatPassword: "",
      step: 1,
      inputValidators: InputValidators
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
  },
  methods: {
    onLoginClick() {
      this.$emit("loginClicked");
    },
    isStepValid(step) {
      switch (step) {
        case 1:
          if (this.$refs.email) {
            return !this.$refs.email.hasError;
          }
          break;

        case 2:
          if (this.$refs.password && this.$refs.repeatPassword) {
            return (
              !this.$refs.password.hasError &&
              !this.$refs.repeatPassword.hasError
            );
          }
          break;

        default:
          return true;
      }

      return false;
    }
  }
};
</script>

<style scoped></style>
