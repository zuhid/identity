import { Component } from '@angular/core';
import { UserService } from '@src/clients';
import { User } from '@src/models';
import { ToastService } from '@src/services';

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

  constructor(private userService: UserService, private toastService: ToastService) { }

  async onRegister() {
    var success = await this.userService.register(this.model);
    if (success) {
      this.toastService.success("Registration successful! Please check your email for verification link.");
    }
  }
}
