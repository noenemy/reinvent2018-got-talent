import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { RankingComponent } from './ranking/ranking.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { PlaydemoComponent } from './playdemo/playdemo.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { AboutusComponent } from './aboutus/aboutus.component';
import { WebcamModule } from 'ngx-webcam';
import { GameService } from './_services/game.service';
import { StagelogService } from './_services/stagelog.service';
import { CastService } from './_services/cast.service';
import { RankingService } from './_services/ranking.service';

@NgModule({
   declarations: [
      AppComponent,
      RankingComponent,
      NavComponent,
      HomeComponent,
      PlaydemoComponent,
      AboutusComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(appRoutes),
      WebcamModule
   ],
   providers: [
       GameService,
       StagelogService,
       CastService,
       RankingService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
