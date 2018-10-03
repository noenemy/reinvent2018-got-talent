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
  castingColumns: string[];
  stageLogColumns: string[];
  rankings: any;
  games: any;
  castings: any;
  stageLogs: any;

  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.rankingColumns = this.getRankingColumns();
    this.gameColumns = this.getGameColumns();
    this.castingColumns = this.getCastingColumns();
    this.stageLogColumns = this.getStageLogColumns();
    this.getRankings();
    this.getGames();
    this.getCastings();
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

  getCastings() {
    this.http.get('http://localhost:5000/api/castings').subscribe(response => {
      this.castings = response;
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

  getCastingColumns(): string[] {
    return ['cast_id', 'title', 'actor', 'gender', 'grade', 'file_loc', 'action_type'];
  }

  getStageLogColumns(): string[] {
    return ['game_id', 'action_type', 'score', 'file_loc', 'age', 'gender', 'log_date'];
  }
}