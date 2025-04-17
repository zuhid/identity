import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ConfigService } from './config.service';
import { Login, LoginResponse } from '../models';
import { TokenService } from './token.service';

@Injectable({ providedIn: 'root' })
export class IdentityService {
  constructor(private apiService: ApiService, private tokenService: TokenService, private configService: ConfigService) { }

  async login(model: Login): Promise<void> {
    let loginResponse: LoginResponse = await this.apiService.post(`${this.configService.identity}/login`, model);
    this.tokenService.setIdentityToken(loginResponse?.identityToken);
  }
}
