import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
  selector: 'nc-card',
  standalone: false,
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {
  @Input() header = "";
  @Input() saveText: string = "Save";
  @Output() helpClick = new EventEmitter();
  @Output() saveClick = new EventEmitter();
  cardId = () => "card-" + this.header.replace(" ", "");

  displayFooter = () => this.saveClick.observed;
}
