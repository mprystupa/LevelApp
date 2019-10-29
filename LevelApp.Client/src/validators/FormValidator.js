export default class FormValidator {
  constructor() {
    this.formRefs = [];
    for (let i = 0; i < arguments.length; i++) {
      this.formRefs.push(arguments[i]);
      console.log(arguments[i]);
    }
    console.log(this.formRefs);
  }

  validateForm() {
    for (let i = 0; i < this.formRefs.length; i++) {
      this.formRefs[i].validate();
    }
    console.log(this.formRefs);
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
