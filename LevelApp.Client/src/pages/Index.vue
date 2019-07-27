<template>
  <q-page class="flex flex-center">
    <full-page
      ref="fullpage"
      :options="options"
      id="fullpage"
      class="full-width"
    >
      <!-- Background -->
      <div
        id="background"
        class="background bg-transition bg-hex-overlay fixed full-width full-height"
        :class="backgroundClass"
      ></div>
      <div class="bg-hex-overlay fixed full-width full-height"></div>
      <div class="full-width section">
        <!-- Logo -->
        <div class="row justify-center">
          <div>
            <img
              class="mb-1"
              alt="LevelApp logo"
              width="300px"
              src="../assets/levelapp-logo-full.svg"
            />
          </div>
        </div>
        <!-- Text -->
        <div class="row justify-center text-align-center">
          <div>
            <q-separator color="white" spaced inset />
            <div class="text-h5 text-white text-weight-thin mt-1">
              Learn whatever you want, however you want, whenever you want!
            </div>
          </div>
        </div>
        <!-- Buttons -->
        <div class="row justify-center">
          <div class="mt-2">
            <q-btn
              color="white"
              class="mr-2 btn-fixed-width"
              size="lg"
              outline
              rounded
              label="Sign up"
            />
            <q-btn
              color="white"
              class="ml-2 btn-fixed-width"
              size="lg"
              outline
              rounded
              label="Log in"
            />
          </div>
        </div>
      </div>
      <div class="full-width section text-white text-align-center">
        <div class="row justify-center mr-3 ml-3 mb-3">
          <div class="text-h3 mb-1">
            Learning and teaching new things <strong>your way</strong>
          </div>
          <div class="text-h5 text-weight-thin">
            LevelApp is all about you - whether you want to share your knowledge
            or let others teach you. With use of RPG skill trees and
            achievements, you will soon find out that exchange of knowledge was
            never so easy and efficient. Learn, progress, create and share -
            everything is in reach of your hand, absolutely free.
          </div>
        </div>
      </div>
      <div class="full-width section text-white">
        <div class="row mb-4 min-h-250 justify-center">
          <div class="col-2">
            <img
              class="block m-auto"
              alt="Select icon"
              width="150px"
              src="../assets/landing-page/panel-1-pic-0.svg"
            />
          </div>
          <div class="col-7 self-center">
            <div class="text-h4 mb-1">Select</div>
            <div class="text-h6 text-weight-thin text-align-justify">
              Pick a course that interests you from over 200 courses created by
              professionals and start learning right off the bat. There are many
              different courses for you to choose from, and all you need to do
              is open the Courses tab and start looking for one that you need
              the most.
            </div>
          </div>
        </div>
        <div class="row min-h-250 justify-center">
          <div class="col-2">
            <img
              class="block m-auto"
              alt="Learn icon"
              width="150px"
              src="../assets/landing-page/panel-1-pic-1.svg"
            />
          </div>
          <div class="col-7 self-center">
            <div class="text-h4 mb-1">Learn</div>
            <div class="text-h6 text-weight-thin text-align-justify">
              After selecting desired course your eyes will gaze upon the Skill
              Tree - a structure showing connections between skills, allowing
              you to easily see what and when you should learn next. In the
              beginning, it will look rather simple, but do not be deceived -
              with every learned skill, new ones are unlocked. Lesson by lesson,
              skill by skill you will notice that your knowledge is growing -
              and it is growing fast!
            </div>
          </div>
        </div>
      </div>
      <div class="full-width section text-white">
        <div class="row mb-4 min-h-250 justify-center">
          <div class="col-7 self-center">
            <div class="text-h4 mb-1 text-align-right">Create</div>
            <div class="text-h6 text-weight-thin text-align-justify">
              LevelApp is not only about learning - it is also a powerful tool
              for teachers! You want to teach your subject in a modern way? Or
              maybe wish to create an introduction course for new members of
              your team? All of this is possible here: you can create lessons
              using intuitive editor, then join them together as a skill and
              finally connect different skill into your own Skill Tree! You can
              design achievements exclusive for your course as well, making each
              and every one of them a special experience.
            </div>
          </div>
          <div class="col-2">
            <img
              class="block m-auto"
              alt="Create icon"
              width="150px"
              src="../assets/landing-page/panel-1-pic-2.svg"
            />
          </div>
        </div>
        <div class="row min-h-250l justify-center">
          <div class="col-7 self-center">
            <div class="text-h4 mb-1 text-align-right">Share</div>
            <div class="text-h6 text-weight-thin text-align-justify">
              After putting those finishing touches in your own course, you
              might want to show it to the world and share it. Using LevelApp it
              could not be simpler - with a single button click you can create a
              link and post it on Facebook, Twitter or send it through the mail!
              Now you can reach your students faster and easier than ever
              before.
            </div>
          </div>
          <div class="col-2">
            <img
              class="block m-auto"
              alt="Share icon"
              width="150px"
              src="../assets/landing-page/panel-1-pic-3.svg"
            />
          </div>
        </div>
      </div>
      <div class="full-width section text-white">
        <div class="row justify-center">
          <div>
            <img
              class="mb-3"
              alt="Multiplatform logo"
              width="800px"
              src="../assets/landing-page/panel-5-pic-0.svg"
            />
          </div>
        </div>
        <div class="row justify-center">
          <div class="text-h3 mb-1">
            Wherever you want
          </div>
        </div>
        <div class="row justify-center">
          <div class="text-h6 mr-3 ml-3 text-weight-thin text-align-center">
            LevelApp is multi-platform - that means you can download it on your
            phone, tablet or PC After putting those finishing touches in your
            own course, you might want to show it to the world and share it.
            Using LevelApp it could not be simpler - with a single button click
            you can create a link and post it on Facebook, Twitter or send it
            through the mail! Now you can reach your students faster and easier
            than ever before.
          </div>
        </div>
      </div>
      <div class="full-width section text-white">
        <div class="row justify-center items-start">
          <div class="text-h3 mb-1">
            Start right now!
          </div>
        </div>
        <div class="row justify-center">
          <div class="text-h6 mr-3 ml-3 mb-2 text-weight-thin text-align-center">
            You can make your account right now and completely for free.
          </div>
        </div>
        <div class="row justify-center mb-15">
          <q-btn
            color="white"
            class="btn-fixed-width z-index-100"
            size="lg"
            outline
            rounded
            label="Sign up"
          />
        </div>
        <div class="row justify-center">
          <div class="waveWrapper waveAnimation">
            <div class="waveWrapperInner bgTop">
              <div
                class="wave waveTop"
                style="background-image: url('http://front-end-noobs.com/jecko/img/wave-top.png')"
              ></div>
            </div>
            <div class="waveWrapperInner bgMiddle">
              <div
                class="wave waveMiddle"
                style="background-image: url('http://front-end-noobs.com/jecko/img/wave-mid.png')"
              ></div>
            </div>
            <div class="waveWrapperInner bgBottom">
              <div
                class="wave waveBottom"
                style="background-image: url('http://front-end-noobs.com/jecko/img/wave-bot.png')"
              ></div>
            </div>
          </div>
        </div>
        <div class="bg-white footer-filling z-index-100">
          <div class="row mt-3 mb-1 justify-center">
            <div class="text-h3 text-weight-thin text-align-center text-black">
              Find us on social media!
            </div>
          </div>
          <div class="row q-gutter-lg justify-center">
            <q-btn unelevated round color="indigo" icon="fab fa-facebook-f" />
            <q-btn unelevated round color="red" icon="fab fa-youtube" />
            <q-btn unelevated round color="blue" icon="fab fa-linkedin-in" />
            <q-btn unelevated round color="light-blue" icon="fab fa-twitter" />
          </div>
        </div>
      </div>
    </full-page>
    <q-page-sticky v-if="isScrolled" position="bottom-right" :offset="[25, 25]">
      <q-btn
        fab
        icon="keyboard_arrow_up"
        color="primary"
        @click="$refs.fullpage.api.moveTo(1)"
      />
    </q-page-sticky>
  </q-page>
</template>

<script>
export default {
  name: "PageIndex",
  sectionBackgroundClasses: {},

  data() {
    return {
      options: {
        navigation: true,
        onLeave: this.onLeave
      },
      backgroundClass: "bg-primary",
      isScrolled: false
    };
  },

  mounted() {
    this.sectionBackgroundClasses = {
      "0": "bg-primary",
      "1": "bg-teal-6",
      "2": "bg-blue-6",
      "3": "bg-indigo-6",
      "4": "bg-deep-purple-6",
      "5": "bg-red-8"
    };
  },

  methods: {
    onLeave(origin, destination, direction) {
      this.backgroundClass = this.sectionBackgroundClasses[destination.index];

      this.isScrolled = destination.index !== 0;
    }
  }
};
</script>

<style>
.footer-filling {
  position: fixed;
  width: 100%;
  height: 226px;
  padding: 5px;
  bottom: 0px;
}

@keyframes move_wave {
  0% {
    transform: translateX(0) translateZ(0) scaleY(1);
  }
  50% {
    transform: translateX(-25%) translateZ(0) scaleY(0.55);
  }
  100% {
    transform: translateX(-50%) translateZ(0) scaleY(1);
  }
}
.waveWrapper {
  overflow: hidden;
  position: absolute;
  left: 0;
  right: 0;
  bottom: 0;
  top: 0;
  margin: auto;
}
.waveWrapperInner {
  position: absolute;
  width: 100%;
  overflow: hidden;
  height: 100%;
  bottom: -1px;
}
.bgTop {
  z-index: 15;
  opacity: 0.5;
}
.bgMiddle {
  z-index: 10;
  opacity: 0.75;
}
.bgBottom {
  z-index: 5;
}
.wave {
  position: absolute;
  left: 0;
  width: 200%;
  height: 70%;
  background-repeat: repeat no-repeat;
  background-position: 0 bottom;
  transform-origin: center bottom;
}
.waveTop {
  background-size: 50% 100px;
}
.waveAnimation .waveTop {
  animation: move-wave 3s;
  -webkit-animation: move-wave 3s;
  -webkit-animation-delay: 1s;
  animation-delay: 1s;
}
.waveMiddle {
  background-size: 50% 120px;
}
.waveAnimation .waveMiddle {
  animation: move_wave 10s linear infinite;
}
.waveBottom {
  background-size: 50% 100px;
}
.waveAnimation .waveBottom {
  animation: move_wave 15s linear infinite;
}

.bg-hex-overlay {
  background: url('../assets/landing-page/background.png');
  -webkit-animation: slide 20s linear infinite;
  z-index: -1;
}

@-webkit-keyframes slide {
  from { background-position: 0 0; }
  to { background-position: -400px -400px; }
}

.bg-transition {
  -webkit-transition: background-color 500ms linear;
  -ms-transition: background-color 500ms linear;
  transition: background-color 500ms linear;
}

.background {
  z-index: -2;
}

.btn-fixed-width {
  width: 200px;
}
</style>
