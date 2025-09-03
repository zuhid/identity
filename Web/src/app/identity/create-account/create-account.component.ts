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
    emailToken: ""
  };

  constructor(private identityService: IdentityService, private router: Router) { }

  createAccount() {
    // this.identityService.createAccount(this.model).then(() => this.router.navigate(["/identity/verify-email"]));
    this.identityService.createAccount(this.model);
  }

  async verifyEmail() {
    var result = await this.identityService.verifyEmail(this.model);
    alert(result);
  }
}
