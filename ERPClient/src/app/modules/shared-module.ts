import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TrCurrencyPipe } from 'tr-currency';
import { CommonModule } from '@angular/common';
import { Blank } from '../components/blank/blank';
import { Section } from '../components/section/section';
import { FormValidateDirective } from 'form-validate-angular';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    Blank,
    Section,
    FormsModule,
    FormValidateDirective,
    TrCurrencyPipe,
  ],
  exports: [
    CommonModule,
    Blank,
    Section,
    FormsModule,
    FormValidateDirective,
    TrCurrencyPipe,
  ],
})
export class SharedModule {}
