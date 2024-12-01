import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AssuntoFormComponent } from '../assunto-form/assunto-form.component';
import { AssuntoService } from '../../services/assunto.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-assunto-list',
  standalone: true,
  imports: [RouterModule, CommonModule, HttpClientModule],
  templateUrl: './assunto-list.component.html',
  styleUrls: ['./assunto-list.component.css']
})
export class AssuntoListComponent implements OnInit {
  assuntos: any[] = [];

  constructor(private assuntoService: AssuntoService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadAssuntos();
  }

  loadAssuntos(): void {
    this.assuntoService.getAssuntos().subscribe((data: any[]) => {     
      this.assuntos = data;
    });
  }

  openEditDialog(id: number): void {
    const dialogRef = this.dialog.open(AssuntoFormComponent, {
      width: '600px',
      data: { id: id }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadAssuntos();
      }
    });
  }

  deleteAssunto(id: number): void {
    this.assuntoService.deleteAssunto(id).subscribe(() => {
      this.loadAssuntos();
    });
  }
}
