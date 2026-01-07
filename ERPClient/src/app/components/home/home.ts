import { Component } from '@angular/core';
import { SharedModule } from '../../modules/shared-module';

@Component({
  selector: 'app-home',
  imports: [SharedModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {}
