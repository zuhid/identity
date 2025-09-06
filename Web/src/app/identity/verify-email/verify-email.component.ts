import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IdentityService } from '../../../services';
import { User } from '../../../models';
import { UserService } from '../../../identity';

@Component({
  selector: 'nc-verify-email',
  standalone: false,
  templateUrl: './verify-email.component.html',
  styleUrl: './verify-email.component.scss'
})
export class VerifyEmailComponent {
  constructor(private userService: UserService, private route: ActivatedRoute) { }
  async ngOnInit(): Promise<void> {

    this.route.queryParamMap.subscribe(async params => {
      let model: User = {
        email: params.get('email') ?? "",
        emailToken: params.get('emailToken') ?? ""
      };
      var result = await this.userService.verifyEmail(model);
      alert(result);
    });
  }
}
