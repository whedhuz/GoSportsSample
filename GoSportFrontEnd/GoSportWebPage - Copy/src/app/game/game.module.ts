import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { GameComponent } from './game.component';
import { GameServiceService } from './services/game-service.service';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule
  ],
  declarations: [
    GameComponent
  ],
  providers: [
    GameServiceService
  ]
})
export class GameModule { }
