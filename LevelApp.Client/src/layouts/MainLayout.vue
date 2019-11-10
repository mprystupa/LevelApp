<template>
  <div>
    <q-layout view="hHh LpR fFf">
      <q-header reveal elevated class="bg-primary text-white">
        <q-toolbar class="pt-1 pb-1 toolbar">
          <q-input rounded outlined dense v-model="text" class="search-bar" color="white" bg-color="search-bar-bg" label="Rounded standout" />
          <div class="">
            <img
              class="toolbar-logo"
              src="../assets/levelapp-logo-horizontal.svg"
            />
          </div>
        </q-toolbar>
      </q-header>

      <q-drawer
        v-model="drawer"
        show-if-above
        :mini="!drawer || miniState"
        @click.capture="drawerClick"
        :width="200"
        :breakpoint="500"
        bordered
        class="drawer-transparent"
      >
        <template v-slot:mini>
          <q-scroll-area class="fit mini-slot cursor-pointer">
            <div class="q-py-lg">
              <div class="column items-center">
                <q-icon name="inbox" color="blue" class="mini-icon" />
                <q-icon name="star" color="orange" class="mini-icon" />
                <q-icon name="send" color="purple" class="mini-icon" />
                <q-icon name="drafts" color="teal" class="mini-icon" />
              </div>
            </div>
          </q-scroll-area>
        </template>

        <q-scroll-area class="fit">
          <q-list padding class="menu-list">
            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="inbox" />
              </q-item-section>

              <q-item-section>
                Inbox
              </q-item-section>
            </q-item>

            <q-item active clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="star" />
              </q-item-section>

              <q-item-section>
                Star
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="send" />
              </q-item-section>

              <q-item-section>
                Send
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="drafts" />
              </q-item-section>

              <q-item-section>
                Drafts
              </q-item-section>
            </q-item>
          </q-list>
        </q-scroll-area>

        <!--
          in this case, we use a button (can be anything)
          so that user can switch back
          to mini-mode
        -->
        <div
          class="q-mini-drawer-hide absolute"
          style="top: 15px; right: -17px"
        >
          <q-btn
            dense
            round
            unelevated
            color="accent"
            icon="chevron_left"
            @click="miniState = true"
          />
        </div>
      </q-drawer>

      <q-page-container>
        <router-view />
      </q-page-container>
    </q-layout>
    <!-- Background -->
    <background background-class="bg-main" />
  </div>
</template>

<script>
import Background from "../components/Background";
export default {
  components: { Background },
  data() {
    return {
      drawer: false,
      miniState: true
    };
  },

  methods: {
    drawerClick(e) {
      // if in "mini" state and user
      // click on drawer, we switch it to "normal" mode
      if (this.miniState) {
        this.miniState = false;

        // notice we have registered an event with capture flag;
        // we need to stop further propagation as this click is
        // intended for switching drawer to "normal" mode only
        e.stopPropagation();
      }
    }
  }
};
</script>

<style lang="stylus">
.bg-main {
  background: rgb(189,237,199);
  background: linear-gradient(130deg, rgba(189,237,199,1) 0%, rgba(128,233,227,1) 100%);
}

.menu-list .q-item {
  border-radius: 0 32px 32px 0
}

.drawer-transparent {
  .q-drawer {
    background-color: rgba(255, 255, 255, 0.2);
    border: none;
  }
}

.toolbar {
  justify-content: space-between;
}

.toolbar::after {
  content: "";
  width: 300px;
  visibility: hidden;
}

.toolbar-logo {
  height: 35px;
}

.search-bar {
  width: 300px;
}

.search-bar-bg {
  background-color: rgba(0, 0, 0, 0.1);
}
</style>
