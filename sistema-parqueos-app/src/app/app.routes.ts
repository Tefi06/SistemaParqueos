import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'clientes',
    pathMatch: 'full'
  },
  {
    path: 'clientes',
    loadComponent: () =>
      import('./pages/clientes/clientes.page')
        .then((m) => m.ClientesPage)
  },
  {
    path: '**',
    redirectTo: 'clientes'
  }
];