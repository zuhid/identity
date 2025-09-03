import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IdentityService } from '../../../services';
import { User } from '../../../models';

@Component({
  selector: 'nc-verify-email',
  standalone: false,
  templateUrl: './verify-email.component.html',
  styleUrl: './verify-email.component.scss'
})
export class VerifyEmailComponent {
  constructor(private identityService: IdentityService, private route: ActivatedRoute) { }
  async ngOnInit(): Promise<void> {

    this.route.queryParamMap.subscribe(async params => {
      let model: User = {
        email: params.get('email') ?? "",
        emailToken: params.get('emailToken') ?? ""
      };
      var result = await this.identityService.verifyEmail(model);
      alert(result);
    });
  }
}
