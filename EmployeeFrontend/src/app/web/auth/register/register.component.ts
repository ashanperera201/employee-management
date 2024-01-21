import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IUserRegister } from '../../../shared/interfaces/user.interface';
import { UserService } from '../../../shared/services/user.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup;

  constructor(private router: Router, private formBuilder: FormBuilder, private userService: UserService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm = (): void => {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  navigateToLogin = (): void => {
    this.router.navigate(['/auth/login']);
  }

  proceedRegistration = (): void => {
    if (this.registerForm.valid) {
      const payload: IUserRegister = {
        emailId: this.registerForm.controls['emailAddress'].value,
        firstName: this.registerForm.controls['firstName'].value,
        lastName: this.registerForm.controls['lastName'].value,
        status: 1,
        password: this.registerForm.controls['password'].value,
      };

      this.userService.userRegistration(payload).subscribe({
        next: (response => {
          if (response) {
            this.navigateToLogin();
          }
        })
      })
    }
  }
}
