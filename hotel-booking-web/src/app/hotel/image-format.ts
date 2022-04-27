import { AbstractControl } from '@angular/forms';
export class ImageFormatValidator {
    static validImageFormat(control: AbstractControl): { [key: string]: boolean } | null {
        if (control.value.length > 0 && (control.value.toLowerCase().endsWith('.jpg') || control.value.toLowerCase().endsWith('.png'))) {
            return null;
        }
        else if (control.value.length > 0) {
            return { 'invalidImageFormat': true }
        }
        else {
            return null;
        }
    }
}