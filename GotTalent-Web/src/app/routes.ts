import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RankingComponent } from './ranking/ranking.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { GameComponent } from './game/game.component';
import { DebugComponent } from './debug/debug.component';
import { PlaydemoComponent } from './playdemo/playdemo.component';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'leaderboard', component: RankingComponent },
    { path: 'game', component: GameComponent },
    { path: 'playdemo', component: PlaydemoComponent },
    { path: 'debug', component: DebugComponent },
    { path: 'aboutus', component: AboutusComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
