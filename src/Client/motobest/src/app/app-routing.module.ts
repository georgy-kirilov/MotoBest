import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchAdvertsComponent } from './search-adverts/search-adverts.component';

const routes: Routes = [
  { path: 'search', component: SearchAdvertsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
