import { switchMap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';

import { TennisMatchDetails } from './tennisMatchDetails';
import { TennisService } from './tennis.service';

@Component({
  selector: 'app-tennis-detail',
  templateUrl: './tennis.component.html',
  styleUrls: ['./tennis.component.scss']
})
export class TennisComponent implements OnInit {
  tennisMatchDetail$: Observable<TennisMatchDetails>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TennisService
  ) {}


  ngOnInit() {
    this.tennisMatchDetail$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.service.getMatchDetails(params.get('id')))
    );
  }

  sendEvent(eventName: string) {
    console.log("event triggered", eventName);
  }

  sendChangeServerEvent(){
    this.sendEvent("game.tennis.changeserver");
  }

  sendScorePlayerOneEvent(){
    this.sendEvent("game.tennis.score");
  }

  sendScorePlayerTwoEvent(){
    this.sendEvent("game.tennis.score");
  }
}
