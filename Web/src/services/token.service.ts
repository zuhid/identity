import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private _payload: any = {};
  private _signature: any = {};

  constructor() {
    let value = sessionStorage.getItem("identityToken");
    if (value) {
      this.init(JSON.parse(value));
    }
  }

  getIdentityToken(): any {
    let value = sessionStorage.getItem("identityToken");
    return value ? JSON.parse(value) : null;
  }

  setIdentityToken(value: any) {
    sessionStorage.setItem("identityToken", JSON.stringify(value));
    this.init(value);
  }

  private init(value: any) {
    this._payload = JSON.parse(window.atob(value.split(".")[1]));
    this._signature = value.split(".")[2];
  }

  public isAuthenticated = () => this._signature != null && this._signature.length > 0;

  public fullName = () => `${this._payload.firstName ?? ""} ${this._payload.lastName ?? ""}`.trim();

}
