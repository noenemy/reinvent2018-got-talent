import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Cast } from '../_models/cast';

@Injectable({
  providedIn: 'root'
})
export class CastService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCastList(): Observable<Cast[]> {
    return this.http.get<Cast[]>(this.baseUrl + 'cast');
  }

  getCast(cast_id): Observable<Cast> {
    return this.http.get<Cast>(this.baseUrl + 'cast/' + cast_id);
  }
}
