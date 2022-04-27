import { ToastrNotificationService } from './../../toastr-notification.service';
import { HotelService } from './../hotel.service';
import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { Hotel } from '../hotel.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-hotel-view',
  templateUrl: './hotel-view.component.html',
  styleUrls: ['./hotel-view.component.css']
})
export class HotelViewComponent implements OnInit, OnDestroy {
  hotels: Hotel[] = [];
  hotel: Hotel;
  sub: Subscription;
  selectedHotel: Hotel;
  errorMessage: string;
  @Output() sendSelectedHotel = new EventEmitter<Hotel>();

  constructor(private hotelService: HotelService, private toastr: ToastrNotificationService) { }

  ngOnInit(): void {
    this.hotelService.getHotels()
      .subscribe({
        next: data => this.hotels = data
      });

    this.sub = this.hotelService.selectedHotelAction$.subscribe(
      currentHotel => this.selectedHotel = currentHotel
    );

    this.displaySelectedHotel(this.selectedHotel);
  }

  displaySelectedHotel(currentHotel: Hotel) {
    if (currentHotel) {
      if (currentHotel.id < 1) {
        this.hotelService.changeSelectedHotel(null);
      }
      else {
        this.onSelectedHotel(currentHotel);
      }
    }
  }

  getHotel(id: number) {
    this.hotelService.getHotel(id).subscribe(data => this.hotel = data);
  }

  onSelectedHotel(hotel: Hotel) {
    this.hotelService.changeSelectedHotel(hotel)
  }

  onDeleteHotel(id: number) {
    this.hotelService.deleteHotel(id).subscribe(
      {
        next: () => {
          this.fetchData(),
            this.onSelectedHotel(null),
            this.toastr.showSuccess('Successfully removed', 'Delete')
        },
        error: err => {
          this.errorMessage = err,
            this.toastr.showError(err, 'Delete')
        }
      });
  }

  onAddNewObject() {
    this.hotelService.changeSelectedHotel(this.emptyObj);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  fetchData() {
    this.hotelService.getHotels().subscribe({
      next: data => this.hotels = data,
      error: err => {
        this.errorMessage = err,
          this.toastr.showError(err, 'Reload')
      }
    })
  }

  emptyObj: Hotel = {
    id: 0,
    name: '',
    address: '',
    emailAddress: '',
    phone: '',
    employeesNumber: null,
    image: ''
  }
}
