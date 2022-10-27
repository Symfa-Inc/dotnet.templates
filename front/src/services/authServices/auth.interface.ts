import { ProcessState } from '@enums/progressState.enum';

export interface User {
  userId: string;
  userName: string;
  email: string;
  dateOfBirth?: string | null | Date;
  country?: string;
  state?: string;
  district?: string;
  postalCode?: string;
  position?: string;
}

export type UserAdditionalFields = Omit<User, 'email' | 'userId' | 'userName'>;
export interface AuthState {
  state: ProcessState;
  user: User;
  signUpError: string | null;
  signInError: string | null;
  partlyRegistered: boolean;
}

export interface UserCredentials {
  username: string;
  email: string;
  password: string;
}

export interface SignIn {
  // email: string,
  username: string;
  password: string;
}

export type AuthorizationParams =
  | ({ grant_type: 'password'; scope: string } & SignIn)
  | { refresh_token: string; grant_type: 'refresh_token' }
  | { token: string };

export interface Token {
  access_token: string;
  expires_in: number;
  id_token: string;
  refresh_token: string;
  token_type: 'Bearer';
}
