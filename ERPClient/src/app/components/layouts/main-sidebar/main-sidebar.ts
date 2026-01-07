import { Menus } from './../../../menu';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MenuPipe } from '../../../pipes/menu-pipe';
import { AuthService } from '../../../services/auth';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-main-sidebar',
  imports: [RouterLink, RouterLinkActive, FormsModule, MenuPipe],
  templateUrl: './main-sidebar.html',
  styleUrl: './main-sidebar.css',
})
export class MainSidebar {
  search: string = '';
  menus = Menus;

  constructor(public auth: AuthService) {}
}
