import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { AuthService } from './services/auth';
import { Login } from './components/login/login';
import { Depots } from './components/depots/depots';
import { Orders } from './components/orders/orders';
import { Layouts } from './components/layouts/layouts';
import { Recipes } from './components/recipes/recipes';
import { Products } from './components/products/products';
import { Invoices } from './components/invoices/invoices';
import { Customers } from './components/customers/customers';
import { Productions } from './components/productions/productions';
import { RecipeDetails } from './components/recipe-details/recipe-details';
import { RequirementsPlanning } from './components/requirements-planning/requirements-planning';

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'requirements-planning/:orderId',
    component: RequirementsPlanning,
    canActivate: [() => inject(AuthService).isAuthenticated()],
  },
  {
    path: '',
    component: Layouts,
    canActivateChild: [() => inject(AuthService).isAuthenticated()],
    children: [
      {
        path: '',
        component: Home,
      },
      {
        path: 'customers',
        component: Customers,
      },
      {
        path: 'depots',
        component: Depots,
      },
      {
        path: 'products',
        component: Products,
      },
      {
        path: 'recipes',
        component: Recipes,
      },
      {
        path: 'recipe-details/:id',
        component: RecipeDetails,
      },
      {
        path: 'orders',
        component: Orders,
      },
      {
        path: 'invoices/:type',
        component: Invoices,
      },
      {
        path: 'productions',
        component: Productions,
      },
    ],
  },
];
