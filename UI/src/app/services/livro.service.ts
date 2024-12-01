import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
  private apiUrl = 'https://localhost:7154/api/livro';

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

  gravarAssunto(assunto: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/assunto`, assunto);
  }

  removerAssunto(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/assunto/${id}`);
  }

  getReport(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/report`, { responseType: 'blob' });
  }
}
