import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable, Subject, delay, map, of } from 'rxjs';
import { Employee } from '../../Models/employee';
import { FilterData } from '../../Models/filter-data';
import axios from 'axios';
import { WebApiUrls } from '../../webapi-urls';
@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  employees: Employee[] = [];
  filterData$: Subject<FilterData>;
  constructor(private http: HttpClient,private apiUrls:WebApiUrls) {
    this.filterData$ = new Subject<FilterData>();
  }
  postEmployeeData(employee: Employee): Observable<boolean> {
    console.log('post method called');
    return this.http.post<boolean>(this.apiUrls.addEmployee, employee);
  }
  getEmployeeData(pageNumber:number,pageSize:number): Observable<Employee[]> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('recordsPerPage', pageSize);
    return this.http.get<Employee[]>(this.apiUrls.getAllEmployees,{params});
  }
  getEmployeesWithRoleNull(name: string): Observable<Employee[]> {
    return this.http.get<Employee[]>(
      this.apiUrls.getEmployeesWithRolenull + name
    );
  }
  deleteEmployeeData(employeeIds: string[],pageNumber:number,pageSize:number): Observable<Employee[]> {
    const params = new HttpParams()
    .set('pageNumber', pageNumber)
    .set('recordsPerPage', pageSize);
    return this.http.delete<Employee[]>(this.apiUrls.deleteEmployees, {
      body: employeeIds,
      params:params,
    });
  }
  updateEmployee(employee: Employee) {
    return this.http.post<boolean>(this.apiUrls.updateEmployee, employee);
  }
  getEmployeeIds(): Observable<string[]> {
    return this.http.get<string[]>(this.apiUrls.getAllIds);
  }
  applyFilters(inputFilters: FilterData,pageNumber:number,pageSize:number): Observable<Employee[]> {
    const params = new HttpParams()
    .set('pageNumber', pageNumber)
    .set('recordsPerPage', pageSize);
    return this.http.post<Employee[]>(
      this.apiUrls.filterEmployees,
      inputFilters,{params}
    );
  }
  applySorting(property: string, order: string,pageNumber:number,pageSize:number): Observable<Employee[]> {
    const params = new HttpParams()
      .set('property', property)
      .set('order', order)
      .set('pageNumber', pageNumber)
      .set('recordsPerPage', pageSize);
    return this.http.get<Employee[]>(this.apiUrls.sorting, { params });
  }
  getEmployeeById(id: string): Observable<Employee> {
    console.log(id);
    return this.http.get<Employee>(this.apiUrls.getEmployeeById + id);
  }
  getEmployeesByRoleId(id: number): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrls.getEmployeeByRoleId + id);
  }
  getEmployeeCount():Observable<number>{
    return this.http.get<number>(this.apiUrls.employeeCount);
  }
  ngOnDestroy() {
    this.filterData$.unsubscribe();
  }
}
