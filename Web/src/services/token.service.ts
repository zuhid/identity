import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private _payload: any = {};
  private _signature: any = {};

  constructor() {
    let value = sessionStorage.getItem("authToken");
    if (value) {
      this.init(JSON.parse(value));
    }
  }

  getAuthToken(): any {
    let value = sessionStorage.getItem("authToken");
    return value ? JSON.parse(value) : null;
  }

  setAuthToken(value: any) {
    sessionStorage.setItem("authToken", JSON.stringify(value));
    this.init(value);
  }

  private init(value: any) {
    this._payload = JSON.parse(window.atob(value.split(".")[1]));
    this._signature = value.split(".")[2];
  }

  public isAuthenticated = () => this._signature != null && this._signature.length > 0;

  public fullName = () => `${this._payload.firstName ?? ""} ${this._payload.lastName ?? ""}`.trim();

}
