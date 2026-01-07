import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/http';
import { RequirementsPlanningModel } from '../../models/requirements-planning.model';

@Component({
  selector: 'app-requirements-planning',
  imports: [CommonModule],
  templateUrl: './requirements-planning.html',
  styleUrl: './requirements-planning.css',
})
export class RequirementsPlanning {
  data: RequirementsPlanningModel = new RequirementsPlanningModel();
  orderId: string = '';

  constructor(private acticated: ActivatedRoute, private http: HttpService) {
    this.acticated.params.subscribe((res) => {
      this.orderId = res['orderId'];
      this.get();
    });
  }

  get() {
    this.http.post<RequirementsPlanningModel>(
      'Orders/RequirementsPlanningByOrderId',
      { orderId: this.orderId },
      (res) => {
        this.data = res;
      }
    );
  }
}
