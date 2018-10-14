import { Component, OnInit, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { GameService } from '../../_services/game.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-game-start',
  templateUrl: './game-start.component.html',
  styleUrls: ['./game-start.component.css']
})
export class GameStartComponent implements OnInit {
  @Input() stage: string;
  @Input() game_id: number;
  @Output() gameCreated = new EventEmitter<number>();
  gameForm: FormGroup;

  constructor(private http:HttpClient,
    private gameService: GameService,
    private fb: FormBuilder,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.initGameForm();
  }

  initGameForm() {
    this.gameForm = this.fb.group({
      userName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]]
    });
  }

  createGame() {

    console.log(this.gameForm.value);

    this.gameService.createGame(this.gameForm.value).subscribe((newGameId: number) => {

      this.alertify.success('A new game created : ' + newGameId);
      this.gameStart(newGameId);
    }, error => {
      this.alertify.error(error);
    });
  }

  gameStart(newGameId: number) {
    this.game_id = newGameId;
    this.gameCreated.emit(this.game_id);
  }
}
