import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { HttpService } from '../../services/http';
import { SwalService } from '../../services/swal';
import { RecipeModel } from './../../models/recipe.model';
import { SharedModule } from '../../modules/shared-module';
import { ProductModel } from './../../models/product.model';
import { RecipeDetailPipe } from '../../pipes/recipe-detail-pipe';
import { RecipeDetailModel } from '../../models/recipe-detail.model';

@Component({
  selector: 'app-recipe-details',
  imports: [SharedModule, RecipeDetailPipe],
  templateUrl: './recipe-details.html',
  styleUrl: './recipe-details.css',
})
export class RecipeDetails {
  recipe: RecipeModel = new RecipeModel();
  search: string = '';
  products: ProductModel[] = [];
  recipeId: string = '';
  isUpdateFormActive: boolean = false;

  createModel: RecipeDetailModel = new RecipeDetailModel();
  updateModel: RecipeDetailModel = new RecipeDetailModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private activated: ActivatedRoute
  ) {
    this.activated.params.subscribe((res) => {
      this.recipeId = res['id'];
      this.getRecipeById();
      this.createModel.recipeId = this.recipeId;
    });
  }

  ngOnInit(): void {
    this.getAllProducts();
  }

  getRecipeById() {
    this.http.post<RecipeModel>(
      'RecipeDetails/GetRecipeByIdWithDetails',
      { recipeId: this.recipeId },
      (res) => {
        this.recipe = res;
      }
    );
  }

  getAllProducts() {
    this.http.post<ProductModel[]>('Products/GetAll', {}, (res) => {
      this.products = res.filter((p) => p.type.value == 2);
    });
  }

  create(form: NgForm) {
    if (form.valid) {
      this.http.post<string>(
        'RecipeDetails/Create',
        this.createModel,
        (res) => {
          this.swal.callToast(res);
          this.createModel = new RecipeDetailModel();
          this.createModel.recipeId = this.recipeId;
          this.getRecipeById();
        }
      );
    }
  }

  deleteById(model: RecipeDetailModel) {
    this.swal.callSwal(
      'Reçetedeki Ürünü Sil',
      `${model.product.name} ürününü silmek istiyor musunuz?`,
      () => {
        this.http.post<string>(
          'RecipeDetails/DeleteById',
          { id: model.id },
          (res) => {
            this.getRecipeById();
            this.swal.callToast(res, 'info');
          }
        );
      }
    );
  }

  get(model: RecipeDetailModel) {
    this.updateModel = { ...model };
    this.updateModel.product =
      this.products.find((p) => p.id == this.updateModel.productId) ??
      new ProductModel();
    this.isUpdateFormActive = true;
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>(
        'RecipeDetails/Update',
        this.updateModel,
        (res) => {
          this.swal.callToast(res, "info");
          this.getRecipeById();
          this.isUpdateFormActive = false;
        }
      );
    }
  }
}
