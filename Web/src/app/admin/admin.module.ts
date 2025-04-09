import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ControlsModule } from '../../controls/controls.module';

// Components
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path: "user", component: UserComponent },
  { path: "**", redirectTo: "user" },
];

@NgModule({
  declarations: [UserComponent],
  imports: [FormsModule, CommonModule, ControlsModule, RouterModule.forChild(routes)],
})
export class AdminModule { }
