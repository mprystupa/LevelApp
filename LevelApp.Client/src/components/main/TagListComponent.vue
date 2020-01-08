<template>
  <div class="q-gutter-xs">
    <!-- Existing tags -->
    <transition-group
      appear
      enter-active-class="animated bounceIn"
      leave-active-class="animated bounceOut"
    >
      <q-chip
        v-for="(tag, index) in value"
        class="chip"
        :key="`${tag}-${index}`"
        :data-flip-key="`tag-chip-${index}`"
        removable
        @remove="removeTag(index)"
        :style="getTagBackgroundStyle(tag)"
        text-color="white"
      >
        {{ tag }}
      </q-chip>
    </transition-group>

    <!-- Add new tag -->
    <transition
      appear
      enter-active-class="animated bounceIn"
      leave-active-class="animated bounceOut"
    >
      <q-chip
        clickable
        @click="isAddInputActive = true"
        class="chip-icon-no-margin"
        :class="{
          'add-chip-selected': isAddInputActive
        }"
        :ripple="false"
        color="blue-grey-3"
        text-color="white"
        :icon="isAddInputActive ? '' : 'fas fa-plus'"
      >
        <q-input
          autofocus
          v-if="isAddInputActive"
          @keyup.enter="$event.target.blur"
          @blur="addTag(addTagText)"
          borderless
          v-model="addTagText"
        />

        <!-- Add tag tooltip -->
        <q-tooltip v-if="!isAddInputActive">
          Add new tag
        </q-tooltip>
      </q-chip>
    </transition>
  </div>
</template>

<script>
import { Converters } from "../../helpers/converters";

export default {
  props: ["value"],
  name: "TagListComponent",
  data() {
    return {
      isAddInputActive: false,
      addTagText: ""
    };
  },
  methods: {
    getTagBackgroundStyle(text) {
      return `background-color: #${Converters.textToColor(text)}`;
    },
    addTag(tagText) {
      this.isAddInputActive = false;
      this.addTagText = "";

      tagText = tagText.trim();
      if (tagText && tagText !== "") {
        this.value.push(tagText);
      }

      this.updateValue();
    },
    removeTag(tagIndex) {
      if (tagIndex in this.value) {
        this.value.splice(tagIndex, 1);
      }

      this.updateValue();
    },
    updateValue() {
      this.$emit('value', this.value);
    }
  }
};
</script>

<style scoped>
.chip-icon-no-margin {
  width: 45px;
  transition: ease-in-out 0.2s;
}
.add-chip-selected {
  width: 200px;
}
</style>
