import { Observable, tap, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Booking } from './booking.model';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private URL = 'https://localhost:7166/api/Booking';

  constructor(private http: HttpClient) { }

  getBookingList(): Observable<Booking[]> {
    return this.http.get<Booking[]>(this.URL).pipe(
      tap(data => console.log(JSON.stringify(data))));
  }

  getBookingById(id: number): Observable<Booking> {
    return this.http.get<Booking>(`${this.URL}/${id}`)
  }

  addBooking(booking: Booking): Observable<Booking> {
    return this.http.post<Booking>(this.URL, booking);
  }

  updateBooking(booking: Booking): Observable<Booking> {
    return this.http.put<Booking>(`${this.URL}/${booking.id}`, booking);
  }

  private selectedBookingStream = new BehaviorSubject<Booking | null>(null);
  selectedBookingAction$ = this.selectedBookingStream.asObservable();

  changeSelectedBooking(booking: Booking) {
    this.selectedBookingStream.next(booking);
  }

}
