import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  stage: string;
  game_id: number;

  constructor() { }

  ngOnInit() {
    this.stage = 'splash';
    this.game_id = 0;
  }

  goStage(stage: string) {
    this.stage = stage;
  }
}
