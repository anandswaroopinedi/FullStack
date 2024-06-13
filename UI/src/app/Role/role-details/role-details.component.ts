import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../Services/employee-service.service';
import { Employee } from '../../Models/employee';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-role-details',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './role-details.component.html',
  styleUrl: './role-details.component.scss',
})
export class RoleDetailsComponent implements OnInit {
  employees: Employee[] = [];
  constructor(
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute
  ) {}
  ngOnInit() {
    this.activatedRoute.params.subscribe((params) => {
      const id: number = params['id'];
      console.log(id);
      if (id != undefined && id > 0) {
        this.employeeService.getEmployeesByRoleId(id).subscribe((value) => {
          this.employees = value;
        });
      }
    });
  }
}
