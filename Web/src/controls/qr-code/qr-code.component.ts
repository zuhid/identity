import { Component, Input, ViewChild, ElementRef, OnChanges } from '@angular/core';
import * as QRCode from 'qrcode';

@Component({
  selector: 'zc-qr-code',
  standalone: false,
  templateUrl: './qr-code.component.html',
  styleUrls: ['./qr-code.component.scss']
})
export class QrCodeComponent implements OnChanges {
  @Input() text: string = "";
  @ViewChild('canvas', { static: true }) canvas!: ElementRef;

  ngOnChanges() {
    QRCode.toCanvas(this.canvas.nativeElement, this.text, {
      width: 256,
      errorCorrectionLevel: 'H'
    });
  }
}
