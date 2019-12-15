import BaseService from "../BaseService";

const resource = "lessons";

export default {
  get() {
    return BaseService.get(`${resource}`);
  },

  searchAll(params) {
    return BaseService.get(`${resource}/search`, { params });
  },

  searchCreated(params) {
    return BaseService.get(`${resource}/search/created`, { params });
  },

  getLesson(lessonId) {
    return BaseService.get(`${resource}/${lessonId}`);
  },

  createLesson(payload) {
    return BaseService.post(`${resource}`, payload);
  },

  updateLesson(payload) {
    if (payload.id) {
      return BaseService.put(`${resource}/${payload.id}`, payload);
    }

    console.log("Update payload does not contain an id.");
  },

  deleteLesson(payload) {
    if (payload.id) {
      return BaseService.delete(`${resource}/${payload.id}`, payload);
    }

    console.log("Delete payload does not contain an id.");
  }
};
