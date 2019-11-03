export default class FormValidator {
  constructor() {
    this.formRefs = [];
    for (let i = 0; i < arguments.length; i++) {
      this.formRefs.push(arguments[i]);
    }
  }

  validateForm() {
    for (let i = 0; i < this.formRefs.length; i++) {
      this.formRefs[i].validate();
    }
  }

  isFormValid() {
    for (let i = 0; i < this.formRefs.length; i++) {
      if (this.formRefs[i].hasError) {
        return false;
      }
    }

    return true;
  }
}
