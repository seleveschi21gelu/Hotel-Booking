import { NotFoundComponent } from './../not-found/not-found.component';
import { SpinnerComponent } from './../spinner/spinner.component';
import { BookingListComponent } from './../booking/booking-list/booking-list.component';
import { BookingEditPresentationalComponent } from './../booking/booking-edit-presentational/booking-edit-presentational.component';
import { BookingEditContainerComponent } from './../booking/booking-edit-container/booking-edit-container.component';
import { HotelInfoPresentationalComponent } from './../hotel-details-info/hotel-info-presentational/hotel-info-presentational.component';
import { HotelInfoContainerComponent } from './../hotel-details-info/hotel-info-container/hotel-info-container.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HotelViewComponent } from './hotel-view/hotel-view.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HotelsListComponent } from './hotels-list/hotels-list.component';
import { HotelDetailsComponent } from './hotel-details/hotel-details.component';
import { HotelEditContainerComponent } from './hotel-edit-container/hotel-edit-container.component';
import { HotelEditPresentationalComponent } from './hotel-edit-presentational/hotel-edit-presentational.component';
import { TextMaskModule } from 'angular2-text-mask';
import { StarsComponent } from '../stars/stars.component';

const hotelRoutes = [
  {
    path: 'hotel',
    component: HotelViewComponent
  },
  {
    path: 'hotel/:id/edit',
    component: HotelEditContainerComponent
  },
  {
    path: 'hotel-info/:id',
    component: HotelInfoContainerComponent
  },
  {
    path: 'booking/:id/edit',
    component: BookingEditContainerComponent
  },
  {
    path: 'booking-list',
    component: BookingListComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
]

@NgModule({
  declarations: [HotelViewComponent, HotelsListComponent, HotelDetailsComponent, HotelEditContainerComponent, HotelEditPresentationalComponent,
    HotelInfoContainerComponent, HotelInfoPresentationalComponent,
    StarsComponent, BookingEditContainerComponent, BookingEditPresentationalComponent, SpinnerComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(hotelRoutes),
    TextMaskModule
  ],
  providers: [],
  exports: [HotelViewComponent, HotelEditPresentationalComponent, HotelEditContainerComponent, HotelInfoContainerComponent,
    BookingEditContainerComponent, BookingEditPresentationalComponent, HotelDetailsComponent, SpinnerComponent]
})
export class HotelModule { }
