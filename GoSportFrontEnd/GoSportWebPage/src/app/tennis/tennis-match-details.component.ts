import { ChangeDetectorRef, Component, Input, OnChanges } from '@angular/core';

import { TennisCurrentGameScore, TennisMatchDetails, TennisSetScore } from './tennisMatchDetails';

@Component({
  selector: 'app-tennis-match-details',
  templateUrl: './tennis-match-details.component.html',
  styleUrls: ['./tennis.component.scss']
})
export class TennisMatchDetailsComponent {
  @Input() matchDetail: TennisMatchDetails;

  constructor(private ref: ChangeDetectorRef) {
    this.ref.markForCheck();
  }

  playerOneServing(): boolean {
    return this.matchDetail.servingPlayer == 1;
  }

  playerTwoServing(): boolean {
    return this.matchDetail.servingPlayer == 2;
  }
}
