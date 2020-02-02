<template>
  <div class="inline-block formula-container" :class="{ active: isFormulaSelected }" ref="formulaContainer">
    <q-tooltip
      v-model="isTooltipVisible"
      :delay="1000"
      anchor="top middle"
      self="bottom middle"
      :offset="[10, 10]"
    >
      Click to show LaTeX code
    </q-tooltip>

    <q-popup-proxy @before-show="onPopupShow" @before-hide="onPopupHide">
      <q-card>
        <q-card-section>
          <div class="text-grey-7 text-caption">LaTeX formula code</div>
          <q-input filled v-model="formulaCode" dense readonly>
            <template v-slot:after>
              <q-btn @click="copyFormula" dense color="primary" flat icon="fas fa-copy" />
            </template>
          </q-input>
        </q-card-section>
      </q-card>
    </q-popup-proxy>
  </div>
</template>

<script>
export default {
  props: ["formulaCode"],
  name: "FormulaComponent",
  data() {
    return {
      isFormulaSelected: false,
      isTooltipVisible: false
    };
  },
  mounted() {
    if (this.formulaCode) {
      this.renderFormula(this.formulaCode);
    }
  },
  methods: {
    renderFormula(formulaCode) {
      const katex = window.katex;

      katex.render(formulaCode, this.$refs.formulaContainer, {
        throwOnError: false
      });
    },
    copyFormula() {
      this.$copyText(this.formulaCode).then(() => {
        this.$q.notify({
          color: "positive",
          icon: "fa fas-check",
          message: "Formula has been copied!"
        });
      });
    },
    onPopupShow() {
      this.isTooltipVisible = false;
      this.isFormulaSelected = true;
    },
    onPopupHide() {
      this.isFormulaSelected = false;
    }
  }
};
</script>

<style lang="stylus" scoped>
.formula-container {
  cursor: pointer;
  border: rgba(black, 0) 1px solid;
  transition: border-color ease-in-out 0.2s, background-color ease-in-out 0.2s;
  padding: 2px;
  border-radius: 5px;

  &.active {
    background-color: rgba($lessons, 0.1);
    border: rgba($lessons, 0.3) 1px solid;
  }
}

.formula-container:hover {
  border: rgba($lessons, 0.2) 1px solid;
}
</style>
