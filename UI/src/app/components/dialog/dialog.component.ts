import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-dialog',
  standalone: true,
  template: `
    <h1 mat-dialog-title>{{ data.titulo }}</h1>
    <div mat-dialog-content>
      <p [innerHTML]="sanitizedMessage"></p>
    </div>
    <div mat-dialog-actions>
      <button mat-button (click)="close()">OK</button>
    </div>
  `
})
export class DialogComponent {
  sanitizedMessage: SafeHtml;

  constructor(
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { titulo: string, mensagem: string },
    private sanitizer: DomSanitizer
  ) {
    this.sanitizedMessage = this.sanitizer.bypassSecurityTrustHtml(data.mensagem);
  }

  close(): void {
    this.dialogRef.close();
  }
}
