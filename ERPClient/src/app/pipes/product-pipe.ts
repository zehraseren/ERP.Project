import { ProductModel } from '../models/product.model';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'product',
})
export class ProductPipe implements PipeTransform {
  transform(value: ProductModel[], search: string): ProductModel[] {
    if (!search) return value;

    return value.filter(
      (p) =>
        p.name.toLowerCase().includes(search.toLowerCase()) ||
        p.type.name.toLowerCase().includes(search.toLowerCase())
    );
  }
}
