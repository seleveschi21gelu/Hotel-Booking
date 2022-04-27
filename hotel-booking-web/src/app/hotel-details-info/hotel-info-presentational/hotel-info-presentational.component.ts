import { HotelInfo } from './../hotel-info.model';
import { Component, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-hotel-info-presentational',
  templateUrl: './hotel-info-presentational.component.html',
  styleUrls: ['./hotel-info-presentational.component.css']
})
export class HotelInfoPresentationalComponent {
  @Input() hotelInfoList: HotelInfo[];
  @Input() hotelInfo: HotelInfo;
  hotelName: string;
  pageTitle = 'Hotel Details';

  constructor() {
  }

}
