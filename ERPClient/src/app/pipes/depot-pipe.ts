import { DepotModel } from '../models/depot.model';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'depot',
})
export class DepotPipe implements PipeTransform {
  transform(value: DepotModel[], search: string): DepotModel[] {
    if (!search) return value;

    return value.filter(
      (p) =>
        p.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.city.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.town.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
        p.fullAddress.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }
}
