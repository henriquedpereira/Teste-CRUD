import { TestBed } from '@angular/core/testing';

import { FormapagService } from './formapag.service';

describe('FormapagService', () => {
  let service: FormapagService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormapagService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
