import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Role } from '../../Models/role';
import { RoleService } from '../../Services/Role/role.service';
import { FilterData } from '../../Models/filter-data';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-role-info',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './role-info.component.html',
  styleUrl: './role-info.component.scss',
})
export class RoleInfoComponent implements OnChanges {
  @Input() filterData?: FilterData;
  roles: Role[] = [];
  constructor(private roleService: RoleService) {
    roleService.getRoles().subscribe((value) => {
      this.roles = value;
      console.log(value);
    });
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (this.filterData) {
      this.roleService.applyFilters(this.filterData).subscribe((value) => {
        this.roles = value;
      });
    }
  }
}
