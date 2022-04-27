import { Observable } from 'rxjs';
import { BookingService } from './../booking.service';
import { Component, OnInit } from '@angular/core';
import { Booking } from '../booking.model';

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  bookingList$: Observable<Booking[]>;

  pageTitle: 'Booking';
  constructor(private bookingService: BookingService) { }

  ngOnInit(): void {
    this.bookingList$ = this.bookingService.getBookingList();
  }

  onSelectedBooking(booking: Booking) {
    this.bookingService.changeSelectedBooking(booking);
  }

}
