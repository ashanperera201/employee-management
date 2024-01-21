import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserLogin, IUserRegister } from '../interfaces/user.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private backendBaseUrl: string = environment.baseUrl;
  private backendApiVersion: number = environment.backendVersion;

  constructor(private httpClient: HttpClient) { }

  userLogin = (payload: IUserLogin): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/users/auth/login`;
    return this.httpClient.post(url, payload)
  }

  userRegistration = (payload: IUserRegister): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/users/auth/register`;
    debugger
    return this.httpClient.post(url, payload);
  }

  getUserById = (id: string): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/users/${id}`;
    return this.httpClient.get(url);
  }

  updateUserProfile = (payload: IUserLogin): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/users/auth/update`;
    return this.httpClient.put(url, payload);
  }

  storeUserAccessToken = (token: string): void => {
    localStorage.setItem('access_token', token);
  }

  getAccessToken = (): string | null => {
    return localStorage.getItem('access_token')
  }
}
