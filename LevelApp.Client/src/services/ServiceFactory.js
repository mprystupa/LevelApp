import UserService from "./core/UserService";
import LessonService from "./core/LessonService";

const services = {
  users: UserService,
  lessons: LessonService
};

export const ServiceFactory = {
  get: name => services[name]
};
