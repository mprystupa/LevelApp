<template>
  <q-dialog v-model="isVisibleModel">
    <q-card style="max-width: 700px;">
      <q-card-section class="p-0">
        <div class="row full-width">
          <div class="col-0 col-sm-4 flex flex-center bg-secondary">
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
              Nice to see you again!
            </div>
          </div>
          <div class="col-12 col-sm-8 p-2">
            <div class="text-h4 pt-1 pb-1">Log in</div>

            <q-separator class="mt-2 mb-2" />

            <!-- Login form -->
            <form @submit.prevent.stop="onLoginSubmit">
              <div class="p-1 pt-0 pb-0">
                <q-input
                  class="mb-1"
                  rounded
                  outlined
                  dense
                  ref="email"
                  v-model="email"
                  placeholder="E-mail address"
                  :rules="[
                    val =>
                      inputValidators.Required(val) ||
                      'E-mail address is required',
                    val =>
                      inputValidators.CorrectEmail(val) ||
                      'E-mail address is incorrect'
                  ]"
                  autocomplete="username"
                >
                  <template
                    v-if="!($refs.email && $refs.email.hasError)"
                    v-slot:append
                  >
                    <q-icon size="16px" name="fas fa-envelope" />
                  </template>
                </q-input>
                <q-input
                  rounded
                  outlined
                  dense
                  ref="password"
                  type="password"
                  v-model="password"
                  placeholder="Password"
                  :rules="[
                    val =>
                      inputValidators.Required(val) || 'Password is required'
                  ]"
                  autocomplete="current-password"
                >
                  <template
                    v-if="!($refs.password && $refs.password.hasError)"
                    v-slot:append
                  >
                    <q-icon size="16px" name="fas fa-key" />
                  </template>
                </q-input>
                <div class="mb-2 float-right mr-2">
                  <a class="text-primary text-no-underline" href="/"
                    >Forgot password?</a
                  >
                </div>

                <q-btn
                  class="full-width mb-2"
                  unelevated
                  rounded
                  color="primary"
                  label="Log in"
                  type="submit"
                />

                <div class="text-subtitle1 text-center">
                  Don't have an account yet?
                  <a
                    class="text-primary text-no-underline text-bold"
                    href="javascript:void(0);"
                    @click="onSignUpClick"
                    >Sign up!</a
                  >
                </div>
              </div>
            </form>

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
                <div>Log in with Facebook</div>
              </q-btn>
              <q-btn class="full-width" unelevated rounded outline color="red">
                <q-icon class="mr-1" name="fab fa-google" />
                <div>Log in with Google</div>
              </q-btn>
            </div>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script>
import { InputValidators } from "../../../validators/InputValidators";
import FormValidator from "../../../validators/FormValidator";
import { ServiceFactory } from "../../../services/ServiceFactory";
import LocalStorageService from "../../../services/local-storage/LocalStorageService";
const UsersService = ServiceFactory.get("users");

export default {
  name: "Login",
  props: ["isVisible"],
  data() {
    return {
      isVisibleModel: this.isVisible,
      email: "",
      password: "",
      inputValidators: InputValidators,
      loginFormValidator: null
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

      if (val === true) {
        this.initializeModal();
      }
    }
  },
  methods: {
    initializeModal() {
      this.$nextTick(() => {
        this.loginFormValidator = new FormValidator(
          this.$refs.email,
          this.$refs.password
        );
      });
    },
    onSignUpClick() {
      this.$emit("signUpClicked");
    },
    onLoginSubmit() {
      this.loginFormValidator.validateForm();

      if (this.loginFormValidator.isFormValid()) {
        UsersService.login({
          email: this.email,
          password: this.password
        })
          .then(response => {
            this.$q.notify({
              color: "primary",
              icon: "fas fa-check",
              message: "Logged in!",
              position: "bottom"
            });

            LocalStorageService.setToken({ accessToken: response.data });

            // Redirect to Main module of application
            this.$router.push("/main");
          });
      }
    }
  }
};
</script>

<style scoped></style>
