import UserService from "./core/UserService";

const services = {
  users: UserService
};

export const ServiceFactory = {
  get: name => services[name]
};
