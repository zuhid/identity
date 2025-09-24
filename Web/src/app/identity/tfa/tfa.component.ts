import { Component } from '@angular/core';
import { User } from '../../../models';
import { UserService } from '@src/clients';
import { ToastService } from '@src/services';

@Component({
  selector: 'zc-tfa',
  standalone: false,
  templateUrl: './tfa.component.html',
  styleUrl: './tfa.component.scss'
})
export class TfaComponent {
  constructor(private userService: UserService, private toastService: ToastService) { }

  public model: User = {
    email: "admin@company.com",
    emailToken: "",
    phone: "789-456-1230",
    phoneToken: "",
  };
  public tfaType: string = "";
  public qrdata: string = "";

  async emailSendToken() {
    var result = await this.userService.emailSendToken(this.model);
    if (result) {
      this.tfaType = "email";
      this.toastService.success("Token sent to you email");
    }
  }

  async emailVerifyToken() {
    var result = await this.userService.emailVerifyToken(this.model);
    this.toastService.success("Login Succesful");
  }

  async phoneSendToken() {
    var result = await this.userService.phoneSendToken(this.model);
    if (result) {
      this.tfaType = "email";
      this.toastService.success("Token sent to you email");
    }
  }

  async phoneVerifyToken() {
    var result = await this.userService.phoneVerifyToken(this.model);
    this.toastService.success("Login Succesful");
  }

  async generateQrCodeUri() {
    var result = await this.userService.generateQrCodeUri(this.model);
    this.qrdata = result.tfaToken;
  }

  async verifyQrCode() {
    var result = await this.userService.verifyQrCode(this.model);
    this.toastService.success(result);
  }
}
