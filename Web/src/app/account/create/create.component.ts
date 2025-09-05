import { Component } from '@angular/core';
import { User } from '../../../models';
import { IdentityService } from '../../../services';
import { Router } from '@angular/router';
import { UserService } from '../../../identity';

@Component({
  selector: 'nc-create',
  standalone: false,
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreateComponent {
  public model: User = {
    email: 'admin@company.com',
    password: 'P@ssw0rd',
  };

  constructor(private userService: UserService, private router: Router) { }

  createAccount() {
    this.userService.create(this.model);
  }
}
