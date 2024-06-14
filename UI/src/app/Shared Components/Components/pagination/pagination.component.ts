import { Component, EventEmitter, Input, OnChanges, Output, Pipe } from '@angular/core';
import { EmployeeService } from '../../../Services/Employee/employee-service.service';
@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss'
})
export class PaginationComponent {
  @Input() pageNumbers:number=0;
  @Output() selectedPageNumber:EventEmitter<number>=new EventEmitter<number>();
  startIndex:number=1;
  endIndex:number=1;
  constructor()
  {

  }
  getnumbers(): number[] {
    const numbersArray: number[] = [];
    for (let i = this.startIndex; i <= this.endIndex; i++) {
      numbersArray.push(i);
    }
    return numbersArray;
  }
  selectPage(pageNumber:number)
  {
    this.selectedPageNumber.emit(pageNumber);
  }
  checkPreviousPage()
  {
    if(this.startIndex>1)
    {
      this.startIndex-=1;
      this.endIndex-=1;
    }
  }
  checkNextPage()
  {
    console.log(this.endIndex,this.pageNumbers);
    if(this.endIndex<this.pageNumbers)
    {
      this.startIndex+=1;
      this.endIndex+=1;
    }
  }
}
