import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { Taxonomy } from './Taxonomy';

@Injectable({
  providedIn: 'root'
})
export class TaxonomyServiceService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }

  getTaxonomy():Observable<Taxonomy[]>{
    return this.http.get<Taxonomy[]>("https://localhost:7082/api/taxonomy")
      .pipe(
        catchError(this.handleError<Taxonomy[]>([]))
      );
  }

  private handleError<T>(result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
