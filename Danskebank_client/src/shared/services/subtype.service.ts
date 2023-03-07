import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Subtype, SubtypeDto } from '../models/subtype';

@Injectable({
  providedIn: 'root'
})
export class SubtypeService {
  private apiEndpoint = 'http://localhost:5234/api/ProductSubtypes';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Subtype[]> {
    return this.http.get<Subtype[]>(this.apiEndpoint);
  }

  getProduct(id: number): Observable<Subtype> {
    return this.http.get<Subtype>(`${this.apiEndpoint}/${id}`);
  }

  createProduct(subtype: SubtypeDto): Observable<Subtype> {
    return this.http.post<Subtype>(this.apiEndpoint, subtype);
  }

  updateProduct(subtype: Subtype): Observable<Subtype> {
    return this.http.put<Subtype>(`${this.apiEndpoint}/${subtype.subtypeID}`, subtype);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiEndpoint}/${id}`);
  }
}
