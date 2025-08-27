import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ConfigService, TokenService } from '../services';
import { Login, LoginResponse } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: ApiService, private tokenService: TokenService, private configService: ConfigService) { }

  get = (): Observable<any> => this.apiService.getJson(`${this.configService.identity}/user`);

  async login(model: Login) {
    let loginResponse: LoginResponse = await this.apiService.post(`${this.configService.identity}/login`, model);
    this.tokenService.setAuthToken(loginResponse?.authToken);
  }
}
