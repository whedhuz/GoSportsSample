import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { GameModule } from './game/game.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,

    // Routing modules
    AppRoutingModule,

    // Child modules
    GameModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
