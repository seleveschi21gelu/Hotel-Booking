import { ChangeDetectionStrategy, Component, Input, OnChanges, OnInit, Output, SimpleChanges, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Hotel } from '../hotel.model';

@Component({
  selector: 'app-hotel-details',
  templateUrl: './hotel-details.component.html',
  styleUrls: ['./hotel-details.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HotelDetailsComponent implements OnChanges {
  @Input() hotel: Hotel;
  @Output() deleteHotelEvent = new EventEmitter<number>();

  pageTitle = "Hotel";
  hotelForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.createHotelForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['hotel'] && changes['hotel'].currentValue) {
      this.hotelForm.patchValue({
        name: this.hotel.name,
        address: this.hotel.address,
        emailAddress: this.hotel.emailAddress,
        phone: this.hotel.phone,
        employeesNumber: this.hotel.employeesNumber,
        image: this.hotel.image
      });
    }
  }

  createHotelForm() {
    this.hotelForm = this.fb.group({
      name: [{ value: '', disabled: true }],
      address: [{ value: '', disabled: true }],
      emailAddress: [{ value: '', disabled: true }],
      phone: [{ value: '', disabled: true }],
      employeesNumber: [{ value: null, disabled: true }],
      image: ['']
    });
  }

  deleteHotel(id: number) {
    if (id !== 0)
      if (confirm(`Are you sure you want to delete "${this.hotel.name}" hotel?`)) {
        this.deleteHotelEvent.emit(id);
      }
  }
}
