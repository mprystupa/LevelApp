<template>
  <q-dialog @hide="onDialogHide" v-model="isVisible">
    <q-card style="max-width: 700px;">
      <q-card-section class="p-0">
        <div class="row full-width">
          <div class="col-4 flex flex-center bg-secondary">
            <div>
              <img
                class=""
                alt="LevelApp logo"
                width="100%"
                src="../../assets/levelapp-logo-full.svg"
              />
            </div>

            <div class="text-subtitle1 text-white flex-align-self-end p-2">
              Nice to see you again!
            </div>
          </div>
          <div class="col-8 p-2">
            <div class="text-h4 pt-1 pb-1">Log in</div>

            <q-separator class="mt-2 mb-2" />

            <!-- Login form -->
            <div class="p-1 pt-0 pb-0">
              <q-input
                class="mb-1"
                rounded
                outlined
                dense
                v-model="text"
                label="Login"
              >
                <template v-slot:append>
                  <q-icon name="fas fa-user" />
                </template>
              </q-input>
              <q-input rounded outlined dense v-model="text" label="Password">
                <template v-slot:append>
                  <q-icon name="fas fa-key" />
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
export default {
  name: "Login",
  props: ["isVisible"],
  created() {
    this.$emit("backgroundClassChange", "bg-primary");
  },
  methods: {
    onDialogHide() {
      this.$emit("hide");
    }
  }
};
</script>

<style scoped></style>
