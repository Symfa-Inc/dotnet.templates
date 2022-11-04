import axios from 'axios';

export const apiHttpService = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
  // withCredentials: true,
  responseType: 'json',
  headers: {
    'Content-Type': 'application/json',
  },
});
