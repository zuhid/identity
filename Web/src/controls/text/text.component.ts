import { Component, forwardRef, Input } from "@angular/core";
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from "@angular/forms";
import { BaseControlComponent } from "../baseControl";

@Component({
  selector: 'zc-text',
  standalone: false,
  templateUrl: './text.component.html',
  styleUrl: './text.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextComponent),
      multi: true
    }
  ]
})
export class TextComponent extends BaseControlComponent implements ControlValueAccessor {
  @Input() field!: string; // the field of the model bound to this control
  @Input() label!: string; // label for the field
  @Input() multiline: boolean = false; // label for the field

  // Standard implementation begin
  private _text = "";
  get text(): string {
    return this._text;
  }
  set text(v: string | null) {
    if (this._text !== v) {
      this._text = v || "";
      this.onChangeCallback(v);
    }
  }

  writeValue(v: string) {
    if (this._text !== v) {
      this._text = v;
    }
  }
  // Standard implementation end
}
