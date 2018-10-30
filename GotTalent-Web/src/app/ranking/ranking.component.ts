import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameService } from '../_services/game.service';
import { Game } from '../_models/game';
import { StagelogService } from '../_services/stagelog.service';
import { StageLog } from '../_models/stagelog';
import { CastService } from '../_services/cast.service';
import { Cast } from '../_models/cast';
import { Ranking } from '../_models/ranking';
import { RankingService } from '../_services/ranking.service';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {
  rankingColumns: string[];
  rankings: Ranking[];

  constructor(private http: HttpClient, 
    private gameService: GameService,
    private stageLogService: StagelogService,
    private castService: CastService,
    private rankingService: RankingService) { }

  ngOnInit() {
    this.rankingColumns = this.getRankingColumns();
    this.getRankings();
  }

  getRankings() {
    this.rankingService.getRankings().subscribe((rankings: Ranking[]) => {
      this.rankings = rankings;
    }, error => {
      console.log(error);
    });
  }

  getRankingColumns(): string[] {
    return ['game_id', 'result_page_url', 'total_score', 'total_rank', 'cast_result', 'grade_result', 'gender_result', 'age_result'];
  }
}