import BaseService from "../BaseService";

const resource = "lessons";

export default {
  get() {
    return BaseService.get(`${resource}`);
  },

  getUnassigned() {
    return BaseService.get(`${resource}/unassigned`);
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

  getLesson(lessonId) {
    return BaseService.get(`${resource}/${lessonId}`);
  },

  async createLesson(payload) {
    await BaseService.post(`${resource}`, payload).then(
      result => {
        return this.setLessonIcon({ id: result.data, icon: payload.iconFile });
      }
    );
  },

  updateLesson(payload) {
    if (payload.id) {
      return BaseService.put(`${resource}/${payload.id}`, payload);
    }

    console.error("Update payload does not contain an id.");
  },

  setLessonIcon(payload) {
    if (payload.id) {
      const formData = new FormData();
      formData.append("icon", payload.icon);
      formData.append("id", payload.id);

      return BaseService.post(`${resource}/${payload.id}/icon`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
    }

    console.error("Icon payload does not contain an id.");
  },

  deleteLesson(payload) {
    return BaseService.delete(`${resource}/${payload}`, payload);
  },

  addFavouriteLesson(payload) {
    return BaseService.post(`${resource}/${payload}/favourite/add`);
  },

  removeFavouriteLesson(payload) {
    return BaseService.post(`${resource}/${payload}/favourite/remove`);
  },

  finishLesson(payload) {
    if (payload.id) {
      return BaseService.post(`${resource}/${payload.id}/finish`, payload);
    }

    console.error("Finish lesson payload does not contain an id.");
  }
};
