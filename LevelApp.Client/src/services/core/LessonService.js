import BaseService from "../BaseService";

const resource = "lessons";

export default {
  get() {
    return BaseService.get(`${resource}`);
  },

  getLesson(lessonId) {
    return BaseService.get(`${resource}/${lessonId}`);
  },

  createLesson(payload) {
    return BaseService.post(`${resource}`, payload);
  },

  updateLesson(payload) {
    if (payload.id) {
      return BaseService.put(`${resource}/${payload.id}`);
    }

    console.log("Update payload does not contain an id.");
  },

  deleteLesson(payload) {
    if (payload.id) {
      return BaseService.delete(`${resource}/${payload.id}`);
    }

    console.log("Delete payload does not contain an id.");
  }
};
