import { Component } from '@angular/core';
import { UserService } from '@src/clients';
import { User } from '@src/models';

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

  constructor(private userService: UserService) { }

  async onRegister() {
    var result = await this.userService.register(this.model);
    alert(result);
  }
}
