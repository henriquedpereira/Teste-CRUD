import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssuntoListComponent } from './components/assunto-list/assunto-list.component';
import { AssuntoFormComponent } from './components/assunto-form/assunto-form.component';

import { LivroListComponent } from './components/livro-list/livro-list.component';
import { LivroFormComponent } from './components/livro-form/livro-form.component';

import { AutorListComponent } from './components/autor-list/autor-list.component';
import { AutorFormComponent } from './components/autor-form/autor-form.component';

import { FormapagListComponent } from './components/formapag-list/formapag-list.component';
import { FormapagFormComponent } from './components/formapag-form/formapag-form.component';

const routes: Routes = [
  { path: 'assuntos', component: AssuntoListComponent },
  { path: 'assuntos/form/:id', component: AssuntoFormComponent },

  { path: 'livros', component: LivroListComponent },
  { path: 'livros/form/:id', component: LivroFormComponent },
  { path: '', redirectTo: '/livros', pathMatch: 'full' },

  { path: 'autores', component: AutorListComponent },
  { path: 'autores/form/:id', component: AutorFormComponent },

  { path: 'formas_pagamento', component: FormapagListComponent },
  { path: 'formas_pagamento/form/:id', component: FormapagFormComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
