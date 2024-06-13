import { Component, OnInit } from '@angular/core';
import { DepartmentService } from '../../Services/Department/department-service.service';
import { LocationService } from '../../Services/Location/location.service';
import { Location } from '../../Models/location';
import { Department } from '../../Models/department';
import { Employee } from '../../Models/employee';
import { EmployeeService } from '../../Services/Employee/employee-service.service';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Role } from '../../Models/role';
import { Router } from '@angular/router';
import { RoleService } from '../../Services/Role/role.service';

@Component({
  selector: 'app-add-role',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-role.component.html',
  styleUrl: './add-role.component.scss',
})
export class AddRoleComponent implements OnInit {
  isDisplayEmployees: boolean = false;
  departments: Department[] = [];
  locations: Location[] = [];
  employees: Employee[] = [];
  employeesSelected: string[] = [];
  roleForm: FormGroup;
  isSubmitted: boolean = false;
  isSuccesfullyAdded: boolean = false;
  postMessage: string = '';
  constructor(
    private departmentService: DepartmentService,
    private locationService: LocationService,
    private employeeService: EmployeeService,
    private router: Router,
    private roleService: RoleService
  ) {
    this.roleForm = new FormGroup({
      roleName: new FormControl('', [Validators.required]),
      departmentId: new FormControl(null),
      locationId: new FormControl(null),
      description: new FormControl(null),
    });
  }
  ngOnInit() {
    this.departmentService.getDepartments().subscribe((value) => {
      this.departments = value;
    });
    this.locationService.getLocations().subscribe((value) => {
      this.locations = value;
    });
    this.employeeService.getEmployeesWithRoleNull('$').subscribe((value) => {
      this.employees = value;
    });
  }
  selectEmployees(e: any) {
    console.log(e.target.value);
    if (e.target.value == null || e.target.value == '') {
      this.employeeService.getEmployeesWithRoleNull('$').subscribe((value) => {
        this.employees = value;
      });
    } else {
      this.employeeService
        .getEmployeesWithRoleNull(e.target.value)
        .subscribe((value) => {
          this.employees = value;
        });
    }
  }
  CheckEmployee(id: string, event: any) {
    if (event.currentTarget.checked == true) {
      this.employeesSelected.push(id);
    } else {
      this.employeesSelected = this.employeesSelected.filter(
        (empId) => empId != id
      );
    }
  }
  MapFormToRole(roleDetails: any) {
    let role: Role = {
      id: null,
      name: roleDetails.roleName,
      departmentid: roleDetails.departmentId,
      description: roleDetails.description,
      departmentName: '',
      locationName: '',
      locationId: roleDetails.locationId,
      roleDeptLocId: 0,
    };
    return role;
  }
  ReturnToRolesPage() {
    this.router.navigate(['/home/roles']);
  }
  vale: any;
  OnSubmit() {
    this.isSubmitted = true;
    if (this.roleForm.valid) {
      let role: Role = this.MapFormToRole(this.roleForm.value);

      this.roleService
        .addRole(role, this.employeesSelected)
        .subscribe((value) => {
          this.isSuccesfullyAdded = true;
          this.postMessage = value;
          setTimeout(() => {
            this.isSuccesfullyAdded = false;
            this.roleForm.reset();
            Object.keys(this.roleForm.controls).forEach((field) => {
              this.roleForm.get(field)!.setErrors(null);
            });
          }, 3000);
        });
    }
  }
}
