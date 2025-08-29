import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// Custom Controls
import { TextComponent } from './text/text.component';
import { PasswordComponent } from './password/password.component';
import { CardComponent } from './card/card.component';
import { ButtonComponent } from './button/button.component';
import { ToastComponent } from './toast/toast.component';

@NgModule({
  declarations: [
    TextComponent,
    PasswordComponent,
    CardComponent,
    ButtonComponent,
    ToastComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
  ],
  exports: [
    TextComponent,
    PasswordComponent,
    CardComponent,
    ButtonComponent,
    ToastComponent
  ],
})
export class ControlsModule { }
