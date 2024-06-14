import {
  Component,
  OnDestroy,
  Input,
  OnChanges,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { EmployeeService } from '../../Services/Employee/employee-service.service';
import { Employee } from '../../Models/employee';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { FilterData } from '../../Models/filter-data';
import { RouterLink } from '@angular/router';
import { FormatNullToNonePipe } from '../../Shared Components/Custom Pipes/format-null-to-none.pipe';
import * as XLSX from 'xlsx';
import { PaginationComponent } from "../../Shared Components/Components/pagination/pagination.component";
@Component({
    selector: 'app-employee-info',
    standalone: true,
    templateUrl: './employee-info.component.html',
    styleUrl: './employee-info.component.scss',
    imports: [CommonModule, RouterLink, FormatNullToNonePipe, PaginationComponent]
})
export class EmployeeInfoComponent implements OnChanges, OnDestroy {
  @Input() filterData?: FilterData;
  subscribtion?: Subscription;
  employeeData: Employee[] = [];
  checkAllEmployees: boolean = false;
  checkedEmployeesCount: number = 0;
  selectedEmployeesDelete: string[] = [];
  isShowOptionMenu: boolean = false;
  selectedPageNumber:number=1;
  pageSize:number=3;
  @ViewChild('headCheckBoxChecked') headCheckBoxCheckedRef?: ElementRef;
  isheadCheckBoxChecked: boolean = false;
  pageNumbers:number=0;
  constructor(private employeeService: EmployeeService) {}
  ngOnInit() {
    this.employeeService.getEmployeeCount().subscribe((value)=>{
      this.pageNumbers=Math.ceil(value/this.pageSize)
    })
    this.employeeService.getEmployeeData(this.selectedPageNumber,this.pageSize).subscribe((employees) => {
      this.employeeData = employees;
    },
    (error)=>{console.log(error['status']);
  console.log(error['message'])}
    );
    console.log(this.employeeData);
  }
  ngOnChanges() {
    if (this.filterData != undefined) {
      this.employeeService.applyFilters(this.filterData,this.selectedPageNumber,this.pageSize).subscribe({
        next: (employees) => {
          this.employeeData = employees;
        },
        error: (error) => console.error('error', error),
      });
    }
  }
  checkFiltersApplied() {
    if (
      this.filterData?.Locations.length ||
      this.filterData?.Departments.length ||
      this.filterData?.Statuses.length
    ) {
      return false;
    }
    return true;
  }
  selectOne(id: string, event: any) {
    if (event.currentTarget.checked == true) {
      this.checkedEmployeesCount += 1;
      this.selectedEmployeesDelete.push(id);
    } else {
      this.checkedEmployeesCount -= 1;
      this.selectedEmployeesDelete = this.selectedEmployeesDelete.filter(
        (empId) => empId != id
      );
      if (this.headCheckBoxCheckedRef) {
        this.headCheckBoxCheckedRef.nativeElement.currentTarget.style.checked =
          false;
      }
    }
  }
  selectAll() {
    this.checkAllEmployees = !this.checkAllEmployees;
    const checkboxes = document.querySelectorAll(
      '.check-box'
    ) as NodeListOf<HTMLInputElement>;
    for (let i = 0; i < checkboxes.length; i++) {
      checkboxes[i].checked = this.checkAllEmployees;
      if (this.checkAllEmployees == true) this.checkedEmployeesCount += 1;
      else this.checkedEmployeesCount -= 1;
    }
    checkboxes.forEach(
      (checkbox) => (checkbox.checked = this.checkAllEmployees)
    );
  }
  deleteEmployees() {
    this.employeeService
      .deleteEmployeeData(this.selectedEmployeesDelete,this.selectedPageNumber,this.pageSize)
      .subscribe((employees) => {
        this.employeeData = employees;
      });
      this.checkedEmployeesCount=0;
  }
  sortTable(properety: string, order: string) {
    this.employeeService
      .applySorting(properety, order,this.selectedPageNumber,this.pageSize)
      .subscribe((employees) => {
        this.employeeData = employees;
      });
  }
  showMenuOptions(index: number) {
    const ellipsisRef = document.getElementsByClassName('menu-options')[
      index
    ] as HTMLInputElement;
    ellipsisRef.style.display =
      ellipsisRef.style.display == 'block' ? 'None' : 'block';
  }
  deleteEmployeeUsingEllipsis(id: string) {
    var arr: string[] = [];
    arr.push(id);
    this.employeeService.deleteEmployeeData(arr,this.selectedPageNumber,this.pageSize).subscribe((employees) => {
      this.employeeData = employees;
    });
  }
  ExportData() {
    const filteredEmployeeData = this.employeeData.map(employee => {
      const { profileImage,jobTitleId,departmentId,locationId,statusId,projectId, ...rest } = employee;
      return rest;
    });
    const workSheet:XLSX.WorkSheet = XLSX.utils.json_to_sheet(filteredEmployeeData);
    const workBook:XLSX.WorkBook={Sheets:{'employees':workSheet}, SheetNames:['employees']};
    XLSX.writeFile(workBook,'employees.xlsx');
  }
  choosePageNumber(pageNumber:number)
  {
    this.selectedPageNumber=pageNumber;
    if(this.filterData!=undefined)
    {
      this.employeeService.applyFilters(this.filterData,this.selectedPageNumber,this.pageSize).subscribe({
        next: (employees) => {
          this.employeeData = employees;
        },
        error: (error) => console.error('error', error),
      });
    }
    else{
      this.employeeService.getEmployeeData(pageNumber,this.pageSize).subscribe((value)=>{
        this.employeeData=value;
      })
    }
  }
  ngOnDestroy(): void {
    this.subscribtion?.unsubscribe();
  }
}
