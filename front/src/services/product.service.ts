import {
  User,
} from '@features/auth/auth.interface';
import { AxiosResponse } from 'axios';
import { apiHttpService } from './api-http.service';

export class ProductService {
  static async getProduct(): Promise<AxiosResponse<User>> {
    return apiHttpService.get('/product');
  }
}
