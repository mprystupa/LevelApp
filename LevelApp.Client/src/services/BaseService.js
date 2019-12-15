import axios from "axios";
import LocalStorageService from "./local-storage/LocalStorageService";
import { Notify } from "quasar";

const baseDomain = "https://localhost:5001";
const baseUrl = `${baseDomain}/api`;

let axiosInstance = axios.create({
  baseURL: baseUrl
});

// Initialize request interceptor
axiosInstance.interceptors.request.use(
  function(config) {
    const token = LocalStorageService.getAccessToken();
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  function(error) {
    Promise.reject(error);
  }
);

// Initialize response interceptor
axiosInstance.interceptors.response.use(
  function(response) {
    return response;
  },
  function(error) {
    if (error) {
      if (error && error.response && error.response.status === 401 && window.location !== "") {
        window.location = "";

        Notify.create({
          color: "warning",
          icon: "fas fa-lock",
          message: "You are not logged in.",
          position: "top"
        });
      } else if (error && error.response) {
        let message = error.response.data.title || error.response.data.Message;

        Notify.create({
          color: "negative",
          icon: "fas fa-times",
          message: message,
          position: "top"
        });
      } else {
        let message = 'Something went wrong.';

        Notify.create({
          color: "negative",
          icon: "fas fa-times",
          message: message,
          position: "top"
        });
      }
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;
