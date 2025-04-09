import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// Custom Controls
import { TextComponent } from './text/text.component';
import { CardComponent } from './card/card.component';
import { ButtonComponent } from './button/button.component';

@NgModule({
  declarations: [
    TextComponent,
    CardComponent,
    ButtonComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
  ],
  exports: [
    TextComponent,
    CardComponent,
    ButtonComponent
  ],
})
export class ControlsModule { }
