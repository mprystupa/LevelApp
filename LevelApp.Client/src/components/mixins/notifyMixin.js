var notifyMixin = {
  methods: {
    notify(message, color="primary", icon = "", message = "", position = "top") {
      this.$q.notify({
        color: color,
        icon: icon,
        message: message,
        position: position
      });
    }
  }
};
