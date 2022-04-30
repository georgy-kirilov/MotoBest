import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SearchAdvertsComponent } from './search-adverts/search-adverts.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { ApiRoutes } from './common/api-routes';
import { UnitService } from './services/unit-service';
import { FeatureService } from './services/feature-service';

@NgModule({
  declarations: [
    AppComponent,
    SearchAdvertsComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [
    UnitService,
    FeatureService,
    ApiRoutes,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
