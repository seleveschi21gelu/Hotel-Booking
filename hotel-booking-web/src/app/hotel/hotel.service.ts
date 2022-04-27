import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { Hotel } from './hotel.model';

@Injectable({
  providedIn: 'root'
})
export class HotelService {
  private URL = 'https://localhost:7166/api/Hotel';

  constructor(private http: HttpClient) { }

  public getHotels(): Observable<Hotel[]> {
    return this.http.get<Hotel[]>(this.URL).pipe(
      tap(data => console.log(JSON.stringify(data)),
        catchError(this.handleError))
    );
  }

  public getHotel(id: number): Observable<Hotel> {
    return this.http.get<Hotel>(`${this.URL}/${id}`);
  }

  public addHotel(hotel: Hotel): Observable<Hotel> {
    return this.http.post<Hotel>(this.URL, hotel).pipe(
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError));
  }

  public updateHotel(hotel: Hotel): Observable<Hotel> {
    return this.http.put<Hotel>(`${this.URL}/${hotel.id}`, hotel).pipe(
      tap(data => console.log(JSON.stringify(data)),
        catchError(this.handleError))
    );
  }

  public upload(file: FormData, name: string): Observable<any> {
    // let body = JSON.stringify({ 'file': { file: file }, 'hotel': hotel })
    return this.http.post<any>(`${this.URL}/upload?name=${name}`, file).pipe(
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError))
  }

  public deleteHotel(id: number): Observable<Hotel> {
    return this.http.delete<Hotel>(`${this.URL}/${id}`).pipe(
      catchError(this.handleError));
  }

  private selectedHotelStream = new BehaviorSubject<Hotel | null>(null);
  selectedHotelAction$ = this.selectedHotelStream.asObservable().pipe(tap(data => console.log(JSON.stringify(data))));

  changeSelectedHotel(hotel: Hotel) {
    this.selectedHotelStream.next(hotel);
  }

  private handleError(err: any) {
    let errorMessage: string;

    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.log(err);

    return throwError(errorMessage);
  }
}
