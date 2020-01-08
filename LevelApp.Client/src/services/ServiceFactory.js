import UserService from "./core/UserService";
import LessonService from "./core/LessonService";
import CoursesService from "./core/CoursesService";

const services = {
  users: UserService,
  lessons: LessonService,
  courses: CoursesService
};

export const ServiceFactory = {
  get: name => services[name]
};
