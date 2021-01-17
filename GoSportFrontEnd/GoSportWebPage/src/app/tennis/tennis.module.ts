import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TennisRoutingModule } from './tennis-routing.module';
import { TennisComponent } from './tennis.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TennisRoutingModule
  ],
  declarations: [
    TennisComponent
  ]
})
export class TennisModule {}
