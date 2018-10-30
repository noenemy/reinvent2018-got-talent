import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
declare var TweenMax: any;

@Component({
  selector: 'app-game-splash',
  templateUrl: './game-splash.component.html',
  styleUrls: ['./game-splash.component.css']
})
export class GameSplashComponent implements OnInit {
  @Input() stage: string;
  @Output() go = new EventEmitter<string>();
  constructor() { }

  ngOnInit() {
    this.animate();
  }

  animate(): void {
    // TweenMax.fromTo('#section_1', 3, {x:300}, {x:10});
    // TweenMax.fromTo('#section_2', 2.4, {x:300}, {x:10});
    // TweenMax.fromTo('#section_3', 3.3, {x:300}, {x:10});
  }

  gameStart() {
    this.go.emit('start');
  }
}
