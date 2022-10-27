const ACCESS_TOKEN = 'access_token';
const REFRESH_TOKEN = 'refresh_token';

export class TokenService {
  static saveTokens(tokens: { access_token: string; refresh_token: string }) {
    localStorage.setItem(ACCESS_TOKEN, tokens.access_token);
    localStorage.setItem(REFRESH_TOKEN, tokens.refresh_token);
  }

  static getAccess() {
    return localStorage.getItem(ACCESS_TOKEN) || '';
  }

  static getRefresh() {
    return localStorage.getItem(REFRESH_TOKEN) || '';
  }

  static getTokens() {
    return {
      [ACCESS_TOKEN]: this.getAccess(),
      [REFRESH_TOKEN]: this.getRefresh(),
    };
  }

  static removeAccess() {
    return localStorage.removeItem(ACCESS_TOKEN);
  }

  static removeRefresh() {
    return localStorage.removeItem(REFRESH_TOKEN);
  }

  static clearTokens() {
    this.removeAccess();
    this.removeRefresh();
  }
}
