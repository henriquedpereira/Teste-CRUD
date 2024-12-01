import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormapagFormComponent } from './formapag-form.component';

describe('FormapagFormComponent', () => {
  let component: FormapagFormComponent;
  let fixture: ComponentFixture<FormapagFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormapagFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormapagFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
