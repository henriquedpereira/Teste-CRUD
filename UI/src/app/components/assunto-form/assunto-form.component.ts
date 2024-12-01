import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
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
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.assuntoForm = this.fb.group({
      codAs: [0, Validators.required],
      descricao: ['', [Validators.required, Validators.maxLength(20)]]
    });
    this.id = data.id;
  }

  ngOnInit(): void {
    if (this.id !== 0) {
      this.assuntoService.getAssunto(this.id).subscribe((assunto: any) => {
        this.assuntoForm.patchValue(assunto);
      });
    }
  }

  onSubmit(): void {
    if (this.assuntoForm.valid) {
      this.assuntoService.GravarAssunto(this.assuntoForm.value).subscribe(
        (response: any) => {         
          this.dialog.open(DialogComponent, {
            data: {
              title: 'Success',
              text: 'Assunto gravado com sucesso!'
            }
          });
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
    }
  }
}
