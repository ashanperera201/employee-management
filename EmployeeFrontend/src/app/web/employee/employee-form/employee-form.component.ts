import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms'
import { EmployeeService } from '../../../shared/services/employee.service';
import { IEmployee } from '../../../shared/interfaces/employee.interface';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-employee-form',
  standalone: false,
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.scss'
})
export class EmployeeFormComponent implements OnInit {
  employeeForm!: FormGroup;
  title: string = 'Create';


  constructor(private formBuilder: FormBuilder, private employeeService: EmployeeService,
    public dialogRef: MatDialogRef<EmployeeFormComponent>) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm = (): void => {
    this.employeeForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', Validators.required],
      salary: ['', Validators.required],
      joinedDate: ['', Validators.required],
    })
  }

  closeEmployeeForm = (): void => {
    this.dialogRef.close();
  }

  textAvoider = (event: any): boolean => {
    let patt = /^([0-9])$/;
    let result = patt.test(event.key);
    return result;
  }

  onSubmission = (): void => {
    if (this.employeeForm.valid) {

      const payload: IEmployee = {
        fullName: this.employeeForm.controls['fullName'].value,
        email: this.employeeForm.controls['email'].value,
        salary: this.employeeForm.controls['salary'].value,
        joinedDate: this.employeeForm.controls['joinedDate'].value,
      }

      this.employeeService.saveEmployee(payload).subscribe({
        next: (response => {
          console.log(response);
        })
      })
    } else {
      // show toaster
    }
  }

}
