import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AssuntoListComponent } from './components/assunto-list/assunto-list.component';
import { AssuntoFormComponent } from './components/assunto-form/assunto-form.component';
import { AutorListComponent } from './components/autor-list/autor-list.component';
import { AutorFormComponent } from './components/autor-form/autor-form.component';
import { LivroListComponent } from './components/livro-list/livro-list.component';
import { LivroFormComponent } from './components/livro-form/livro-form.component';
import { FormapagListComponent } from './components/formapag-list/formapag-list.component';
import { FormapagFormComponent } from './components/formapag-form/formapag-form.component';
import { DialogComponent } from './components/dialog/dialog.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    DialogComponent,
    ConfirmDialogComponent,
    AppRoutingModule,
    AssuntoFormComponent,
    AssuntoListComponent,
    AutorFormComponent,
    AutorListComponent,
    LivroFormComponent,
    LivroListComponent,
    FormapagFormComponent,
    FormapagListComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
