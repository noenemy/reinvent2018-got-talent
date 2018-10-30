import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameService } from '../_services/game.service';
import { Game } from '../_models/game';
import { StagelogService } from '../_services/stagelog.service';
import { StageLog } from '../_models/stagelog';
import { CastService } from '../_services/cast.service';
import { Cast } from '../_models/cast';
import { GameResultService } from '../_services/gameresult.service';
import { GameResult } from '../_models/gameresult';

@Component({
  selector: 'app-debug',
  templateUrl: './debug.component.html',
  styleUrls: ['./debug.component.css']
})
export class DebugComponent implements OnInit {
  gameResultColumns: string[];
  gameColumns: string[];
  castColumns: string[];
  stageLogColumns: string[];
  gameResults: GameResult[];
  games: Game[];
  castList: Cast[];
  stageLogs: StageLog[];

  constructor(private http: HttpClient, 
    private gameService: GameService,
    private stageLogService: StagelogService,
    private castService: CastService,
    private gameResultService: GameResultService) { }

  ngOnInit() {
    this.gameResultColumns = this.getGameResultColumns();
    this.gameColumns = this.getGameColumns();
    this.castColumns = this.getCastColumns();
    this.stageLogColumns = this.getStageLogColumns();
    this.getGameResults();
    this.getGames();
    this.getCastList();
    this.getStageLogs();    
  }
  getGameResults() {
    this.gameResultService.getGameResults().subscribe((gameResults: GameResult[]) => {
      this.gameResults = gameResults;
    }, error => {
      console.log(error);
    });
  }

  getGames() {
    this.gameService.getGames().subscribe((games: Game[]) => {
      this.games = games;
    }, error => {
      console.log(error);
    });
  }

  getCastList() {
    this.castService.getCastList().subscribe((castList: Cast[]) => {
      this.castList = castList;
    }, error => {
      console.log(error);
    });
  }

  getStageLogs() {
    this.stageLogService.getStageLogs().subscribe((stageLogs: StageLog[]) => {
      this.stageLogs = stageLogs;
    }, error => {
      console.log(error);
    });
  }

  getGameResultColumns(): string[] {
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