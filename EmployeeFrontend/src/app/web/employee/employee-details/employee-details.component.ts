import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeFormComponent } from '../employee-form/employee-form.component';
import { EmployeeService } from '../../../shared/services/employee.service';

@Component({
  selector: 'app-employee-details',
  standalone: false,
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.scss'
})
export class EmployeeDetailsComponent implements OnInit {

  displayedColumns: string[] = ['email', 'fullName', 'joinedDate', 'salary', 'isDeleted'];
  dataSource = new MatTableDataSource<any>();
  currentPage: number = 1;
  pageSize: number = 5;
  destroy$ = new Subject<boolean>();
  employeeDataSet!: any;

  constructor(public matDialog: MatDialog, private employeeService: EmployeeService) { }


  ngOnInit(): void {
    this.loadEmployeeData();
  }

  loadEmployeeData = (): void => {
    this.employeeService.getEmployees(this.currentPage, this.pageSize).pipe(takeUntil(this.destroy$)).subscribe(employees => {
      if (employees && employees.isSuccess) {
        this.employeeDataSet = employees.response;
        console.log(this.employeeDataSet);

        this.dataSource.data = this.employeeDataSet;
      }
    })
  }

  openEmployeeCreate = (): void => {
    this.matDialog.open(EmployeeFormComponent, {
      height: '55%',
      width: '30%'
    })
  }

  onPageChange = (event: any): void => {
    this.currentPage = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadEmployeeData();
  }
}
