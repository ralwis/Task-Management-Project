import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { BaseService } from '../../../shared/services/base.service';
import { Task } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskDetailService extends BaseService{
  private readonly baseUrl = environment.apiUrl;

  private httpClient = inject(HttpClient);

  createTask(taskData: Task): Observable<number> {
    const url = `${this.baseUrl}/tasks`;
    return this.httpClient.post<number>(url, { taskDTO: taskData }).pipe(
      catchError(this.handleError)
    );
  }

  updateTask(taskData: Task): Observable<number> {
    const url = `${this.baseUrl}/tasks/${taskData.id}`;
    return this.httpClient.put<number>(url, { taskDTO: taskData }).pipe(
      catchError(this.handleError)
    );
  }
}
