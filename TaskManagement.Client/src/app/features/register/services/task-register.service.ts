import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { Task } from '../../detail/models/task.model';
import { BaseService } from '../../../shared/services/base.service';
import { Status } from '../models/status.model';

@Injectable({
  providedIn: 'root'
})
export class TaskRegisterService extends BaseService{
  private readonly baseUrl = environment.apiUrl;

  private httpClient = inject(HttpClient);

  getAllTasks(): Observable<Task[]> {
    const url = `${this.baseUrl}/GetTasks`;

    return this.httpClient.get<Task[]>(url)
    .pipe(
      catchError(this.handleError)
    );
  }

  getStatuses(): Observable<Status[]> {
    const url = `${this.baseUrl}/GetStatuses`;

    return this.httpClient.get<Status[]>(url)
    .pipe(
      catchError(this.handleError)
    );
  }

  deleteTask(id: number): Observable<number> {
    const url = `${this.baseUrl}/tasks/${id}`;
    return this.httpClient.delete<number>(url).pipe(catchError(this.handleError));
  }
}
