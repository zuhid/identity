import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class IndexedDbService {
  private request: any;
  db: any;
  store = "storage";

  constructor() {
    this.init().then();
  }

  private async init() {
    this.request = indexedDB.open("app", 1);
    this.request.onsuccess = (event: any) => this.db = event.target.result;
    this.request.onupgradeneeded = (event: any) => {
      this.db = event.target.result;
      this.db.createObjectStore(this.store, { keyPath: "url" });
    }
    // this.request.onerror = (event: any) => console.log(JSON.stringify(event));
  }

  async get(key: string): Promise<any> {
    if (!this.db) {
      await this.init();
    }
    if (this.db) {
      let result = this.db.transaction(this.store, "readwrite").objectStore(this.store)
      result.onsuccess = (event: any) => event?.target?.result ?? null;
      result.get(key);
      return result;
    }
  }

  async set(data: object) {
    if (!this.db) {
      await this.init();
    }
    if (this.db) {
      this.db.transaction(this.store, "readwrite").objectStore(this.store).put(data);
    }
  }
}

