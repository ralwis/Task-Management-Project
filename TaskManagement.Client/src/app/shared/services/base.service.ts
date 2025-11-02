import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  public handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An error occurred!';

    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      if (error.status === 401) {
        errorMessage = 'Unauthorized request';
      } else if (error.status === 400) {
        errorMessage = error.error?.message || 'Bad request';
      } else if (error.status === 0) {
        errorMessage = 'Unable to connect to server';
      } else {
        errorMessage = error.error?.message || `Server returned code ${error.status}`;
      }
    }

    return throwError(() => new Error(errorMessage));
  }
}
