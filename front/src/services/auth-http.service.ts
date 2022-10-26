import axios from 'axios';

const authHttpService = axios.create({
  baseURL: process.env.REACT_APP_AUTH_URL,
  // withCredentials: true,
  // responseType: 'json',
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded',
  },
});

export default authHttpService;
