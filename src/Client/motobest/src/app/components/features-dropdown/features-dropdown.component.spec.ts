import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturesDropdownComponent } from './features-dropdown.component';

describe('FeaturesDropdownComponent', () => {
  let component: FeaturesDropdownComponent;
  let fixture: ComponentFixture<FeaturesDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeaturesDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FeaturesDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
