import { Component } from '@angular/core';
import { TennisService } from './tennisService/tennis.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private tennis:TennisService){

  }
  title = 'GoSportWebPage';


}
