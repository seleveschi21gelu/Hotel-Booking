import { Subscription } from 'rxjs';
import { HotelInfoService } from './../hotel-info.service';
import { HotelInfo } from './../hotel-info.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-hotel-info-container',
  templateUrl: './hotel-info-container.component.html',
  styleUrls: ['./hotel-info-container.component.css']
})
export class HotelInfoContainerComponent implements OnInit {
  hotelInfoList: HotelInfo[];
  hotelInfo: HotelInfo;
  sub: Subscription;
  constructor(private hotelInfoService: HotelInfoService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.hotelInfoService.getAllHotelInfo().subscribe(
      { next: data => this.hotelInfoList = data }
    )
    const param = +this.route.snapshot.paramMap.get('id');

    if (param) {
      const id = param;
      this.getHotelInfoById(id);
    }
  }

  getHotelInfoById(id: number) {
    this.hotelInfoService.getHotelInfoById(id).subscribe({
      next: info => this.hotelInfo = info
    });
  }
}
