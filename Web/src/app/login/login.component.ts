import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'nc-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private router: Router) { }

  async login() {
    this.router.navigate(["admin/user"]);
  }
}
