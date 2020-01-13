<template>
  <div class="icon-wrapper">
    <div class="row flex-center full-width q-mb-lg">
      <div class="flex lesson-icon bg-lessons clip-hex" @click="onIconClick">
        <img class="icon-img" ref="iconImg" />
      </div>

      <!-- Overlay -->
      <div class="flex lesson-icon clip-hex icon-overlay">
        <span class="text-subtitle1 m-auto">Change icon</span>
      </div>
    </div>
    <input
      class="icon-input"
      type="file"
      ref="iconInput"
      @change="onIconChange"
    />
  </div>
</template>

<script>
export default {
  props: ["iconColor", "iconUrl"],
  name: "IconLoader",
  data() {
    return {
      icon: null,
      fileReader: null
    };
  },
  created() {
    this.fileReader = new FileReader();
  },
  mounted() {
    if (this.iconUrl && this.iconUrl !== "") {
      this.$refs.iconImg.src = this.iconUrl;
    }
  },
  methods: {
    onIconClick() {
      this.$refs.iconInput.click();
    },
    onIconChange() {
      if (this.$refs.iconInput.files && this.$refs.iconInput.files[0]) {
        this.icon = this.$refs.iconInput.files[0];
        let iconImg = this.$refs.iconImg;

        this.fileReader.onload = function(e) {
          iconImg.src = e.target.result;
        };

        this.fileReader.readAsDataURL(this.icon);
        this.$emit('change', this.icon);
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.lesson-icon {
  width: 200px;
  height: 200px;
  cursor: pointer;
}

.icon-input {
  visibility: hidden;
}

.icon-wrapper {
  position: relative;
  transition: filter ease-in-out 0.2s;
}

.icon-wrapper:hover {
  filter: drop-shadow(2px 2px 6px black);
}

.icon-overlay {
  position: absolute;
  background-color: rgba(black, 0);
  transition: background-color ease-in-out 0.2s;
  pointer-events: none;

  span {
    transition: color ease-in-out 0.2s;
    color: rgba(white, 0);
  }
}

.icon-wrapper:hover .icon-overlay {
  background-color: rgba(black, 0.2);

  span {
    color: rgba(white, 1);
  }
}

.icon-img {
  width: 65%;
  height: 65%;
  margin: auto;
  display: none;
}

.icon-img[src] {
  display: block;
}
</style>
