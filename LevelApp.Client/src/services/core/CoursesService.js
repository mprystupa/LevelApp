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
    if (payload.id) {
      return BaseService.delete(`${resource}/${payload.id}`, payload);
    }

    console.log("Delete payload does not contain an id.");
  },

  addFavouriteCourse(payload) {
    return BaseService.post(`${resource}/${payload}/favourite/add`);
  },

  removeFavouriteCourse(payload) {
    return BaseService.post(`${resource}/${payload}/favourite/remove`);
  }
};
