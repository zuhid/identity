import { Component } from '@angular/core';
import { User } from '../../../models';
import { UserService } from '../../../identity';
import { Router } from '@angular/router';

@Component({
  selector: 'nc-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  public model: User = {
    email: 'admin@company.com',
    password: 'P@ssw0rd',
  };


  constructor(private userService: UserService, private router: Router) { }

  async onRegister() {
    var result = await this.userService.register(this.model);
    alert(result);
  }
}
