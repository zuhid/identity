import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '@src/clients';
import { User } from '@src/models';
import { ToastService } from '@src/services';

@Component({
  selector: 'zc-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  public model: User = {
    email: 'admin@company.com',
    password: 'P@ssw0rd',
    confirmPassword: 'P@ssw0rd',
  };

  constructor(private userService: UserService, private toastService: ToastService, private router: Router) { }

  async register() {
    if (this.validateModel()) {
      var success = await this.userService.register(this.model);
      if (success) {
        this.toastService.success("Registration successful! Please check your email for verification link.");
      }
    }
  }

  async goToLogin() { this.router.navigate(["/identity/login"]);}

  validateModel(): boolean {
    var isValid = true;
    if (!this.model.email || !this.model.password || !this.model.confirmPassword) {
      this.toastService.error("Email, Password, and Confirm Password are required.");
      isValid = false;
    }
    if (this.model.password !== this.model.confirmPassword) {
      this.toastService.error("Password and Confirm Password do not match.");
      isValid = false;
    }
    return isValid;
  }
}
