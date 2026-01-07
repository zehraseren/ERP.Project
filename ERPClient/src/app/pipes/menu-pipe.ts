import { MenuModel } from '../menu';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'menu',
})
export class MenuPipe implements PipeTransform {
  transform(value: MenuModel[], search: string): MenuModel[] {
    if (search === '') {
      return value;
    }

    return value.filter((p) =>
      p.name.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }
}
