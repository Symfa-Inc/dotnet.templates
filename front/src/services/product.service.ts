import { User } from '@pages/main/auth/auth.interface';
import { AxiosResponse } from 'axios';
import { apiHttpService } from './apiHttp.service';

export class ProductService {
  static async getProduct(): Promise<AxiosResponse<User>> {
    return apiHttpService.get('/product');
  }
}
