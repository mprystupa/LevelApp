import axios from "axios";

const baseDomain = "https://localhost:5001";
const baseUrl = `${baseDomain}/api`;

export default axios.create({
  baseUrl
  //In case that you need a token:
  //headers: { "Authorization": "Bearer yourToken"}
});
