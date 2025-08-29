import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ConfigService } from './config.service';
import { User, Login, LoginResponse } from '../models';
import { TokenService } from './token.service';

@Injectable({ providedIn: 'root' })
export class IdentityService {
  constructor(private apiService: ApiService, private tokenService: TokenService, private configService: ConfigService) { }

  async createAccount(model: User): Promise<void> {
    let response = await this.apiService.post(`${this.configService.identity}/user`, model);
  }


  async login(model: Login): Promise<void> {
    let loginResponse: LoginResponse = await this.apiService.post(`${this.configService.identity}/user/login`, model);
    this.tokenService.setAuthToken(loginResponse?.authToken);
  }
  async smsToken(model: Login) {
    await this.apiService.put(`${this.configService.identity}/user/smsToken`, model);
  }
  async emailToken(model: Login) {
    await this.apiService.put(`${this.configService.identity}/user/emailToken`, model);
  }
}
