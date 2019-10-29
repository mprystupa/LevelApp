import BaseService from "../BaseService";

const resource = "users";

export default {
  get() {
    return BaseService.get(`${resource}`);
  },

  getUser(userId) {
    return BaseService.get(`${resource}/${userId}`);
  },

  createUser(payload) {
    return BaseService.post(`${resource}`, payload);
  },

  login(payload) {
    return BaseService.post(`${resource}/login`, payload);
  }
};
