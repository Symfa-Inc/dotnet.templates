import {
  User, UserAdditionalFields,
} from '@features/auth/auth.interface';
import { AxiosResponse } from 'axios';
import { apiHttpService } from './api-http.service';

export class ProfileService {
  static async profile(): Promise<AxiosResponse<User>> {
    return apiHttpService.get('/userprofile');
  }

  static async updateProfile(profileData: UserAdditionalFields): Promise<AxiosResponse<User>> {
    return apiHttpService.put('/userprofile', profileData);
  }

  static async createProfile(data: UserAdditionalFields): Promise<AxiosResponse<any>> {
    return apiHttpService.post<UserAdditionalFields>('/userprofile', data);
  }
}
