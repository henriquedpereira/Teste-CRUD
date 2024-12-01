import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { FormapagFormComponent } from '../formapag-form/formapag-form.component';
import { FormapagService } from '../../services/formapag.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-formapag-list',
  standalone: true,
  imports: [MatDialogModule, CommonModule],
  templateUrl: './formapag-list.component.html',
  styleUrls: ['./formapag-list.component.css']
})
export class FormapagListComponent implements OnInit {
  formapags: any[] = [];

  constructor(private formapagService: FormapagService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.listar();
  }

  listar(): void {
    this.formapagService.lista().subscribe((data: any[]) => {
      this.formapags = data;
    });
  }

  form(id: number): void {

    const dialogRef = this.dialog.open(FormapagFormComponent, {
      maxWidth: '100vw',
      maxHeight: '100vh',
      width: '95%',
      data: { id: id }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.listar();
    });
  }

  remover(id: number): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        titulo: 'Confirmar exclusão?',
        mensagem: 'Tem certeza que dessa excluir esse item?'
      }
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.formapagService.remover(id).subscribe(result => {
          this.dialog.open(DialogComponent, {
            data: {
              titulo: 'Sucesso',
              mensagem: result.message
            }
          });

          this.listar();
        });
      }
    });

  }
}
