import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { AlertifyService } from '../../_services/alertify.service';
import { GameResult } from '../../_models/gameresult';
import { HttpClient } from '@angular/common/http';
import { GameService } from '../../_services/game.service';

@Component({
  selector: 'app-game-result',
  templateUrl: './game-result.component.html',
  styleUrls: ['./game-result.component.css']
})
export class GameResultComponent implements OnInit {
  @Input() stage: string;
  @Input() game_id: number;
  @Output() go = new EventEmitter<string>();
  game_result: GameResult;
  constructor(private http: HttpClient,
    private gameService: GameService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.calcGameResult();
  }

  public calcGameResult() {

    this.alertify.message('Now working on it...');

    this.gameService.calcGameResult(this.game_id).subscribe((gameResult: GameResult) => {

      this.alertify.success('Successfully uploaded!');
      this.game_result = gameResult;
      //this.imageSignedURL = stageLogResult.file_loc;
    }, error => {
      this.alertify.error(error);
    });
  }

  gameStart() {
    this.go.emit('splash');
  }
}
