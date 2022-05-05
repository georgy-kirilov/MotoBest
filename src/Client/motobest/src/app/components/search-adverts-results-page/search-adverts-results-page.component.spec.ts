import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchAdvertsResultsPageComponent } from './search-adverts-results-page.component';

describe('SearchAdvertsResultsPageComponent', () => {
  let component: SearchAdvertsResultsPageComponent;
  let fixture: ComponentFixture<SearchAdvertsResultsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchAdvertsResultsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchAdvertsResultsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
