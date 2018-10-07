import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {
  rankingColumns: string[];
  gameColumns: string[];
  castColumns: string[];
  stageLogColumns: string[];
  rankings: any;
  games: any;
  castList: any;
  stageLogs: any;

  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.rankingColumns = this.getRankingColumns();
    this.gameColumns = this.getGameColumns();
    this.castColumns = this.getCastColumns();
    this.stageLogColumns = this.getStageLogColumns();
    this.getRankings();
    this.getGames();
    this.getCastList();
    this.getStageLogs();
  }

  getRankings() {
    this.http.get('http://localhost:5000/api/rankings').subscribe(response => {
      this.rankings = response;
    }, error => {
      console.log(error);
    });
  }

  getGames() {
    this.http.get('http://localhost:5000/api/games').subscribe(response => {
      this.games = response;
    }, error => {
      console.log(error);
    });
  }

  getCastList() {
    this.http.get('http://localhost:5000/api/cast').subscribe(response => {
      this.castList = response;
    }, error => {
      console.log(error);
    });
  }

  getStageLogs() {
    this.http.get('http://localhost:5000/api/stagelogs').subscribe(response => {
      this.stageLogs = response;
    }, error => {
      console.log(error);
    });
  }

  getRankingColumns(): string[] {
    return ['game_id', 'result_page_url', 'total_score', 'total_rank', 'cast_result', 'grade_result', 'gender_result', 'age_result'];
  }

  getGameColumns(): string[] {
    return ['game_id', 'name', 'share_yn', 'start_date', 'end_date'];
  }

  getCastColumns(): string[] {
    return ['cast_id', 'title', 'actor', 'gender', 'grade', 'file_loc', 'action_type'];
  }

  getStageLogColumns(): string[] {
    return ['game_id', 'action_type', 'score', 'file_loc', 'age', 'gender', 'log_date'];
  }
}