import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
  selector: 'nc-card',
  standalone: false,
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {
  @Input() header = "";
  @Input() help = false;
  @Input() submit!: string;
  @Output() helpClick = new EventEmitter();
  @Output() submitClick = new EventEmitter();
  cardId = () => "card-" + this.header.replace(" ", "");
}
