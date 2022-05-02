import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturesGroupDropdownComponent } from './features-group-dropdown.component';

describe('FeaturesDropdownComponent', () => {
  let component: FeaturesGroupDropdownComponent;
  let fixture: ComponentFixture<FeaturesGroupDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeaturesGroupDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FeaturesGroupDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
