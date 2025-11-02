import { TestBed } from '@angular/core/testing';

import { TaskRegisterService } from './task-register.service';

describe('TaskRegisterService', () => {
  let service: TaskRegisterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaskRegisterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
