import { SwalService } from './swal';
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private swal: SwalService) {}

  errorHandler(err: HttpErrorResponse) {
    console.log(err);

    if (err.status === 403) {
      let errorMsg = '';
      for (const e of err.error.ErrorMessages) {
        errorMsg += e + '\n';
      }

      this.swal.callToast(errorMsg, 'error');
    } else if (err.status === 500)
      this.swal.callToast(err.error.errorMessages[0], 'error');
  }
}
