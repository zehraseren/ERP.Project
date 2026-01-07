import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { HttpService } from '../../services/http';
import { SwalService } from '../../services/swal';
import { OrderPipe } from '../../pipes/order-pipe';
import { OrderModel } from '../../models/order.model';
import { ProductModel } from '../../models/product.model';
import { SharedModule } from '../../modules/shared-module';
import { CustomerModel } from '../../models/customer.model';
import { OrderDetailModel } from '../../models/order-detail.model';
import { OnInit, ViewChild, Component, ElementRef } from '@angular/core';

@Component({
  selector: 'app-orders',
  imports: [SharedModule, OrderPipe],
  providers: [DatePipe],
  templateUrl: './orders.html',
  styleUrl: './orders.css',
})
export class Orders implements OnInit {
  orders: OrderModel[] = [];
  customers: CustomerModel[] = [];
  products: ProductModel[] = [];
  search: string = '';
  createOrderDetail: OrderDetailModel = new OrderDetailModel();
  updateOrderDetail: OrderDetailModel = new OrderDetailModel();

  @ViewChild('createModalCloseBtn') createModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  @ViewChild('updateModalCloseBtn') updateModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  createModel: OrderModel = new OrderModel();
  updateModel: OrderModel = new OrderModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private date: DatePipe
  ) {
    this.createModel.date = this.date.transform(new Date(), 'yyyy/MM/dd') ?? '';
    this.createModel.deliveryDate =
      this.date.transform(new Date(), 'yyyy/MM/dd') ?? '';
  }

  ngOnInit(): void {
    this.getAll();
    this.getAllProducts();
    this.getAllCustomers();
  }

  getAll() {
    this.http.post<OrderModel[]>('Orders/GetAll', {}, (res) => {
      this.orders = res;
    });
  }

  getAllProducts() {
    this.http.post<ProductModel[]>('Products/GetAll', {}, (res) => {
      this.products = res;
    });
  }

  getAllCustomers() {
    this.http.post<CustomerModel[]>('Customers/GetAll', {}, (res) => {
      this.customers = res;
    });
  }

  addDetail() {
    const product = this.products.find(
      (p) => p.id == this.createOrderDetail.productId
    );
    if (product) this.createOrderDetail.product = product;

    this.createModel.orderDetails.push(this.createOrderDetail);
    this.createOrderDetail = new OrderDetailModel();
  }

  addUpdateDetail() {
    const product = this.products.find(
      (p) => p.id == this.updateOrderDetail.productId
    );
    if (product) this.updateOrderDetail.product = product;

    this.updateModel.orderDetails.push(this.updateOrderDetail);
    this.updateOrderDetail = new OrderDetailModel();
  }

  removeDetail(index: number) {
    this.createModel.orderDetails.splice(index, 1);
  }

  removeUpdateDetail(index: number) {
    this.updateModel.orderDetails.splice(index, 1);
  }

  create(form: NgForm) {
    if (form.valid) {
      this.http.post<string>('Orders/Create', this.createModel, (res) => {
        this.swal.callToast(res);
        this.createModel = new OrderModel();
        this.createModel.date =
          this.date.transform(new Date(), 'yyyy/MM/dd') ?? '';
        this.createModel.deliveryDate =
          this.date.transform(new Date(), 'yyyy/MM/dd') ?? '';
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: OrderModel) {
    this.swal.callSwal(
      'Depoyu Sil',
      `${model.customer.name} - ${model.number} numaralı siparişi silmek istiyor musunuz?`,
      () => {
        this.http.post<string>('Orders/DeleteById', { id: model.id }, (res) => {
          this.getAll();
          this.swal.callToast(res, 'info');
        });
      }
    );
  }

  get(model: OrderModel) {
    this.updateModel = { ...model };
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>('Orders/Update', this.updateModel, (res) => {
        this.swal.callToast(res, 'info');
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  setStatusClass(statusValue: number) {
    if (statusValue === 1) return 'text-danger';
    else if (statusValue === 2) return 'text-primary';
    else return 'text-success';
  }
}
