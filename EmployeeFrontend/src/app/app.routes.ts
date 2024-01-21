import { Routes } from '@angular/router';
import { employeeGuard } from './shared/services/common/auth-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: 'auth', loadChildren: () => import('./web/auth/auth.module').then(m => m.AuthModule) },
  { path: 'employee',canActivate: [employeeGuard], loadChildren: () => import('./web/employee/employee.module').then(m => m.EmployeeModule) },
  { path: '**', redirectTo: 'auth' }
];
