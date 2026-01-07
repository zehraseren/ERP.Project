import { NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { HttpService } from '../../services/http';
import { SwalService } from '../../services/swal';
import { RecipePipe } from '../../pipes/recipe-pipe';
import { RecipeModel } from '../../models/recipe.model';
import { ProductModel } from '../../models/product.model';
import { SharedModule } from '../../modules/shared-module';
import { RecipeDetailModel } from '../../models/recipe-detail.model';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-recipes',
  imports: [SharedModule, RecipePipe, RouterLink],
  templateUrl: './recipes.html',
  styleUrl: './recipes.css',
})
export class Recipes implements OnInit {
  recipes: RecipeModel[] = [];
  search: string = '';
  products: ProductModel[] = [];
  semiProducts: ProductModel[] = [];
  detail: RecipeDetailModel = new RecipeDetailModel();

  @ViewChild('createModalCloseBtn') createModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  @ViewChild('updateModalCloseBtn') updateModalCloseBtn:
    | ElementRef<HTMLButtonElement>
    | undefined;

  createModel: RecipeModel = new RecipeModel();
  updateModel: RecipeModel = new RecipeModel();

  constructor(private http: HttpService, private swal: SwalService) {}

  ngOnInit(): void {
    this.getAll();
    this.getAllProducts();
  }

  getAll() {
    this.http.post<RecipeModel[]>('Recipes/GetAll', {}, (res) => {
      this.recipes = res;
    });
  }

  getAllProducts() {
    this.http.post<ProductModel[]>('Products/GetAll', {}, (res) => {
      this.products = res;
      this.semiProducts = res.filter((p) => p.type.value == 2);
    });
  }

  addDetail() {
    const product = this.products.find((p) => p.id == this.detail.productId);
    if (product) {
      this.detail.product = product;
    }

    this.createModel.details.push(this.detail);
    this.detail = new RecipeDetailModel();
  }

  removeDetail(index: number) {
    this.createModel.details.splice(index, 1);
  }

  create(form: NgForm) {
    if (form.valid) {
      this.http.post<string>('Recipes/Create', this.createModel, (res) => {
        this.swal.callToast(res);
        this.createModel = new RecipeModel();
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: RecipeModel) {
    this.swal.callSwal(
      'Reçeteyi Sil',
      `${model.product.name} ürüne ait reçeteyi silmek istiyor musunuz?`,
      () => {
        this.http.post<string>(
          'Recipes/DeleteById',
          { id: model.id },
          (res) => {
            this.getAll();
            this.swal.callToast(res, 'info');
          }
        );
      }
    );
  }
}
