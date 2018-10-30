import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ranking } from '../_models/ranking';

@Injectable({
  providedIn: 'root'
})
export class RankingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getRankings(): Observable<Ranking[]> {
    return this.http.get<Ranking[]>(this.baseUrl + 'rankings');
  }

  get(game_id): Observable<Ranking> {
    return this.http.get<Ranking>(this.baseUrl + 'rankings/' + game_id);
  }
}
