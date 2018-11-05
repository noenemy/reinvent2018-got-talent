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
  message1: string;
  message2: string;  
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
    this.message1 = 'We will start the audition right away. Lights, Camera..';
    this.message2 = 'Let\'s begin with your profile picture.';   
    this.goStage('stage');

  }

  onStageCompleted(action_type: string) {
    let isGameCompleted = false;

    switch (action_type) {
      case 'Profile':
        this.action_type = 'Happiness';
        this.message1 = 'Out of all the feelings we get, my famous one would be happy face.';
        this.message2 = 'Let me see your happy face!';
        break;
      case 'Happiness':
        this.action_type = 'Anger';
        this.message1 = 'You have already notice this audition is for The Revengers.';
        this.message2 = 'Revenge comes from the anger. Show us the angry face!';
        break;
      case 'Anger':
        this.action_type = 'Sadness';
        this.message1 = 'When half of the population was gone, we all felt sad.';
        this.message2 = 'It is time to get your memories back. Show us the sad face.';
        break;
      case 'Sadness':
        this.action_type = 'Suprise';
        this.message1 = 'Imagine all the people are back from gone! How much would you be surprised!?';
        this.message2 = 'Please show us the face expression if everyone really returns!';
        break;
      case 'Suprise':
        this.action_type = 'NoMoreAction';
        this.message1 = 'Ok. Well done.';
        this.message2 = 'Now time to see your result.';
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
