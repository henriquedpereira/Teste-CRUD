import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormapagListComponent } from './formapag-list.component';

describe('FormapagListComponent', () => {
  let component: FormapagListComponent;
  let fixture: ComponentFixture<FormapagListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormapagListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormapagListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
