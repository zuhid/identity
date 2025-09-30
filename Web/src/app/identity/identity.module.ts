import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthenticationGuard, AuthorizationGuard } from '@src/guards';
import { ControlsModule } from '@src/controls/controls.module';

// Components
import { UserComponent } from './user/user.component';
import { AccountComponent } from './account/account.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: "register", component: RegisterComponent },
  { path: "login", component: LoginComponent },
  { path: "user", component: UserComponent, canActivate: [AuthenticationGuard], canActivateChild: [AuthorizationGuard] },
  { path: "account", component: AccountComponent, canActivate: [AuthenticationGuard], canActivateChild: [AuthorizationGuard] },
  { path: "**", redirectTo: "login" },
  // old

];

@NgModule({
  declarations: [
    UserComponent,
    AccountComponent,
  ],
  imports: [FormsModule, CommonModule, ControlsModule, RouterModule.forChild(routes)],
})
export class IdentityModule { }
