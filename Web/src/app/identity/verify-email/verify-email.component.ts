import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '@src/clients';
import { User } from '@src/models';

@Component({
  selector: 'zc-verify-email',
  standalone: false,
  templateUrl: './verify-email.component.html',
  styleUrl: './verify-email.component.scss'
})
export class VerifyEmailComponent {
  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) { }
  async ngOnInit(): Promise<void> {

    this.route.queryParamMap.subscribe(async params => {
      let model: User = {
        email: params.get('email') ?? "",
        emailToken: params.get('emailToken') ?? ""
      };
      var result = await this.userService.verifyEmail(model);
      if (result) {
        this.router.navigate(["/identity/login"]);
      }
      else {
        alert(result);
      }
    });
  }
}
