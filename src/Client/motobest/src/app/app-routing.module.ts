import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FullAdvertDetailsPageComponent } from './components/full-advert-details-page/full-advert-details-page.component';
import { SearchAdvertsResultsPageComponent as SearchAdvertsResultsPageComponent } from './components/search-adverts-result-page/search-adverts-results-page.component';
import { SearchAdvertsComponent } from './components/search-adverts/search-adverts.component';

const routes: Routes = [
  { path: 'search', component: SearchAdvertsComponent },
  { path: 'search/results', component: SearchAdvertsResultsPageComponent },
  { path: 'adverts', component: FullAdvertDetailsPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
