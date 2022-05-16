import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchAdvertsCardComponent } from './search-adverts-card.component';

describe('SearchAdvertsCardComponent', () => {
  let component: SearchAdvertsCardComponent;
  let fixture: ComponentFixture<SearchAdvertsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchAdvertsCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchAdvertsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
