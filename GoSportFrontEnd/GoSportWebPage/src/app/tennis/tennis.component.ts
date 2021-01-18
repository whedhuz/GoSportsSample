import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TennisMatchDetails } from './tennisMatchDetails';
import { TennisService } from './tennis.service';
import { TennisMatchDetailsComponent } from './tennis-match-details.component';

@Component({
  selector: 'app-tennis-detail',
  templateUrl: './tennis.component.html',
  styleUrls: ['./tennis.component.scss']
})
export class TennisComponent implements OnInit {
  @ViewChild(TennisMatchDetailsComponent, { static: false }) matchDetailsC: TennisMatchDetailsComponent;
  tennisMatchDetail: TennisMatchDetails;
  matchId: string;
  employeeName: string;

  constructor(
    private route: ActivatedRoute,
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

  playerOneServing(): boolean {
    return this.tennisMatchDetail.servingPlayer == 1;
  }

  playerTwoServing(): boolean {
    return this.tennisMatchDetail.servingPlayer == 2;
  }

  sendEvent(eventName: string): void {
    // TODO: add text value validation and error display
    // if (!this.employeeName || this.employeeName != '')
    this.service.postEvent(eventName, this.matchId, this.employeeName)
      .subscribe(successResponse => this.tennisMatchDetail = successResponse.responseObj);
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
