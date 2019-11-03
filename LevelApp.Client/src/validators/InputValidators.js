export const InputValidators = {
  Required: (val) => !!val,
  CorrectEmail: (val) => {
    const re = /^(([^<>()\]\\.,;:\s@"]+(\.[^<>()\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(val).toLowerCase());
  },
  MinLength: (val, length) => val.length >= length,
  MaxLength: (val, length) => val.length <= length,
  Equals: (val, valToCompare) => val === valToCompare
}
