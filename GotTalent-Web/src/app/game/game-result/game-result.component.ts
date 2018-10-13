import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-game-result',
  templateUrl: './game-result.component.html',
  styleUrls: ['./game-result.component.css']
})
export class GameResultComponent implements OnInit {
  @Input() stage: string;
  @Output() go = new EventEmitter<string>();
  constructor() { }

  ngOnInit() {
  }

  gameStart() {
    this.go.emit('splash');
  }
}
