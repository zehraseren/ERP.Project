import { Pipe, PipeTransform } from '@angular/core';
import { RecipeModel } from '../models/recipe.model';

@Pipe({
  name: 'recipe',
})
export class RecipePipe implements PipeTransform {
  transform(value: RecipeModel[], search: string): RecipeModel[] {
    if (!search) return value;

    return value.filter((p) =>
      p.product.name.toLowerCase().includes(search.toLowerCase())
    );
  }
}
