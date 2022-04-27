import { AbstractControl, ValidatorFn } from "@angular/forms";

export class CustomValidators {

    static ratingRange(min: number, max: number): ValidatorFn {
        return (c: AbstractControl): { [key: string]: boolean | null } => {
            if (c.value !== null && (isNaN(c.value) || c.value < min || c.value > max)) {
                return {
                    'rangeNumber': true
                }
            }
            return null;
        }
    }

    static ageValidator(): ValidatorFn {
        return (control: AbstractControl) => {
            let dateOfBirth = new Date(control.value);
            let today = new Date();
            let days = Math.floor((Date.UTC(today.getFullYear(), today.getMonth(), today.getDate())
                - Date.UTC(dateOfBirth.getFullYear(), dateOfBirth.getMonth(), dateOfBirth.getDate())) / (1000 * 60 * 60 * 24));
            let years = days / 365;
            if (years < 18) {
                return { 'younger': true }
            } else {
                return null;
            }
        }
    }
}