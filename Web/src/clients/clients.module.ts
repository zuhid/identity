import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../identity';

@NgModule({
  declarations: [UserService],
  imports: [CommonModule],
  exports: [UserService]
})
export class ClientsModule { }
