import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { LivroFormComponent } from '../livro-form/livro-form.component';
import { LivroService } from '../../services/livro.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-livro-list',
  standalone: true,
  imports: [MatDialogModule, CommonModule],
  templateUrl: './livro-list.component.html',
  styleUrls: ['./livro-list.component.css']
})
export class LivroListComponent implements OnInit {
  livros: any[] = [];

  constructor(private livroService: LivroService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.listar();
  }

  listar(): void {
    this.livroService.lista().subscribe((data: any[]) => {
      this.livros = data;
    });
  }

  form(id: number): void {

    const dialogRef = this.dialog.open(LivroFormComponent, {
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
        this.livroService.remover(id).subscribe(result => {
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

  downloadReport(): void {
    this.livroService.getReport().subscribe((blob: Blob) => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'relatorio_livros.pdf';
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    });
  }

}
