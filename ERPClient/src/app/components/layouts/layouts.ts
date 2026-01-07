import { Navbar } from './navbar/navbar';
import { Footer } from './footer/footer';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainSidebar } from './main-sidebar/main-sidebar';
import { ControlSidebar } from './control-sidebar/control-sidebar';

@Component({
  selector: 'app-layouts',
  imports: [RouterOutlet, Navbar, MainSidebar, ControlSidebar, Footer],
  templateUrl: './layouts.html',
  styleUrl: './layouts.css',
})
export class Layouts {}
