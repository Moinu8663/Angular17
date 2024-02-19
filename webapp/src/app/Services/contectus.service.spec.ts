import { TestBed } from '@angular/core/testing';

import { ContectusService } from './contectus.service';

describe('ContectusService', () => {
  let service: ContectusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContectusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
