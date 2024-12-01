import { Component, OnInit } from '@angular/core';
import { AutorService } from '../../services/autor.service';
import { MatDialog } from '@angular/material/dialog';
import { AutorFormComponent } from '../autor-form/autor-form.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-autor-list',
  standalone: true,
  imports: [CommonModule, MatDialogModule],
  templateUrl: './autor-list.component.html',
  styleUrls: ['./autor-list.component.css']
})
export class AutorListComponent implements OnInit {
  autores: any[] = [];

  constructor(private autorService: AutorService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.listar();
  }

  listar(): void {
    this.autorService.lista().subscribe((data: any[]) => {
      this.autores = data;
    });
  }

  form(id: number): void {

    const dialogRef = this.dialog.open(AutorFormComponent, {
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
        titulo: 'Confirmar Exclusão',
        mensagem: 'Tem certeza que deseja excluir este autor?'
      }
    });

    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.autorService.remover(id).subscribe(result => {
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
