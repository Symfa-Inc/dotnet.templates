import { STATUS_CODES } from '@enums/status-codes.enum';
import { PATHS } from '@router/paths';
import { AxiosError } from 'axios';
import { apiHttpService } from './api-http.service';
import { AuthService } from './auth.service';
import { TokenService } from './token.service';

apiHttpService.interceptors.response.use((config) => config, async (error: AxiosError) => {
  const tokens = TokenService.getTokens();

  try {
    const { response } = error;
    if (response && response.status === STATUS_CODES.UNAUTHORIZED) {
      if (tokens.refresh_token) {
        TokenService.clearTokens();
        const tokenResponse = await AuthService.refreshToken(tokens.refresh_token);
        const newTokens = tokenResponse.data;
        TokenService.saveTokens(newTokens);

        error!.config!.headers!.Authorization = `Bearer ${newTokens.access_token}`;
        return apiHttpService.request(error.config);
      }

      location.href = PATHS.SignIn;
      // store.dispatch(logout());
      // window.history.pushState(null, '', PATHS.SignIn);
      // return { data: {} };
    }
  } catch (e) {
    const innerError = e as AxiosError;

    const isAuthServerError = (innerError.response?.data as Partial<Record<string, unknown>>)?.error_description;

    if (isAuthServerError === 'The specified refresh token is no longer valid.') {
    // if (innerError.response?.status === STATUS_CODES.UNAUTHORIZED) {
      location.href = PATHS.SignIn;
      // store.dispatch(logout());
      // window.history.pushState(null, '', PATHS.SignIn);
      // return { data: {} };
    }

    throw e;
  }

  throw error;
});

apiHttpService.interceptors.request.use((config) => {
  const tokens = TokenService.getTokens();
  const availableToken = tokens.access_token || tokens.refresh_token;

  if (availableToken) {
    config!.headers!.Authorization = `Bearer ${availableToken}`;
  }

  return config;
});

export default apiHttpService;
