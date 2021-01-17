import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { TennisService } from './tennisService/tennis.service';
import { TennisMatchDetails } from './tennisService/tennisMatchDetails';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  // TODO: Refactor to its own component
  constructor(
    private tennisService: TennisService,
    private route: ActivatedRoute){

  }

  matchId: string;
  matchDetails$: Observable<TennisMatchDetails>;

  ngOnInit() {
    this.matchDetails$ = this.route.paramMap.pipe(
      switchMap(params => {
        this.matchId = params.get('id');
        return this.tennisService.getMatchDetails(this.matchId);
      })
    );
  }
}
