import { Component, DoCheck, Input, OnInit, ViewChild } from '@angular/core';
import { OperationsBarComponent } from '../operations-bar/operations-bar.component';
import { CommonModule } from '@angular/common';
import { EmployeeInfoComponent } from '../employee-info/employee-info.component';
import { EmployeeService } from '../../Services/employee-service.service';
import { FilterData } from '../../Models/filter-data';
import { Router } from '@angular/router';
import { Employee } from '../../Models/employee';

@Component({
  selector: 'app-employee-body',
  standalone: true,
  templateUrl: './employee-body.component.html',
  styleUrl: './employee-body.component.scss',
  imports: [OperationsBarComponent, CommonModule, EmployeeInfoComponent],
})
export class EmployeeBodyComponent implements DoCheck {
  @ViewChild('employeeInfo') employeeInfo!: EmployeeInfoComponent;
  filterData?: FilterData;
  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) {}
  ngDoCheck() {
    this.employeeService.filterData$.subscribe((value) => {
      this.filterData = value;
    });
  }
  ExportData(value: boolean) {
    if (value) {
      this.employeeInfo.ExportData();
    }
  }
}
