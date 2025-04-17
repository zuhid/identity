import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ConfigService } from '../services';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: ApiService, private configService: ConfigService) { }

  get = (): Observable<any> => this.apiService.getJson(`${this.configService.identity}/user`);
}
