import { Component } from '@angular/core';
import { User } from '../../../models';
import { IdentityService } from '../../../services';

@Component({
  selector: 'nc-tfa',
  standalone: false,
  templateUrl: './tfa.component.html',
  styleUrl: './tfa.component.scss'
})
export class TfaComponent {
  constructor(private identityService: IdentityService) { }

  public model: User = {
    email: "admin@company.com",
    phone: "789-456-1230",
    phoneToken: ""
  };

  async createPhone() {
    var result = await this.identityService.createPhone(this.model);

  }

  async verifyPhone() {
    var result = await this.identityService.verifyPhone(this.model);
  }

}
