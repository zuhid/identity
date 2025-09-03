import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ControlsModule } from '../../controls/controls.module';

// Components
import { LoginComponent } from './login/login.component';
import { CreateAccountComponent } from './create-account/create-account.component';
import { TfaComponent } from './tfa/tfa.component';
// import { VerifyEmailComponent } from './verify-email/verify-email.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  // { path: "tfa", component: TfaComponent },
  { path: "**", redirectTo: "login" },
];

@NgModule({
  declarations: [
    LoginComponent,
    CreateAccountComponent,
    TfaComponent,
    // VerifyEmailComponent
  ],
  imports: [FormsModule, CommonModule, ControlsModule, RouterModule.forChild(routes)],
})
export class IdentityModule { }
