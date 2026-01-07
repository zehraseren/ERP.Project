import { RouterLink } from '@angular/router';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-blank',
  imports: [RouterLink],
  templateUrl: './blank.html',
  styleUrl: './blank.css',
})
export class Blank {
  @Input() pageName: string = '';
  @Input() routes: string[] = [];
}
