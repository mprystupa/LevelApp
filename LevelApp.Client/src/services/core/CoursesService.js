import BaseService from "../BaseService";

const resource = "courses";

export default {
  get() {
    return BaseService.get(`${resource}`);
  },

  searchAll(params) {
    return BaseService.get(`${resource}/search`, {
      params
    });
  },

  searchCreated(params) {
    return BaseService.get(`${resource}/search/created`, {
      params
    });
  },

  getCourse(courseId) {
    return BaseService.get(`${resource}/${courseId}`);
  },

  createCourse(payload) {
    return BaseService.post(`${resource}`, payload);
  },

  updateCourse(payload) {
    if (payload.id) {
      return BaseService.put(`${resource}/${payload.id}`, payload);
    }

    console.log("Update payload does not contain an id.");
  },

  deleteCourse(payload) {
    return BaseService.delete(`${resource}/${payload}`, payload);
  },

  addFavouriteCourse(payload) {
    return BaseService.post(`${resource}/${payload}/favourite/add`);
  },

  removeFavouriteCourse(payload) {
    return BaseService.post(`${resource}/${payload}/favourite/remove`);
  },

  addAttendingCourse(payload) {
    return BaseService.post(`${resource}/${payload}/attending/add`);
  },

  removeAttendingCourse(payload) {
    return BaseService.post(`${resource}/${payload}/attending/remove`);
  }
};
