import { Routes } from '@angular/router';
import { TaskRegisterComponent } from './features/register/components/task-register/task-register.component';
import { LoginComponent } from './features/login/components/login/login.component';
import { TaskDetailComponent } from './features/detail/components/task-detail/task-detail.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'task-register', component: TaskRegisterComponent },
  { path: 'task-detail', component: TaskDetailComponent },
  { path: '**', redirectTo: '/login' }
];
