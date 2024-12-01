import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FormapagService } from '../../services/formapag.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-formapag-form',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule, ReactiveFormsModule, MatDialogModule],
  providers: [FormapagService],
  templateUrl: './formapag-form.component.html',
  styleUrls: ['./formapag-form.component.css']
})
export class FormapagFormComponent implements OnInit {
  formapagForm: FormGroup;
  id: number;

  constructor(
    private fb: FormBuilder,
    private formapagService: FormapagService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<FormapagFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.formapagForm = this.fb.group({
      codForm: [0, Validators.required],
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
    this.id = data.id;
  }

  ngOnInit(): void {
    if (this.id !== 0) {
      this.formapagService.busca(this.id).subscribe((formapag: any) => {
        this.formapagForm.patchValue(formapag);
      });
    }
  }

  onSubmit(): void {
    if (this.formapagForm.valid) {
      this.formapagService.gravar(this.formapagForm.value).subscribe(
        (response: any) => {
          this.dialog.open(DialogComponent, {
            data: {
              titulo: 'Sucesso',
              mensagem: 'Forma de pagamento gravada com sucesso!'
            }
          });

          this.dialogRef.close();
        },
        (error: any) => {

          console.log(error);

          this.dialog.open(DialogComponent, {
            data: {
              titulo: 'Erro',
              mensagem: error.error.map((err: any) => err.errorMessage).join('\n')
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
          mensagem: validationErrors.join('</br>')
        }
      });
    }
  }

  private getFormValidationErrors(): string[] {
    const errors: string[] = [];
    Object.keys(this.formapagForm.controls).forEach(key => {
      const controlErrors = this.formapagForm.get(key)?.errors;
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
