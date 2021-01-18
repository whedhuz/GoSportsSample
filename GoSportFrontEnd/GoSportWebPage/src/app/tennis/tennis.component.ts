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
  tennisMatchDetail: TennisMatchDetails;
  matchId: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TennisService
  ) {}


  ngOnInit() {
    this.getMatchDetails();
  }

  getMatchDetails(): void {
    this.matchId = this.route.snapshot.paramMap.get('id');
    this.service.getMatchDetails(this.matchId)
      .subscribe(matchDetails => this.tennisMatchDetail = matchDetails);
  }

  sendEvent(eventName: string): void {
    console.log("event triggered", eventName);
    this.service.postEvent(eventName, this.matchId, "test")
  }

  sendChangeServerEvent(): void{
    this.sendEvent("game.tennis.changeserver");
  }

  sendScorePlayerOneEvent(): void{
    this.sendEvent("game.tennis.score");
  }

  sendScorePlayerTwoEvent(): void{
    this.sendEvent("game.tennis.score");
  }
}
