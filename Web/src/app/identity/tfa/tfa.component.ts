import { Component } from '@angular/core';
import { User } from '../../../models';
import { UserService } from '@src/clients';

@Component({
  selector: 'nc-tfa',
  standalone: false,
  templateUrl: './tfa.component.html',
  styleUrl: './tfa.component.scss'
})
export class TfaComponent {
  constructor(private userService: UserService) { }

  public model: User = {
    email: "admin@company.com",
    phone: "789-456-1230",
    phoneToken: ""
  };

  async createPhone() {
    // var result = await this.userService.createPhone(this.model);

  }

  async verifyPhone() {
    // var result = await this.userService.verifyPhone(this.model);
  }

}
