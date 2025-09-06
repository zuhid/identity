import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ConfigService, TokenService } from '../services';
import { Login, LoginResponse, User } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = '';

  constructor(private apiService: ApiService, private configService: ConfigService) {
    this.baseUrl = `${this.configService.identity}/user`;
  }

  async register(model: User) {
    return await this.apiService.post(`${this.baseUrl}/register`, model);
  }

  async verifyEmail(model: User) {
    return await this.apiService.put(`${this.baseUrl}/verifyEmail`, model);
  }

}
