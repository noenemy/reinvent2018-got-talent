import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { RankingComponent } from './ranking/ranking.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { AboutusComponent } from './aboutus/aboutus.component';
import { WebcamModule } from 'ngx-webcam';
import { GameService } from './_services/game.service';
import { StagelogService } from './_services/stagelog.service';
import { CastService } from './_services/cast.service';
import { RankingService } from './_services/ranking.service';
import { GameResultService } from './_services/gameresult.service';
import { GameComponent } from './game/game.component';
import { GameSplashComponent } from './game/game-splash/game-splash.component';
import { GameStartComponent } from './game/game-start/game-start.component';
import { GameStageComponent } from './game/game-stage/game-stage.component';
import { GameResultComponent } from './game/game-result/game-result.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AlertifyService } from './_services/alertify.service';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { DebugComponent } from './debug/debug.component';
import { PlaydemoComponent } from './playdemo/playdemo.component';

@NgModule({
   declarations: [
      AppComponent,
      RankingComponent,
      NavComponent,
      HomeComponent,
      AboutusComponent,
      GameComponent,
      GameSplashComponent,
      GameStartComponent,
      GameStageComponent,
      GameResultComponent,
      DebugComponent,
      PlaydemoComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(appRoutes),
      WebcamModule,
      FormsModule,
      ReactiveFormsModule,
      AngularFontAwesomeModule
   ],
   providers: [
      GameService,
      StagelogService,
      CastService,
      GameResultService,
      RankingService,
      AlertifyService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
