import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  template: `
    <h1 mat-dialog-title>{{ data.titulo }}</h1>
    <div mat-dialog-content>
      <p>{{ data.mensagem }}</p>
    </div>
    <div mat-dialog-actions>
    <button class="btn btn-danger btn-sm mr-2" mat-button (click)="onCancel()">Cancelar</button>
      <button class="btn btn-primary btn-sm " mat-button (click)="onConfirm()">Confirmar</button>      
    </div>
  `
})
export class ConfirmDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { titulo: string; mensagem: string }
  ) { }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }
}
