import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IEmployee } from '../interfaces/employee.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private backendBaseUrl: string = environment.baseUrl;
  private backendApiVersion: number = environment.backendVersion;

  constructor(private httpClient: HttpClient) { }

  getEmployees = (page: number, pageSize: number = 5): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/employees?term=&page=${page}&pageSize=${pageSize}`;
    return this.httpClient.get(url);
  }

  saveEmployee = (payload: IEmployee): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/employees`;
    return this.httpClient.post(url, payload);
  }

  updateEmployee = (payload: IEmployee): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/employees`;
    return this.httpClient.put(url, payload)
  }

  getEmployeeByEmail = (email: string): Observable<any> => {
    const url: string = `${this.backendBaseUrl}/v${this.backendApiVersion}/employees/${email}`;
    return this.httpClient.get(url);
  }
}
