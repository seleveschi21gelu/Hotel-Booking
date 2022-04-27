import { Hotel } from './../hotel.model';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-hotels-list',
  templateUrl: './hotels-list.component.html',
  styleUrls: ['./hotels-list.component.css']
})
export class HotelsListComponent implements OnInit {
  @Input() hotels: Hotel[];
  @Input() hotel: Hotel;
  @Input() selectedHotel: Hotel;
  @Output() selectedHotelEvent = new EventEmitter<Hotel>();
  @Output() addEmptyHotel = new EventEmitter<any>();
  pageTitle = "Hotels";

  constructor() { }

  ngOnInit(): void {
  }

  addNewHotel() {
    this.addEmptyHotel.emit();
  }

  onSelectedHotel(hotel: Hotel) {
    this.selectedHotelEvent.emit(hotel);
  }

}
