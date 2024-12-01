import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AutorService } from '../../services/autor.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-autor-form',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule, ReactiveFormsModule, MatDialogModule],
  providers: [AutorService],
  templateUrl: './autor-form.component.html',
  styleUrls: ['./autor-form.component.css']
})
export class AutorFormComponent implements OnInit {
  autorForm: FormGroup;
  id: number;

  constructor(
    private fb: FormBuilder,
    private autorService: AutorService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<AutorFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.autorForm = this.fb.group({
      codAu: [0, Validators.required],
      nome: ['', [Validators.required, Validators.maxLength(20)]]
    });
    this.id = data.id;
  }

  ngOnInit(): void {
    if (this.id !== 0) {
      this.autorService.busca(this.id).subscribe((autor: any) => {
        this.autorForm.patchValue(autor);
      });
    }
  }

  onSubmit(): void {
    if (this.autorForm.valid) {
      this.autorService.gravar(this.autorForm.value).subscribe(
        (response: any) => {
          this.dialog.open(DialogComponent, {
            data: {
              titulo: 'Sucesso',
              mensagem: 'Autor gravado com sucesso!'
            }
          });

          this.dialogRef.close();
        },
        (error: any) => {
          this.dialog.open(DialogComponent, {
            data: {
              title: 'Erro',
              text: error
            }
          });
        }
      );
    } else {
      const validationErrors = this.getFormValidationErrors();
      this.dialog.open(DialogComponent, {
        data: {
          title: 'Erro de Validação',
          text: validationErrors.join('</br>')
        }
      });
    }
  }

  private getFormValidationErrors(): string[] {
    const errors: string[] = [];
    Object.keys(this.autorForm.controls).forEach(key => {
      const controlErrors = this.autorForm.get(key)?.errors;
      if (controlErrors != null) {
        Object.keys(controlErrors).forEach(keyError => {
          errors.push(`Campo ${key} - ${this.getErrorMessage(keyError, controlErrors[keyError])}`);
        });
      }
    });
    return errors;
  }

  private getErrorMessage(errorKey: string, errorValue: any): string {
    const messages: { [key: string]: string } = {
      required: 'é obrigatório',
      maxlength: `excede o tamanho máximo de ${errorValue.requiredLength} caracteres`
    };
    return messages[errorKey] || 'erro desconhecido';
  }
}
