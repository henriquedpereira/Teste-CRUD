import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssuntoListComponent } from './components/assunto-list/assunto-list.component';
import { AssuntoFormComponent } from './components/assunto-form/assunto-form.component';

const routes: Routes = [
  { path: 'assuntos', component: AssuntoListComponent },
  { path: 'assuntos/form/:id', component: AssuntoFormComponent },
  { path: '', redirectTo: '/assuntos', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
