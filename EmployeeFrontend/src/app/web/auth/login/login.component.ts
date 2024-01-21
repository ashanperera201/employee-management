import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { IUserLogin } from '../../../shared/interfaces/user.interface';
import { UserService } from '../../../shared/services/user.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  constructor(private router: Router, private formBuilder: FormBuilder, private userService: UserService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm = (): void => {
    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  navigateToRegister = (): void => {
    this.router.navigate(['auth/register']);
  }

  proceedLogin = (): void => {
    if (this.loginForm.valid) {
      const payload: IUserLogin = {
        userName: this.loginForm.controls['userName'].value,
        password: this.loginForm.controls['password'].value,
      }

      this.userService.userLogin(payload).subscribe({
        next: (result => {
          if (result) {
            const token = result.response.accessToken;
            this.userService.storeUserAccessToken(token);
            this.router.navigate(['/employee/details'])
          }
        })
      })

    }
  }

}
