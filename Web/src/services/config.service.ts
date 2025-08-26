import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class ConfigService {
  private _config: any = {};

  public load() {
    fetch("/assets/config.json").then((res) => res.json().then((data) => (this._config = data)));
  }

  public get identity() {
    return "http://localhost:5215";
    // return this._config.identity;
  }
}
