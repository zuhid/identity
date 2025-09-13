import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'zc-button',
  standalone: false,
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {
  @Input() id!: string; // the id of the button
  @Input() text!: string; // the text of the button
  @Output() onclick = new EventEmitter();
}
