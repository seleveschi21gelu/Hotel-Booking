import { CustomValidators } from './../../custom-validator';
import { DatePipe, formatDate } from '@angular/common';
import { Validators } from '@angular/forms';
import { Hotel } from './../../hotel/hotel.model';
import { FormControl, FormBuilder, FormGroup, FormControlName } from '@angular/forms';
import { Booking } from './../booking.model';
import { Component, Input, OnInit, Output, EventEmitter, OnChanges, SimpleChanges, ViewChild, AfterViewInit, DoCheck, ViewChildren, ElementRef } from '@angular/core';
import { Observable, debounceTime, merge, fromEvent } from 'rxjs';
import { GenericValidator } from 'src/app/generic-validator';

@Component({
  selector: 'app-booking-edit-presentational',
  templateUrl: './booking-edit-presentational.component.html',
  styleUrls: ['./booking-edit-presentational.component.css']
})
export class BookingEditPresentationalComponent implements OnInit, OnChanges, AfterViewInit {
  @Input() bookings: Booking[];
  @Input() hotel: Hotel;
  @Input() booking: Booking;
  @Output() createBookingEvent = new EventEmitter<Booking>();
  @Output() updateBookingEvent = new EventEmitter<Booking>();
  @Output() invalidErrorMessage = new EventEmitter<boolean>();
  @Output() selectedBookingEvent = new EventEmitter<Booking>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  phoneMask = ['(', '+', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/];
  hotelId: number;
  bookingForm: FormGroup;
  displayMessage: { [key: string]: string } = {};
  genericValidator: GenericValidator;
  validationMessage: { [key: string]: { [key: string]: string } };
  startDate: string;
  endDate: string;
  maxDate: string;
  todayDate: any;

  constructor(private fb: FormBuilder, private datePipe: DatePipe) {
    this.createBookingForm();
    this.showValidationMessages();
    const dateFormat = 'yyyy-MM-dd';
    this.todayDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
    this.maxDate = this.endDate = this.datePipe.transform(new Date(), dateFormat);
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (!this.booking) {
      this.bookingForm.patchValue({ hotelName: this.hotel?.name });
    }
    if (changes['booking'] && changes['booking'].currentValue) {
      this.hotelId = this.hotel?.id;

      this.bookingForm.patchValue(this.booking);
      this.convertDataFormat();

      this.selectedBookingEvent.emit(this.booking);
    }
  }

  ngOnInit(): void {
    this.displayMessage = this.genericValidator.processMessages(this.bookingForm);
  }

  ngAfterViewInit(): void {
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) =>
      fromEvent(formControl.nativeElement, 'blur'))

    merge(this.bookingForm.valueChanges, ...controlBlurs).pipe(debounceTime(1000)).subscribe(() => {
      this.displayMessage = this.genericValidator.processMessages(this.bookingForm);
    });
  }

  createBookingForm() {
    this.bookingForm = this.fb.group({
      firstName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      lastName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      dateOfBirth: new FormControl(null, [Validators.required, CustomValidators.ageValidator()]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', Validators.required),
      address: new FormControl('', [Validators.required, Validators.minLength(4)]),
      startBooking: new FormControl(null, Validators.required),
      endBooking: new FormControl(null, Validators.required),
      hotelName: new FormControl({ value: '', disabled: true })
    })
  }

  saveBooking(currentBooking: Booking) {

    currentBooking = this.emptyBookingObj();
    if (this.bookingForm.valid) {
      const booking = { ...currentBooking, ...this.bookingForm.value };
      booking.id = this.booking ? this.booking.id : 0;
      booking.hotelId = this.hotel?.id;
      if (booking.id === 0) {
        this.createBookingEvent.emit(booking);
      } else {
        this.updateBookingEvent.emit(booking);
      }
    } else {
      this.scrollToError();
      this.invalidErrorMessage.emit(true);
    }
  }

  onDateChange(): void {
    this.startDate = this.bookingForm.get('startBooking').value;
    this.endDate = this.bookingForm.get('endBooking').value;

    if (this.startDate > this.endDate) {
      this.bookingForm.patchValue({ endBooking: null });
    }
  }

  convertDataFormat() {
    this.bookingForm.controls.dateOfBirth.setValue(formatDate(this.booking.dateOfBirth, 'yyyy-MM-dd', 'en'))
    this.bookingForm.controls.startBooking.setValue(formatDate(this.booking.startBooking, 'yyyy-MM-dd', 'en'))
    this.bookingForm.controls.endBooking.setValue(formatDate(this.booking.endBooking, 'yyyy-MM-dd', 'en'))
  }

  emptyBookingObj(): Booking {
    return {
      id: 0,
      firstName: '',
      lastName: '',
      dateOfBirth: new Date(),
      email: '',
      phone: '',
      address: '',
      startBooking: new Date(),
      endBooking: new Date(),
      hotelId: 0
    }
  }

  showValidationMessages() {
    this.validationMessage = {
      firstName: {
        required: 'First name is required.',
        minlength: 'First name cannot have less than 3 characters.'
      },
      lastName: {
        required: 'Last name is required.',
        minlength: 'Last name cannot have less than 3 characters.'
      },
      dateOfBirth: {
        required: 'Date of birth is required',
        younger: 'The minimum age is 18'
      },
      email: {
        required: 'Email address is required.',
        email: 'Invalid email address.'
      },
      phone: {
        required: 'Phone number is required.'
      },
      address: {
        required: 'Address is required.',
        minlength: 'Address cannot have less than 4 characters.'
      },
      startBooking: {
        required: 'Please insert the start date.'
      },
      endBooking: {
        required: 'Please insert the end date.'
      }
    }
    this.genericValidator = new GenericValidator(this.validationMessage);
  }

  scrollTo(el: Element) {
    if (el) {
      el.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

  scrollToError() {
    const firstElementWithError = document.querySelector('.ng-invalid[formControlName]');
    this.scrollTo(firstElementWithError);
  }

}
