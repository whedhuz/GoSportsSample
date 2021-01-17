import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { TennisMatchDetails } from './tennisMatchDetails';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { HandleError, HttpErrorHandler } from '../httpErrorHandler.service';

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

    let url = this.baseUrl + 'Game/Tennis'
    return this.http.get<TennisMatchDetails>(url, options)
      .pipe(
        catchError(this.handleError<TennisMatchDetails>('searchHeroes', null))
      );
  }
}
