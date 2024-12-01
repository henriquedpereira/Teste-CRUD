import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors, FormArray } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LivroService } from '../../services/livro.service';
import { AssuntoService } from '../../services/assunto.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-livro-form',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule, ReactiveFormsModule, MatDialogModule],
  providers: [LivroService, AssuntoService],
  templateUrl: './livro-form.component.html',
  styleUrls: ['./livro-form.component.css']
})
export class LivroFormComponent implements OnInit {
  livroForm: FormGroup;
  id: number;
  assuntos: any[] = [];

  constructor(
    private fb: FormBuilder,
    private livroService: LivroService,
    private assuntoService: AssuntoService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<LivroFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.livroForm = this.fb.group({
      codL: [0, Validators.required],
      titulo: ['', [Validators.required, Validators.maxLength(40)]],
      editora: ['', [Validators.required, Validators.maxLength(40)]],
      edicao: [0, [Validators.required]],
      anoPublicacao: ['', [Validators.required, Validators.maxLength(4), this.yearValidator]],
      listaAssuntos: this.fb.array([]),
      listaAutores: this.fb.array([]),
      listaFormasPag: this.fb.array([]),
    });
    this.id = data.id;
  }

  ngOnInit(): void {
    this.livroService.busca(this.id).subscribe((livro: any) => {
      this.livroForm.patchValue(livro);
      this.setAssuntos(livro.listaAssuntos);
      this.setAutores(livro.listaAutores);
      this.setFormaPag(livro.listaFormasPag);
    });
  }

  get listaAssuntos(): FormArray {
    return this.livroForm.get('listaAssuntos') as FormArray;
  }

  get listaAutores(): FormArray {
    return this.livroForm.get('listaAutores') as FormArray;
  }

  get listaFormasPag(): FormArray {
    return this.livroForm.get('listaFormasPag') as FormArray;
  }

  setAssuntos(assuntos: any[]): void {
    const FormArray = this.livroForm.get('listaAssuntos') as FormArray;
    assuntos.forEach(item => {
      FormArray.push(this.fb.group({
        text: [item.text],
        value: [item.value],
        selected: [item.selected]
      }));
    });
  }

  onAssuntoChange(event: any, index: number): void {
    const FormArray = this.livroForm.get('listaAssuntos') as FormArray;
    const item = FormArray.at(index);
    item.patchValue({ selected: event.target.checked });
  }

  setAutores(autores: any[]): void {
    const FormArray = this.livroForm.get('listaAutores') as FormArray;
    autores.forEach(item => {
      FormArray.push(this.fb.group({
        text: [item.text],
        value: [item.value],
        selected: [item.selected]
      }));
    });
  }

  onAutoresChange(event: any, index: number): void {
    const FormArray = this.livroForm.get('listaAutores') as FormArray;
    const item = FormArray.at(index);
    item.patchValue({ selected: event.target.checked });
  }

  setFormaPag(formaPags: any[]): void {
    const FormArray = this.livroForm.get('listaFormasPag') as FormArray;
    formaPags.forEach(item => {
      FormArray.push(this.fb.group({
        text: [item.text],
        value: [item.value],
        selected: [item.selected],
        value2: [item.value2 ?? 0]
      }));
    });
  }

  onFormaPagChange(event: any, index: number): void {
    const FormArray = this.livroForm.get('listaFormasPag') as FormArray;
    const item = FormArray.at(index);
    item.patchValue({ selected: event.target.checked });
  }

  onSubmit(): void {
    if (this.livroForm.valid) {
      this.livroService.gravar(this.livroForm.value).subscribe(
        (response: any) => {
          this.dialog.open(DialogComponent, {
            data: {
              titulo: 'Sucesso',
              mensagem: 'Livro gravado com sucesso!'
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
    } else {
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
    Object.keys(this.livroForm.controls).forEach(key => {
      const controlErrors = this.livroForm.get(key)?.errors;
      if (controlErrors != null) {
        const fieldErrors = Object.keys(controlErrors).map(keyError => this.getErrorMessage(key, keyError, controlErrors[keyError]));
        errors.push(...fieldErrors);
      }
    });
    return errors;
  }

  private getErrorMessage(field: string, errorKey: string, errorValue: any): string {
    const fieldNames: { [key: string]: string } = {
      codL: 'Código',
      titulo: 'Título',
      editora: 'Editora',
      edicao: 'Edição',
      anoPublicacao: 'Ano de Publicação'
    };

    const messages: { [key: string]: string } = {
      required: 'é obrigatório',
      maxlength: `excede o tamanho máximo de ${errorValue.requiredLength} caracteres`,
      invalidYear: 'deve ser um ano válido de 4 dígitos'
    };

    return `${fieldNames[field]} - ${messages[errorKey] || 'erro desconhecido'}`;
  }

  private yearValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    const yearRegex = /^\d{4}$/;
    if (!yearRegex.test(value)) {
      return { invalidYear: true };
    }
    return null;
  }
}
