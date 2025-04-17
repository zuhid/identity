import { Injectable } from "@angular/core";
import { Observable, shareReplay } from "rxjs";
import { IndexedDbService } from "./indexed-db.service";
import { ToastService } from "./toast.service";
import { TokenService } from "./token.service";
import { environment } from "../environments/environment";

@Injectable({ providedIn: "root" })
export class ApiService {
  private cacheLength: number = 10 * 1000;

  constructor(private tokenService: TokenService, private toastService: ToastService, private indexedDbService: IndexedDbService) { }

  /**
   * @param url : The url to call
   * @param errorMessage : if the url fails, a custom message to display
   * @returns json object if succesful, otherwise null
   */
  getJson = (url: string, interval?: number, errorMessage?: string) =>
    this.get("application/json", url, interval, errorMessage).pipe((res) => res, shareReplay());

  getCsv = (url: string, interval?: number, errorMessage?: string) =>
    this.get("text/csv", url, interval, errorMessage).pipe((res) => res, shareReplay());

  getAsset = async (url: string): Promise<any> => {
    // return this.fetchApi("get", `${environment.webBaseUrl}/assets/${url}`, "");
    const response = await fetch(`${environment.webBaseUrl}/assets/${url}`);
    return response.ok ? response.text() : null;
  };

  /**
   * @param url : The url to call
   * @param errorMessage : if the url fails, a custom message to display
   * @param model : the object to add
   * @returns json object if succesful, otherwise null
   */
  post = async (url: string, model: any, errorMessage?: string): Promise<any> =>
    await this.fetchApi("post", url, "application/json", errorMessage, model);

  /**
   * @param url : The url to call
   * @param errorMessage : if the url fails, a custom message to display
   * @param model : the object to update
   * @returns json object if succesful, otherwise null
   */
  put = async (url: string, model: any, errorMessage?: string): Promise<any> =>
    await this.fetchApi("put", url, "application/json", errorMessage, model);

  /**
   * @param url : The url to call
   * @param errorMessage : if the url fails, a custom message to display
   * @returns json object if succesful, otherwise null
   */
  delete = async (url: string, errorMessage?: string): Promise<any> => await this.fetchApi("delete", url, "application/json", errorMessage, null);

  private get(contentType: string, url: string, interval?: number, errorMessage?: string): Observable<any> {
    return new Observable<any>((subscriber) => {
      this.fetchApi("get", url, contentType, errorMessage, null).then((data) => {
        this.indexedDbService.set({ url: url, expDate: Date.now() + this.cacheLength, data: data });
        return subscriber.next(data);
      }); // call the api right away
      if (interval && interval > 0) {
        // if interval is set then call it
        setInterval(async () => {
          this.indexedDbService.get(url).then((n) => {
            if (n && n.expDate && n.expDate > Date.now()) {
              subscriber.next(n.data);
            } else {
              this.fetchApi("get", url, contentType, errorMessage, null).then((data) => {
                this.indexedDbService.set({ url: url, expDate: Date.now() + this.cacheLength, data: data });
                return subscriber.next(data);
              });
            }
          });
        }, interval);
      }
    });
  }

  private async fetchApi(method: string, url: string, contentType: string, errorMessage?: string, model?: any): Promise<any> {
    let identityToken = await this.tokenService.getIdentityToken();
    return fetch(url, {
      method: method,
      headers: {
        "Content-Type": contentType,
        Authorization: `Bearer ${identityToken}`,
      },
      body: model ? JSON.stringify(model) : null,
    })
      .then(async (response) => {
        if (!response || !response.ok) {
          throw response; // let catch handle not 'ok' response
        }
        switch (contentType) {
          case "application/json":
            return response.json().then((m) => m);
          case "text/csv":
            return response.text().then((m) =>
              m
                .trim()
                .split("\n")
                .map((v) => v.split(","))
            );
          default:
            return response.text().then((m) => m);
        }
      })
      .catch(() => {
        this.toastService.error(errorMessage ? errorMessage : `Failed to ${method} ${url}`);
        return null;
      });
  }
}
