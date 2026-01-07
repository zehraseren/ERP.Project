import { DepotModel } from './depot.model';
import { ProductModel } from './product.model';

export class InvoiceDetailModel {
  id: string = '';
  invoiceId: string = '';
  productId: string = '';
  depotId: string = '';
  product: ProductModel = new ProductModel();
  depot: DepotModel = new DepotModel();
  quantity: number = 0;
  price: number = 0;
}
