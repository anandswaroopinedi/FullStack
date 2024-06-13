import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from '../../Models/department';
import { HttpClient } from '@angular/common/http';
import { WebApiUrls } from '../../webapi-urls';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  constructor(private http: HttpClient,private apiUrls:WebApiUrls) {}
  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.apiUrls.departments);
  }
}
