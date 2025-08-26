import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../models';
import { IdentityService } from '../../services';

@Component({
  selector: 'nc-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public model: Login = {
    username: 'admin@company.com',
    password: 'P@ssw0rd'
  };

  constructor(private identityService: IdentityService, private router: Router) { }

  async login() {
    this.identityService.login(this.model).then(() => this.router.navigate(["/admin/user"]));
  }
}
