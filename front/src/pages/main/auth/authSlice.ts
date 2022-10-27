/* eslint-disable camelcase */
import { ProcessState } from '@enums/progressState.enum';
import { STATUS_CODES } from '@enums/statusCodes.enum';
import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { PATHS } from '@router/paths';
import { AuthService } from '@services/auth.service';
import { ProfileService } from '@services/profile.service';
import { TokenService } from '@services/token.service';
import { AppThunk, RootState } from '@store/store';
import { AxiosError } from 'axios';
import { UserManager, UserManagerSettings } from 'oidc-client-ts';
import { AuthState, SignIn, UserAdditionalFields, UserCredentials } from './auth.interface';

const initialState: AuthState = {
  state: ProcessState.Idle,
  user: {
    userId: '',
    userName: '',
    email: '',
  },
  signInError: null,
  signUpError: null,
  partlyRegistered: false,
};

const scopes = 'email offline_access';

const clientSettings: UserManagerSettings = {
  authority: process.env.REACT_APP_AUTH_URL || '',
  client_id: process.env.REACT_APP_CLIENT_ID || '',
  redirect_uri: `${window?.location?.origin}${PATHS.ProviderAuthCallBack}`,
  response_type: 'code',
  scope: scopes,
  extraTokenParams: { scope: scopes },
  // post_logout_redirect_uri: '/hz',
  // metadata: {
  //   authorization_endpoint: 'http://localhost:5000/connect/authorize',
  // },
};

export const userManager = new UserManager(clientSettings);

function handleError(err: unknown, handler: (error: any) => any) {
  if (err instanceof AxiosError && err.response?.data && STATUS_CODES.BAD_REQUEST === err.response.status) {
    const error = err.response?.data;
    const errorText = typeof error === 'string' ? error : error?.error_description || '';
    return handler(errorText);
  }
  throw err;
}

export const signinAction = createAsyncThunk('auth/signin', async (credentials: SignIn, { rejectWithValue }) => {
  try {
    const tokenResponse = await AuthService.getToken(credentials);
    TokenService.saveTokens(tokenResponse.data);

    const userResponse = await ProfileService.profile();
    return userResponse.data;
  } catch (err) {
    return handleError(err, rejectWithValue);
  }
});

export const signupAction = createAsyncThunk(
  'auth/signup',
  async (credentials: UserCredentials, { rejectWithValue }) => {
    try {
      const response = await AuthService.signup(credentials);
      const tokenResponse = await AuthService.getToken(credentials);
      TokenService.saveTokens(tokenResponse.data);

      return response.data;
    } catch (err) {
      return handleError(err, rejectWithValue);
    }
  },
);

export const completeRegistrationAction = createAsyncThunk(
  'auth/complete_registration',
  async (profile: UserAdditionalFields, { rejectWithValue }) => {
    try {
      const response = await ProfileService.createProfile(profile);
      return response.data;
    } catch (err) {
      return handleError(err, rejectWithValue);
    }
  },
);

export const signinWithProviderAction = createAsyncThunk(
  'auth/signin_with_provider',
  async (provider: string, { rejectWithValue }) => {
    try {
      const data = await userManager.signinPopup({ extraQueryParams: { provider } });
      const { access_token, refresh_token = '' } = data;
      TokenService.saveTokens({ access_token, refresh_token });
      const userResponse = await ProfileService.profile();
      return userResponse.data;
    } catch (err) {
      return handleError(err, rejectWithValue);
    }
  },
);

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    resetSignInErrorState: (state) => {
      state.state = ProcessState.Idle;
      state.signInError = null;
    },
    resetSignUpErrorState: (state) => {
      state.state = ProcessState.Idle;
      state.signUpError = null;
    },
    incompleteRegistration: (state) => {
      state.partlyRegistered = true;
    },
    updateProfile: (state, action) => {
      state.user = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(signinAction.pending, (state) => {
        state.state = ProcessState.Loading;
        state.signInError = null;
        state.partlyRegistered = false;
      })
      .addCase(signinAction.fulfilled, (state, action) => {
        state.user = action.payload!;
        state.state = ProcessState.Idle;
      })
      .addCase(signinAction.rejected, (state, action) => {
        if ((action.payload as string).includes('UserProfileNotFoundException')) {
          // if (action.error.code === 'UserProfileNotFoundException') {
          state.signInError = 'User profile not found, please complete registration';
          state.partlyRegistered = true;
          state.state = ProcessState.Idle;
        } else {
          state.state = ProcessState.Error;
          state.signInError = action.payload as string;
        }
      })

      .addCase(signupAction.pending, (state) => {
        state.state = ProcessState.Loading;
        state.signUpError = null;
      })
      .addCase(signupAction.fulfilled, (state) => {
        state.partlyRegistered = true;
        state.state = ProcessState.Idle;
      })
      .addCase(signupAction.rejected, (state, data) => {
        state.state = ProcessState.Error;
        state.signUpError = (data.payload as string) ?? '';
      })

      .addCase(completeRegistrationAction.pending, (state) => {
        state.state = ProcessState.Loading;
        state.signUpError = null;
      })
      .addCase(completeRegistrationAction.fulfilled, (state, action: PayloadAction<typeof initialState['user']>) => {
        state.partlyRegistered = false;
        state.user = action.payload;
      })
      .addCase(completeRegistrationAction.rejected, (state, data) => {
        state.state = ProcessState.Error;
        state.signUpError = (data.payload as string) ?? '';
      })

      .addCase(signinWithProviderAction.fulfilled, (state, action: PayloadAction<typeof initialState['user']>) => {
        state.user = action.payload!;
      })
      .addCase(signinWithProviderAction.rejected, (state, data) => {
        state.state = ProcessState.Error;
        state.signUpError = (data.payload as string) ?? '';
      });
  },
});

export const { resetSignInErrorState, resetSignUpErrorState, updateProfile } = authSlice.actions;

export const logout = (): AppThunk => async (dispatch, _getState) => {
  dispatch(updateProfile(initialState.user));
  const accessToken = TokenService.getAccess();
  TokenService.clearTokens();
  if (accessToken) await AuthService.revokeToken(accessToken);
};

export const restoreSession = (): AppThunk => async (dispatch, _getState) => {
  const accessToken = TokenService.getAccess();
  if (accessToken) {
    const { data } = await ProfileService.profile();
    dispatch(updateProfile(data));
  }
};

export const selectUser = (state: RootState) => state.auth.user;

export const selectLoadingState = (state: RootState) => state.auth.state;

export const selectSignInError = (state: RootState) => state.auth.signInError;

export const selectSignUpError = (state: RootState) => state.auth.signUpError;

export const selectPartlyRegistered = (state: RootState) => state.auth.partlyRegistered;

export default authSlice.reducer;
