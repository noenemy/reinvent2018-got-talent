import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../_models/game';
import { GameResult } from '../_models/gameresult';

const httpOptions = {
  'Accept' : 'application/json',
  'Content-Type' : 'application/json'
};

@Injectable({
  providedIn: 'root'
})
export class GameService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.baseUrl + 'games');
  }

  getGame(game_id): Observable<Game> {
    return this.http.get<Game>(this.baseUrl + 'games/' + game_id);
  }

  createGame(user_name): Observable<number> {
    return this.http.post<number>(this.baseUrl + 'games', user_name);
  }

  calcGameResult(game_id): Observable<GameResult> {
    return this.http.get<GameResult>(this.baseUrl + 'gameresults/' + game_id + '/calc');
  }
}
