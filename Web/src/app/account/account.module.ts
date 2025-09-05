import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ControlsModule } from '../../controls/controls.module';
import { CreateComponent } from './create/create.component';
import { LoginComponent } from './login/login.component';
import { TfaComponent } from './tfa/tfa.component';
import { VerifyComponent } from './verify/verify.component';
import { AuthenticationGuard, AuthorizationGuard } from '../../guards';

const routes: Routes = [
  { path: "create", component: CreateComponent },
  { path: "login", component: LoginComponent },
  { path: "verify", component: VerifyComponent },
  {
    path: "tfa", component: TfaComponent, canActivate: [AuthenticationGuard], canActivateChild: [AuthorizationGuard],
  },
  { path: "**", redirectTo: "login" }
];

@NgModule({
  declarations: [
    CreateComponent,
    LoginComponent,
    TfaComponent,
    VerifyComponent,
  ],
  imports: [FormsModule, CommonModule, ControlsModule, RouterModule.forChild(routes)],
})
export class AccountModule { }
