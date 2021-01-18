import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TennisRoutingModule } from './tennis-routing.module';
import { TennisComponent } from './tennis.component';
import { TennisMatchDetailsComponent } from './tennis-match-details.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TennisRoutingModule
  ],
  declarations: [
    TennisComponent,
    TennisMatchDetailsComponent
  ]
})
export class TennisModule {}
