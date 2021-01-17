import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TennisComponent } from './tennis.component';

const tennisRoutes: Routes = [
  { path: 'tennis/:id', component: TennisComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(tennisRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class TennisRoutingModule { }
