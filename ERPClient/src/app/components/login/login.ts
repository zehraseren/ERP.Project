import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { HttpService } from '../../services/http';
import { LoginModel } from '../../models/login.model.model';
import { SharedModule } from '../../modules/shared-module';
import { LoginResponseModel } from '../../models/login.response.model';

@Component({
  selector: 'app-login',
  imports: [SharedModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  model: LoginModel = new LoginModel();

  constructor(private http: HttpService, private router: Router) {}

  signIn() {
    this.http.post<LoginResponseModel>('Auth/Login', this.model, (res) => {
      localStorage.setItem('token', res.token);
      this.router.navigateByUrl('/');
    });
  }
}
