import { HotelInfo } from './../../hotel-details-info/hotel-info.model';
import { HotelInfoService } from './../../hotel-details-info/hotel-info.service';
import { ToastrNotificationService } from './../../toastr-notification.service';
import { Subscription } from 'rxjs';
import { HotelService } from './../hotel.service';
import { Hotel } from '../hotel.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-hotel-edit-container',
  templateUrl: './hotel-edit-container.component.html',
  styleUrls: ['./hotel-edit-container.component.css']
})
export class HotelEditContainerComponent implements OnInit, OnDestroy {
  hotel: Hotel;
  hotelInfo: HotelInfo;
  sub: Subscription;
  selectedFile: File;
  errorMessage: string;
  hotelName: string;

  constructor(private hotelService: HotelService,
    private router: Router,
    private toastr: ToastrNotificationService,
    private hotelInfoService: HotelInfoService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.sub = this.hotelService.selectedHotelAction$.subscribe(currentHotel => {
      this.hotel = currentHotel;
    });

    const id = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.getHotelInfo(id);
      this.hotelService.getHotel(id).subscribe(data => this.hotel = data);
    }
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  addHotel(hotel: Hotel) {
    this.hotelService.addHotel(hotel).subscribe(
      {
        next: () => {
          this.router.navigate(['/hotel']),
            this.hotelService.changeSelectedHotel(null),
            this.toastr.showSuccess('Successfully created', 'Create');
        },
        error: err => {
          this.errorMessage = err,
            this.toastr.showError(err, 'Create')
        }
      });
  }

  getHotelInfo(id: number): any {
    this.hotelInfoService.getHotelInfoById(id).subscribe(data => {
      this.hotelInfo = data
    });
  }

  addHotelInfo(hotelInfo: HotelInfo) {
    this.hotelInfoService.addHotelInfo(hotelInfo).subscribe();
  }

  updateHotel(hotel: Hotel) {
    this.hotelService.updateHotel(hotel).subscribe({
      next: () => {
        this.router.navigate(['/hotel']),
          this.hotelService.changeSelectedHotel(null);
        this.toastr.showSuccess('Successfully updated', 'Update');
      },
      error: err => {
        this.errorMessage = err,
          this.toastr.showError(err, 'Update');
      }
    });
  }

  updateHotelInfo(hotelInfo: HotelInfo) {
    this.hotelInfoService.updateHotelInfo(hotelInfo).subscribe({
      next: () =>
        this.toastr.showSuccess('Successfully updated', 'Update Hotel Info')
    }
    );
  }

  hotelNameForFileName(hotel: string): string {
    return this.hotelName = hotel;
  }

  onUploadNotification() {
    this.toastr.showSuccess('Successfully uploaded', 'Upload');
  }

  invalidImageFormat(value: boolean) {
    if (!value) {
      this.toastr.showError('Invalid image format', 'Upload');
    }
    else {
      this.onUploadNotification();
    }
  }

  onInvalidatForm(isInvalid: boolean) {
    if (isInvalid) {
      this.toastr.showError('Please complete all the required fields.', 'Invalid Form')
    }
  }

  onFileSelected(event: any) {
    if (event.target.files && event.target.files.length) {
      this.selectedFile = event.target.files[0];
    }
    const formData = new FormData();

    formData.append("image", this.selectedFile, this.selectedFile.name);

    this.hotelService.upload(formData, this.hotelName).subscribe(err => {
      this.errorMessage = err,
        this.toastr.showError(err, 'Upload');
    });
  }

  onPicturesSelected(event: any) {
    const formData = new FormData();

    formData.append("image", event, event.name);

    this.hotelInfoService.uploadPictures(formData, this.hotelName).subscribe();
  }

  onShowDeletePictureMessage() {
    this.toastr.showSuccess('Delete image', 'Delete');
  }
}