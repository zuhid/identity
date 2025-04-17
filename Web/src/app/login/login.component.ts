import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../models';

@Component({
  selector: 'nc-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  public model: Login = {
    username: 'testuser',
    password: 'P@ssw0rd'
  };

  constructor(private router: Router) { }

  async login() {
    this.router.navigate(["admin/user"]);
  }
}
