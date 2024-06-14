import { Routes } from '@angular/router';
import { EmployeeBodyComponent } from './Employee/employee-body/employee-body.component';
import { AddEmployeeComponent } from './Employee/add-employee/add-employee.component';
import { RoleBodyComponent } from './Role/role-body/role-body.component';
import { AddRoleComponent } from './Role/add-role/add-role.component';
import { RoleDetailsComponent } from './Role/role-details/role-details.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { activateGuard } from './Guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    pathMatch: "full",
    redirectTo: 'home',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [activateGuard] ,
    children: [
      { path: '', pathMatch: 'full', component: EmployeeBodyComponent },
      {
        path: 'addemployee',
        component: AddEmployeeComponent,
      },
      {
        path: 'roleDetails/:id',
        component: RoleDetailsComponent,
      },
      {
        path: 'addrole',
        component: AddRoleComponent,
      },
      {
        path: 'roles',
        component: RoleBodyComponent,
      },
      {
        path: 'addemployee',
        component: AddEmployeeComponent,
      },
      {
        path: 'addemployee/:id',
        component: AddEmployeeComponent,
      },
      {
        path: 'employees',
        component: EmployeeBodyComponent,
      },
    ],
  },
];
