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
  login = async (model: User) => await this.apiService.put(`${this.baseUrl}/login`, model);

  async emailVerify(model: User) { return await this.apiService.put(`${this.baseUrl}/emailVerify`, model); }
  async emailSendToken(model: User) { return await this.apiService.put(`${this.baseUrl}/emailSendToken`, model); }
  async emailVerifyToken(model: User) { return await this.apiService.put(`${this.baseUrl}/emailVerifyToken`, model); }

  async phoneVerify(model: User) { return await this.apiService.put(`${this.baseUrl}/phoneVerify`, model); }
  async phoneSendToken(model: User) { return await this.apiService.put(`${this.baseUrl}/phoneSendToken`, model); }
  async phoneVerifyToken(model: User) { return await this.apiService.put(`${this.baseUrl}/phoneVerifyToken`, model); }
  async generateQrCodeUri(model: User) { return await this.apiService.put(`${this.baseUrl}/generateQrCodeUri`, model); }
  async verifyQrCode(model: User) { return await this.apiService.put(`${this.baseUrl}/verifyQrCode`, model); }
}
