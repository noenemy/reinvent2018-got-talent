import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StageLog } from '../_models/stagelog';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StagelogService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getStageLogs(): Observable<StageLog[]> {
    return this.http.get<StageLog[]>(this.baseUrl + 'stagelogs');
  }

  getStageLog(game_id, action_type): Observable<StageLog> {
    return this.http.get<StageLog>(this.baseUrl + 'stagelogs/' + game_id + '/' + action_type);
  }

  addStageLog(stage_log): Observable<StageLog> {
    return this.http.post<StageLog>(this.baseUrl + 'stagelogs', stage_log);
  }
}
