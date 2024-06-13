import { Component, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { DepartmentService } from '../../Services/Department/department-service.service';
import { LocationService } from '../../Services/Location/location.service';
import { Department } from '../../Models/department';
import { Location } from '../../Models/location';
import { FilterDropdownComponent } from '../../Shared Components/filter-dropdown/filter-dropdown.component';
import { FilterControlButtonsComponent } from '../../Shared Components/filter-control-buttons/filter-control-buttons.component';
import { RoleService } from '../../Services/Role/role.service';
import { FilterData } from '../../Models/filter-data';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-role-operations',
  standalone: true,
  templateUrl: './role-operations.component.html',
  styleUrl: './role-operations.component.scss',
  imports: [FilterDropdownComponent, FilterControlButtonsComponent, RouterLink],
})
export class RoleOperationsComponent {
  @ViewChild('locationFilter') locationFilter?: FilterDropdownComponent;
  @ViewChild('departmentFilter') departmentFilter?: FilterDropdownComponent;
  departments?: Department[];
  locations?: Location[];
  selectedDepartments: number[] = [];
  selectedLocations: number[] = [];
  deptSubscription?: Subscription;
  locSubscription?: Subscription;
  filter1: string = 'Department';
  filter2: string = 'Location';
  isLocFilterOptionsSelected: boolean = false;
  isDeptFilterOptionsSelected: boolean = false;
  constructor(
    private departmentService: DepartmentService,
    private locationService: LocationService,
    private roleService: RoleService
  ) {
    this.deptSubscription = this.departmentService
      .getDepartments()
      .subscribe((departmentData) => {
        this.departments = departmentData;
        console.log(this.departments);
      });
    this.locSubscription = this.locationService
      .getLocations()
      .subscribe((locationData) => {
        this.locations = locationData;
      });
  }
  LocationFilterApplied(isSelected: boolean) {
    if (isSelected) {
      this.isLocFilterOptionsSelected = true;
    } else {
      this.isLocFilterOptionsSelected = false;
    }
  }
  DepartmentFilterApplied(isSelected: boolean) {
    if (isSelected) {
      this.isDeptFilterOptionsSelected = true;
    } else {
      this.isDeptFilterOptionsSelected = false;
    }
  }
  filterApply(isApplied: boolean) {
    if (isApplied) {
      var inputFilters: FilterData = new FilterData();
      inputFilters.Locations = this.locationFilter!.selectedFiltersIdsArray();
      inputFilters.Departments =
        this.departmentFilter!.selectedFiltersIdsArray();
      console.log(inputFilters);
      this.roleService.filterData$.next(inputFilters);
    }
  }
  filterReset(isReset: boolean) {
    if (isReset) {
      var inputFilters: FilterData = new FilterData();
      this.locationFilter!.reset();
      this.departmentFilter!.reset();
      this.roleService.filterData$.next(inputFilters);
    }
  }
}
