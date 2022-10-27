import { SignIn, Token, User, UserCredentials } from '@pages/auth/auth.interface';
import { toFormUrlEncoded } from '@utils/encoder';
import { AxiosResponse } from 'axios';
// import { Sign } from 'crypto';
import authHttpService from './auth-http.service';

export class AuthService {
  static async getToken(credentials: SignIn): Promise<AxiosResponse<Token>> {
    // if (typeof credentials === 'object' && 'password' in credentials) {
    const data = {
      ...credentials,
      grant_type: 'password',
      scope: 'openid offline_access',
    };

    const form = toFormUrlEncoded(data);
    const credentialString = `${form}&client_id=${process.env.REACT_APP_CLIENT_ID}`;
    return authHttpService.post('/connect/token', credentialString);
    // }
    // return authHttpService.post('/connect/token', { ...credentials, client_id: process.env.REACT_APP_CLIENT_ID });
  }

  static async signup(credentials: UserCredentials) {
    return authHttpService.post<User>('/account/register', credentials, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  static async refreshToken(token: Token['refresh_token']) {
    const params = {
      refresh_token: token,
      client_id: process.env.REACT_APP_CLIENT_ID,
      grant_type: 'refresh_token',
    };
    // const headers = {
    //   'Content-Type': 'application/json',
    // };
    const data = toFormUrlEncoded(params);
    return authHttpService.post<Token>('/connect/token', data /* , { headers } */);
  }

  static async revokeToken(token: string) {
    const params = {
      token,
      client_id: process.env.REACT_APP_CLIENT_ID,
    };
    // const headers = {
    //   'Content-Type': 'application/json',
    // };
    const data = toFormUrlEncoded(params);
    return authHttpService.post<Token>('/connect/revoke', data /* , { headers } */);
  }
}
