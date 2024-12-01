import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AssuntoService } from '../../services/assunto.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-assunto-form',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule, ReactiveFormsModule, MatDialogModule],
  providers: [AssuntoService],
  templateUrl: './assunto-form.component.html',
  styleUrls: ['./assunto-form.component.css']
})
export class AssuntoFormComponent implements OnInit {
  assuntoForm: FormGroup;
  id: number;

  constructor(
    private fb: FormBuilder,
    private assuntoService: AssuntoService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<AssuntoFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.assuntoForm = this.fb.group({
      codAs: [0, Validators.required],
      //descricao: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
    this.id = data.id;
  }

  ngOnInit(): void {
    if (this.id !== 0) {
      this.assuntoService.busca(this.id).subscribe((assunto: any) => {
        this.assuntoForm.patchValue(assunto);
      });
    }
  }

  onSubmit(): void {
    if (this.assuntoForm.valid) {
      this.assuntoService.gravar(this.assuntoForm.value).subscribe(
        (response: any) => {
          this.dialog.open(DialogComponent, {
            data: {
              titulo: 'Sucesso',
              mensagem: 'Assunto gravado com sucesso!'
            }
          });

          this.dialogRef.close();
        },
        (error: any) => {

          console.log(error);

          this.dialog.open(DialogComponent, {
            data: {
              titulo: 'Erro',
              mensagem: error.error.map((err: any) => err.errorMessage).join('</br>')
            }
          });
        }
      );
    }
    else {
      const validationErrors = this.getFormValidationErrors();
      console.log(validationErrors);
      this.dialog.open(DialogComponent, {
        data: {
          titulo: 'Erro de Validação',
          mensagem: validationErrors.join('\n')
        }
      });
    }
  }

  private getFormValidationErrors(): string[] {
    const errors: string[] = [];
    Object.keys(this.assuntoForm.controls).forEach(key => {
      const controlErrors = this.assuntoForm.get(key)?.errors;
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
