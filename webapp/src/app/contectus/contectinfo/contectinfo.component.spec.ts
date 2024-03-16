import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContectinfoComponent } from './contectinfo.component';

describe('ContectinfoComponent', () => {
  let component: ContectinfoComponent;
  let fixture: ComponentFixture<ContectinfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContectinfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ContectinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
