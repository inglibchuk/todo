import { Injectable } from '@angular/core';
import { ToDoTask } from './ToDoTask';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiResponse } from './ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class TodoListService {
  private heroesUrl = 'https://localhost:7082/api/tasks'
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getTasks():Observable<ToDoTask[]>{
    return this.http.get<ToDoTask[]>(this.heroesUrl)
      .pipe(
        catchError(this.handleError<ToDoTask[]>([]))
      );
  }

  updateTask(task: ToDoTask): Observable<any>{
    return this.http.put(this.heroesUrl, task, this.httpOptions)
      .pipe(
        catchError(this.handleError<any>())
      );
  }

  addTask(task: ToDoTask): Observable<ApiResponse<ToDoTask>>{
    return this.http.post<ApiResponse<ToDoTask>>(this.heroesUrl, task, this.httpOptions)
      .pipe(
        catchError(this.handleError<ApiResponse<ToDoTask>>())
      );
  }

  deleteTask(taskId: string): Observable<any>{
    const combinedUrl = `${this.heroesUrl}?taskId=${taskId}`
    return this.http.delete(combinedUrl, this.httpOptions)
      .pipe(
        catchError(this.handleError<any>())
      );
  }

  private handleError<T>(result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
