import { CustomValidators } from './../../custom-validator';
import { HotelInfo } from './../../hotel-details-info/hotel-info.model';
import { debounceTime, fromEvent, merge, Observable } from 'rxjs';
import { GenericValidator } from './../../generic-validator';
import { Component, EventEmitter, Input, OnInit, Output, OnChanges, SimpleChanges, AfterViewInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, FormControlName, FormArray } from '@angular/forms';
import { Hotel } from '../hotel.model';
import { ImageFormatValidator } from '../image-format';

@Component({
  selector: 'app-hotel-edit-presentational',
  templateUrl: './hotel-edit-presentational.component.html',
  styleUrls: ['./hotel-edit-presentational.component.css']
})
export class HotelEditPresentationalComponent implements OnInit, OnChanges, AfterViewInit {
  @Input() hotel: Hotel;
  @Input() hotelInfo: HotelInfo;
  @Output() addHotelEvent = new EventEmitter<Hotel>();
  @Output() onFileSelectedEvent = new EventEmitter<any>();
  @Output() onPictureSelectedEvent = new EventEmitter<any>();
  @Output() showUploadNotificationEvent = new EventEmitter<string>();
  @Output() showUploadPicturesNotificationEvent = new EventEmitter<string>();
  @Output() updateHotelEvent = new EventEmitter<Hotel>();
  @Output() addHotelInfoEvent = new EventEmitter<HotelInfo>();
  @Output() showDeletePictureMessageEvent = new EventEmitter<HotelInfo>();
  @Output() updateHotelInfoEvent = new EventEmitter<HotelInfo>();
  @Output() hotelNameEvent = new EventEmitter<string>();
  @Output() addCoverPage = new EventEmitter<any>();
  @Output() invalidFormMessage = new EventEmitter<boolean>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  picture: any;
  hotelInfoImage: any;
  pageTitle: 'Hotel';
  hotelForm: FormGroup;
  hotelInfoForm: FormGroup;
  isEdit = false;
  isImageChanged = false;
  displayMessage: { [key: string]: string } = {};
  numberRegEx = /\-?\d*\.?\d{1,2}/;
  phoneMask = ['(', '+', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/];
  private validationMessage: { [key: string]: { [key: string]: string } };
  genericValidator: GenericValidator;
  isUploaded = false;
  isAdditionalInfo = false;
  picturesList: any[] = [];
  isImageUploaded = false;
  isImageAdded = false;
  disableAddPicture = false;
  isEditHotelInfo = false;
  coverImageUrl: any;
  changeSource = false;
  isExtensionValid: boolean;
  newPicture: any;
  hotelImageUrl: any;
  hotelImagesList: any[] = [];
  hotelImagesListEvent: any[] = [];

  constructor(private fb: FormBuilder) {
    this.createHotelForm();
    this.createHotelInfoForm();
    this.showValidationsMessage();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.hotel) {
      this.changeSource = this.hotel.id < 1 ? true : false;
    }
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
    if (this.hotelInfo) {
      this.isEditHotelInfo = this.hotel?.hotelDetailsId ? true : false;
    }

    if (this.isEditHotelInfo) {
      this.pictures.disable();
    }

    if (changes['hotelInfo'] && changes['hotelInfo'].currentValue) {
      this.hotelInfoForm.patchValue(this.hotelInfo);
    }
    this.hotelInfoForm.setControl('pictures', this.fb.array(this.hotelInfo?.pictures || []))

    for (let pic of this.pictures?.controls) {
      this.picturesList.push(pic.value);
      this.hotelImagesList.push(pic.value);
    }

    if (this.pictures?.length) {
      this.pictures.disable();
    }

    if (this.isExtensionValid) {
      this.hotelForm.invalid;
    }

    this.coverImageUrl = this.hotel ? this.hotel.image : null;
  }

  replacePathForImages(): any[] {
    let pics = [];
    for (let pic of this.hotelInfo.pictures) {
      pic = pic?.replace(`assets/images/${this.hotel.name}/`, '');
      pics.push(pic);
    }
    return pics;
  }

  checkForValidExtensions(stringToBeChecked: string): boolean {
    let extensions = ['.jpg', '.jpeg', '.png'];
    for (let ext of extensions) {
      if (stringToBeChecked.endsWith(ext)) {
        this.isExtensionValid = true;
        break;
      } else {
        this.isExtensionValid = false;
      }
    }
    this.addCoverPage.emit(this.isExtensionValid);

    return this.isExtensionValid;
  }

  get pictures(): FormArray {
    let picturesForm = (this.hotelInfoForm.get('pictures') as FormArray);
    return picturesForm;
  }

  get picturesForm(): FormControl[] {
    return (this.hotelInfoForm.get('pictures') as FormArray).controls as FormControl[];
  }

  ngAfterViewInit(): void {
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(
      formControl.nativeElement, 'blur'));

    merge(this.hotelForm.valueChanges, ...controlBlurs).pipe(
      debounceTime(1000)).subscribe(() => {
        this.displayMessage = this.genericValidator.processMessages(this.hotelForm);
      })
    if (this.isAdditionalInfo) {
      merge(this.hotelInfoForm.valueChanges, ...controlBlurs).pipe(
        debounceTime(1000)).subscribe(() => {
          this.displayMessage = this.genericValidator.processMessages(this.hotelInfoForm);
        });
    }
  }

  createHotelInfoForm() {
    this.hotelInfoForm = this.fb.group({
      city: ['', [Validators.minLength(3), Validators.maxLength(25)]],
      country: ['', [Validators.minLength(3), Validators.maxLength(25)]],
      rooms: ['', Validators.pattern(this.numberRegEx)],
      stars: [null, [CustomValidators.ratingRange(1, 7), Validators.pattern(this.numberRegEx)]],
      pictures: this.fb.array([])
    })
  }

  ngOnInit(): void {
    this.displayMessage = this.genericValidator.processMessages(this.hotelForm);
  }

  createHotelForm() {
    this.hotelForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      address: ['', [Validators.required, Validators.minLength(4)]],
      emailAddress: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required]],
      employeesNumber: [null, [Validators.required, Validators.pattern(this.numberRegEx)]],
      image: ['', [Validators.required, ImageFormatValidator.validImageFormat]]
    });

  }

  additionalInfo() {
    this.isAdditionalInfo = !this.isAdditionalInfo;
  }

  saveHotel(currentHotel: Hotel) {
    if (!currentHotel) {
      currentHotel = this.emptyObj;
    }

    if (this.hotelForm.invalid) {
      this.scrollToError();
      this.invalidFormMessage.emit(true);
    }

    if (this.hotelForm.valid) {
      const hotel = { ...currentHotel, ...this.hotelForm.value };
      hotel.image = this.processImage(hotel.name);
      if (currentHotel.id === 0) {
        this.addHotelEvent.emit(hotel);
        this.hotelNameEvent.emit(hotel.name);

        if (this.isAdditionalInfo) {
          const hotelInfo = { ...this.hotelInfoForm.value };
          hotelInfo.hotelId = hotel.id;
          hotelInfo.pictures = this.changePicturesPath(hotel.name, this.picturesList);
          this.addHotelInfoEvent.emit(hotelInfo);
        }
      }
      else {
        if (this.isAdditionalInfo) {
          const hotelInfo = { ...this.hotelInfoForm.value };
          if (hotel) {
            hotelInfo.hotelId = this.hotel.id;
            if (hotel.hotelDetailsId < 1) {
              hotelInfo.pictures = this.changePicturesPath(hotel.name, this.picturesList);
              this.addHotelInfoEvent.emit(hotelInfo);
            }
            if (hotel.hotelDetailsId > 0) {
              hotelInfo.pictures = this.changePicturesPath(hotel.name, this.picturesList);
              this.updateHotelInfoEvent.emit(hotelInfo);
              this.hotelNameEvent.emit(hotel.name);
            }
          }
        }
        this.updateHotelEvent.emit(hotel);
        if (this.isImageChanged) {
          this.hotelNameEvent.emit(hotel.name);
          hotel.image = this.processImage(hotel.name);
          this.onFileSelectedEvent.emit(this.newPicture);
        }
      }
      if (this.isImageUploaded) {
        for (let pic of this.hotelImagesListEvent) {
          this.onPictureSelectedEvent.emit(pic);
        }
      }
    }
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

  onFileSelected(event: any): any {
    this.picture = event;
  }

  onUploadImage() {
    if (confirm('Are you sure you want to add this image ?')) {
      this.showUploadNotificationEvent.emit();
      if (this.image.value.length > 0) {
        this.image.disable();
        this.isUploaded = true;
      }
    }
  }

  onUploadImageEdit() {
    this.isImageChanged = !this.isImageChanged;
    this.hotelForm.patchValue({
      image: ''
    });
    if (this.coverImageUrl) {
      this.coverImageUrl = '';
    }
    this.changeSource = true;
  }

  get image(): FormControl {
    return this.hotelForm.controls['image'] as FormControl;
  }

  processImage(hotelName: string): string {
    return this.image.value.replace('C:\\fakepath\\', `assets/images/${hotelName}/cover-page/`);
  }

  changePicturesPath(name: string, pictures: any[]): string[] {
    let newPictureList: string[] = [];
    for (let pic of pictures) {
      pic = pic?.replace('C:\\fakepath\\', `assets/images/${name}/`);
      if (!pic.includes(`assets/images/${name}/`)) {
        pic = `assets/images/${name}/${pic}`;
      }
      newPictureList.push(pic);
    }
    return newPictureList;
  }

  showValidationsMessage() {
    this.validationMessage = {
      name: {
        required: 'Hotel name is required.',
        minlength: 'Hotel name cannot have less than 3 characters.'
      },
      address: {
        required: 'Address is required.',
        minlength: 'Address cannot have less than 4 characters.'
      },
      emailAddress: {
        required: 'Email address is required.',
        email: 'Please insert a valid email.'
      },
      phone: {
        required: 'Phone number is required.'
      },
      employeesNumber: {
        required: 'Employees number is required.',
        pattern: 'Only numbers are allowed.'
      },
      image: {
        required: 'Image is required.',
        invalidImageFormat: 'This format is invalid!'
      },
      city: {
        minlength: 'City cannot have less than 3 characters.',
        maxlength: 'City cannot have more than 25 characters.'
      },
      country: {
        minlength: 'Country cannot have less than 3 characters.',
        maxlength: 'Country cannot have more than 25 characters.'
      },
      stars: {
        rangeNumber: 'Rate the hotel between 1 (lowest) an 7 (highest).',
        pattern: 'Only numbers are allowed.'
      },
      rooms: {
        pattern: 'Only numbers are allowed.'
      }
    }
    this.genericValidator = new GenericValidator(this.validationMessage);
  }

  readURLCoverImage(event: any) {
    let selectedFile: any;
    selectedFile = event.target.files[0];
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.coverImageUrl = event.target.result;
      }
      this.checkForValidExtensions(selectedFile.name);
      this.hotelForm.patchValue({ image: `assets/images/${this.hotel.name}/cover-page/${selectedFile.name}` });
      this.newPicture = event;
      this.isImageChanged = true;
      reader.readAsDataURL(event.target.files[0]);
    }
  }

  readURLHotelImages(event: any) {
    let selectedFile: any;
    selectedFile = event.target.files[0];
    this.hotelImagesListEvent.push(selectedFile);

    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.hotelImageUrl = event.target.result;
      }
      if (this.hotelImageUrl) {
        this.hotelImagesList.push(this.hotelImageUrl);
      }
      this.checkForValidExtensions(selectedFile.name);

      this.isImageChanged = true;
      this.picturesList.push(selectedFile.name);

      this.isImageUploaded = true;

      reader.readAsDataURL(event.target.files[0]);
    }
  }

  scrollTo(el: Element) {
    if (el) {
      el.scrollIntoView({ behavior: 'smooth', block: 'center' })
    }
  }

  scrollToError() {
    const firstElementWithError = document.querySelector('.ng-invalid[formControlName]');
    this.scrollTo(firstElementWithError);
  }

  deleteImage(imageIndex: number) {
    this.hotelImagesList.splice(imageIndex, 1);
    this.picturesList.splice(imageIndex, 1);
  }

  deleteImageUrl() {
    this.hotelImageUrl = '';
    this.picturesList.splice(this.picturesList.length - 1, 1);
  }
}
