import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-game-start',
  templateUrl: './game-start.component.html',
  styleUrls: ['./game-start.component.css']
})
export class GameStartComponent implements OnInit {
  @Input() stage: string;
  @Output() go = new EventEmitter<string>();
  constructor() { }

  ngOnInit() {
  }

  gameStart() {
    this.go.emit('stage');
  }
}
