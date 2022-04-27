import { Directive } from "@angular/core";
import { FormControl, NG_VALIDATORS, Validator } from "@angular/forms";

@Directive({
    selector: '[FileTypeValidator]',
    providers: [
        {
            provide: NG_VALIDATORS, useExisting: FileTypeValidator, multi: true
        }
    ]
})
export class FileTypeValidator implements Validator {
    static validate(c: FormControl): { [key: string]: any } {
        if (c.value) {
            if (c.value) {
                return FileTypeValidator.checkExtension(c);
            };
        }

        return null;
    }

    private static checkExtension(c: FormControl) {
        let valToLower = c.value.toLowerCase();
        let regex = new RegExp("(.*?)\.(jpg|png|jpeg)$");
        let regexTest = regex.test(valToLower);
        return !regexTest ? { "notSupportedFileType": true } : null;
    }

    validate(control: FormControl): { [key: string]: any } {
        return FileTypeValidator.validate(control);
    }
}