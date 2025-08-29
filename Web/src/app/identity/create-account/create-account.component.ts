import { Component } from '@angular/core';
import { User } from '../../../models';
import { IdentityService } from '../../../services';
import { Router } from '@angular/router';

@Component({
  selector: 'nc-create-account',
  standalone: false,
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.scss'
})
export class CreateAccountComponent {
  public model: User = {
    email: 'admin@company.com',
    password: 'P@ssw0rd',
    phone: "789-456-1230",
  };

  constructor(private identityService: IdentityService, private router: Router) { }

  submit() {
    // this.identityService.login(this.model).then(() => this.router.navigate(["/admin/user"]));
    this.identityService.createAccount(this.model).then(() => this.router.navigate(["/admin/user"]));
  }
}
