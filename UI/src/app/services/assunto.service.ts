import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {
  private apiUrl = 'https://localhost:7154/api/assunto'; // Update with your API URL

  constructor(private http: HttpClient) { }

  getAssuntos(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getAssunto(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  GravarAssunto(assunto: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, assunto);
  }

  updateAssunto(id: number, assunto: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, assunto);
  }

  deleteAssunto(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
