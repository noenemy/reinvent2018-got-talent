import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  stage: string;
  game_id: number;
  action_type: string;

  constructor() { }

  ngOnInit() {
    this.stage = 'splash';
    this.game_id = 0;
  }

  goStage(stage: string) {
    this.stage = stage;
  }

  onGameCreated(game_id: number) {
    this.game_id = game_id;
    this.action_type = 'Profile';
    this.goStage('stage');
  }

  onStageCompleted(action_type: string) {
    let isGameCompleted = false;

    switch (action_type) {
      case 'Profile':
        this.action_type = 'Happiness';
        break;
      case 'Happiness':
        this.action_type = 'Anger';
        break;
      case 'Anger':
        this.action_type = 'Sadness';
        break;
      case 'Sadness':
        this.action_type = 'Suprise';
        break;
      case 'Suprise':
        this.action_type = 'NoMoreAction';
        break;
      case 'NoMoreAction':
        isGameCompleted = true;
        break;
    }

    if (isGameCompleted) {
      this.goStage('result');
    } else {
      this.goStage('stage');
    }
  }
}
