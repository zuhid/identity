import { Injectable } from '@angular/core';
import { ApiService, ConfigService } from '../../services';
import { User } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = '';

  constructor(private apiService: ApiService, private configService: ConfigService) {
    this.baseUrl = `${this.configService.identity}/user`;
  }

  async register(model: User) { return await this.apiService.post(`${this.baseUrl}/register`, model); }
  verifyEmail = async (model: User) => await this.apiService.put(`${this.baseUrl}/verifyEmail`, model);
  login = async (model: User) => await this.apiService.put(`${this.baseUrl}/login`, model);
}
