import { User, UserAdditionalFields } from '@services/authServices/auth.interface';
import { AxiosResponse } from 'axios';
import { apiHttpService } from './apiHttp.service';
import authHttpService from './authServices/authHttp.service';

export class ProfileService {
  static async profile(): Promise<AxiosResponse<User>> {
    return authHttpService.get('/userinfo');
  }

  static async updateProfile(profileData: UserAdditionalFields): Promise<AxiosResponse<User>> {
    return apiHttpService.put('/userprofile', profileData);
  }

  static async createProfile(data: UserAdditionalFields): Promise<AxiosResponse<any>> {
    return apiHttpService.post<UserAdditionalFields>('/userprofile', data);
  }
}
