import axios from 'axios';
// import { PATHS } from '@router/paths';
// import { STATUS_CODES } from '@enums/index';
// import { logout } from '@features/auth/authSlice';
// import { store } from '../store/store';
// import { TokenService } from './token.service';
// import { refreshToken } from './auth.service';
// import { AuthService } from './auth.service';

export const apiHttpService = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
  // withCredentials: true,
  responseType: 'json',
  headers: {
    'Content-Type': 'application/json',
  },
});
