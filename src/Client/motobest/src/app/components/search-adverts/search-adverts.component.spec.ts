import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchAdvertsComponent } from './search-adverts.component';

describe('SearchAdvertsComponent', () => {
  let component: SearchAdvertsComponent;
  let fixture: ComponentFixture<SearchAdvertsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchAdvertsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchAdvertsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
