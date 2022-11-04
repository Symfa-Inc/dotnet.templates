import axios from 'axios';

const authHttpService = axios.create({
  baseURL: process.env.REACT_APP_AUTH_URL,
  // withCredentials: true,
  // responseType: 'json',
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded',
  },
});
export const authHttpServiceJson = axios.create({
  baseURL: process.env.REACT_APP_AUTH_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export default authHttpService;
