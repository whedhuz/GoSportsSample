import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { TennisMatchDetails } from './tennisMatchDetails';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { HandleError, HttpErrorHandler } from '../httpErrorHandler.service';
import { ITennisEvent, ITennisEventObject } from './tennisRequests';

@Injectable({
  providedIn: 'root'
})
export class TennisService {

  baseUrl: string = 'https://localhost:44388/';
  private handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('TennisService');
  }

  getMatchDetails(matchId: string): Observable<TennisMatchDetails> {
    matchId = matchId.trim();

    const options = matchId ? { params: new HttpParams().set('id', matchId) } : {};

    let url = this.baseUrl + 'Game/Tennis';
    return this.http.get<TennisMatchDetails>(url, options)
      .pipe(
        catchError(this.handleError<TennisMatchDetails>('searchHeroes', null))
      );
  }

  postEvent(eventType: string, matchId: string, employeeName: string): Observable<TennisMatchDetails> {
    console.log("eventType", eventType, "matchid", matchId, "employeeName", employeeName);

    let eventObject: ITennisEventObject = { gameId: matchId };
    let event: ITennisEvent = {
      // TODO: Make unique sender request id
      id: matchId,
      submitter: employeeName,
      type: eventType,
      contentJson: JSON.stringify(eventObject)
    }

    let url = this.baseUrl + 'EventHook';
    return this.http.post<TennisMatchDetails>(url, event)
      .pipe(
        catchError(this.handleError<TennisMatchDetails>('postEvent'))
      );
  }
}
