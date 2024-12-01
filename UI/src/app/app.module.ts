import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AssuntoListComponent } from './components/assunto-list/assunto-list.component';
import { AssuntoFormComponent } from './components/assunto-form/assunto-form.component';
import { DialogComponent } from './components/dialog/dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    DialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AssuntoFormComponent,
    AssuntoListComponent // Import the standalone component here
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
