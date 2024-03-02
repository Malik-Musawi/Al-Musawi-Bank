import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = 'http://localhost:5240/api/Transaction';

  constructor(private http: HttpClient) {}

  getTransactionsForAccount(accountId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/ByAccount/${accountId}`);
  }
}
