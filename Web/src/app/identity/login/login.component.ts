import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '@src/clients';
import { Login } from '@src/models';
import { TokenService } from '@src/services';

@Component({
  selector: 'nc-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public model: Login = {
    email: 'admin@company.com',
    password: 'P@ssw0rd',
    tfaToken: ''
  };

  constructor(private userService: UserService, private tokenService: TokenService, private router: Router) { }

  async login() {
    let loginResponse = await this.userService.login(this.model);
    this.tokenService.setAuthToken(loginResponse?.authToken);
    this.router.navigate(["/admin/user"]);
  }
  // async smsToken() { this.identityService.smsToken(this.model); }
  // async emailToken() { this.identityService.emailToken(this.model); }

  // async createPhone(model: User): Promise<any> {return await this.apiService.put(`${this.configService.identity}/user/createPhone`, model);}
  // async verifyPhone(model: User): Promise<any> {return await this.apiService.put(`${this.configService.identity}/user/verifyPhone`, model);}
  // async login(model: Login): Promise<void> {
  //   let loginResponse: LoginResponse = await this.apiService.post(`${this.configService.identity}/user/login`, model);
  //   this.tokenService.setAuthToken(loginResponse?.authToken);
  // }
}
