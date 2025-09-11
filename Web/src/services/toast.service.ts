import { Injectable } from '@angular/core';
import { Observable, Subject } from "rxjs";
import { Toast } from "../models";

@Injectable({ providedIn: "root" })
export class ToastService {
  private _toastSubject = new Subject<Toast>();
  toastEvent: Observable<Toast>;

  constructor() {
    this.toastEvent = this._toastSubject.asObservable();
  }

  error = (message: string) => this._toastSubject.next({ type: "error", message: message });
  info = (message: string) => this._toastSubject.next({ type: "info", message: message });
  secondary = (message: string) => this._toastSubject.next({ type: "secondary", message: message });
  success = (message: string) => this._toastSubject.next({ type: "success", message: message });
  warning = (message: string) => this._toastSubject.next({ type: "warning", message: message });
}
