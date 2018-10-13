import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  stage: string;

  constructor() { }

  ngOnInit() {
    this.stage = 'splash';
  }

  goStage(stage: string) {
    this.stage = stage;
  }
}
