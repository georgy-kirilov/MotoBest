import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FullAdvertDetailsPageComponent } from './full-advert-details-page.component';

describe('FullAdvertDetailsPageComponent', () => {
  let component: FullAdvertDetailsPageComponent;
  let fixture: ComponentFixture<FullAdvertDetailsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FullAdvertDetailsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FullAdvertDetailsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
