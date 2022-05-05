import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchAdvertsMenuPageComponent } from './search-adverts-menu-page.component';

describe('SearchAdvertsComponent', () => {
  let component: SearchAdvertsMenuPageComponent;
  let fixture: ComponentFixture<SearchAdvertsMenuPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchAdvertsMenuPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchAdvertsMenuPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
