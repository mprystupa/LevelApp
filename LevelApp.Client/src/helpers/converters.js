export const Converters = {
  textHashCode: text => {
    let hash = 0;
    for (let i = 0; i < text.length; i++) {
      hash = text.charCodeAt(i) + ((hash << 5) - hash);
    }
    return hash;
  },
  textToColor: text => {
    let hash = Converters.textHashCode(text);

    let c = (hash & 0x00ffffff).toString(16).toUpperCase();

    return "00000".substring(0, 6 - c.length) + c;
  }
};
