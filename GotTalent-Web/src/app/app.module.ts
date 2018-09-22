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
      RouterModule.forRoot(appRoutes)
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
