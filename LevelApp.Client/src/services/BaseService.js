import axios from "axios";

const baseDomain = "https://localhost:5001";
const baseUrl = `${baseDomain}/api`;

export default axios.create({
  baseURL: baseUrl
});
