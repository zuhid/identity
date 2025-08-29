import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ControlsModule } from '../../controls/controls.module';

// Components
import { LoginComponent } from './login/login.component';
import { CreateAccountComponent } from './create-account/create-account.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "**", redirectTo: "login" },
];

@NgModule({
  declarations: [
    LoginComponent,
    CreateAccountComponent
  ],
  imports: [FormsModule, CommonModule, ControlsModule, RouterModule.forChild(routes)],
})
export class IdentityModule { }
