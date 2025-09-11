import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class ConfigService {
  private _config: any = {};

  async loadConfig(): Promise<void> { this._config = await (await fetch('/config.json')).json(); }

  public get identity() { return this._config.identity; }
}
