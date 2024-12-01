import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { AssuntoFormComponent } from '../assunto-form/assunto-form.component';
import { AssuntoService } from '../../services/assunto.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-assunto-list',
  standalone: true,
  imports: [MatDialogModule, CommonModule],
  templateUrl: './assunto-list.component.html',
  styleUrls: ['./assunto-list.component.css']
})
export class AssuntoListComponent implements OnInit {
  assuntos: any[] = [];

  constructor(private assuntoService: AssuntoService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.listar();
  }

  listar(): void {
    this.assuntoService.lista().subscribe((data: any[]) => {
      this.assuntos = data;
    });
  }

  form(id: number): void {

    const dialogRef = this.dialog.open(AssuntoFormComponent, {
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
        this.assuntoService.remover(id).subscribe(result => {
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
