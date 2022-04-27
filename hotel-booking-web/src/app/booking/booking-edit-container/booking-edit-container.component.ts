import { Router, ActivatedRoute } from '@angular/router';
import { ToastrNotificationService } from './../../toastr-notification.service';
import { HotelService } from './../../hotel/hotel.service';
import { Hotel } from './../../hotel/hotel.model';
import { Observable, Subscription } from 'rxjs';
import { BookingService } from './../booking.service';
import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { Booking } from '../booking.model';

@Component({
  selector: 'app-booking-edit-container',
  templateUrl: './booking-edit-container.component.html',
  styleUrls: ['./booking-edit-container.component.css']
})
export class BookingEditContainerComponent implements OnInit, OnDestroy {
  @Input() hotel: Hotel;
  selectedBooking: Booking;
  bookings$: Observable<Booking[]>;
  booking$: Observable<Booking>;
  sub: Subscription;
  constructor(private bookingService: BookingService, private hotelService: HotelService,
    private toastr: ToastrNotificationService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.bookingService.selectedBookingAction$.subscribe(data => this.selectedBooking = data);
    this.bookings$ = this.bookingService.getBookingList();
    this.hotelService.selectedHotelAction$.subscribe(value => this.hotel = value);

    const id = +this.route.snapshot.paramMap.get('id');

    if (id) {
      this.bookingService.getBookingById(id).subscribe(data => this.selectedBooking = data);
    } else {
      this.bookingService.changeSelectedBooking(null);
    }
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  getHotel(hotel: Hotel): Hotel {
    this.hotel = hotel;
    return hotel;
  }

  addBooking(booking: Booking) {
    this.bookingService.addBooking(booking).subscribe(
      {
        next: () => {
          this.router.navigate(['/hotels']);
          this.toastr.showSuccess('Created successfully', 'Booking');
        }
      }
    );
  }

  updateBooking(booking: Booking) {
    this.bookingService.updateBooking(booking).subscribe({
      next: () => {
        this.router.navigate(['/booking-list']);
        this.toastr.showSuccess('Updated successfully', 'Booking');
      }
    })
  }

  onInvalidFormMessage(isInvalid: boolean) {
    if (isInvalid) {
      this.toastr.showError('Please complete all the required fields', 'Invalid form')
    }
  }

  onSelectedBooking(booking: Booking) {
    this.bookingService.changeSelectedBooking(booking);
  }

}
