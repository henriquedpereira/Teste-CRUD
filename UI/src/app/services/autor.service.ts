import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AutorService {
  private apiUrl = 'https://localhost:7154/api/Autor';

  constructor(private http: HttpClient) { }

  lista(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  busca(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  gravar(assunto: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, assunto);
  }

  remover(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
