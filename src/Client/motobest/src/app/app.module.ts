import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SearchAdvertsComponent } from './components/search-adverts/search-adverts.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { ApiRoutes } from './common/api-routes';
import { UnitService } from './services/unit-service';
import { FeatureService } from './services/feature-service';
import { AdvertService } from './services/advert-service';
import { FeaturesGroupDropdownComponent } from './components/features-group-dropdown/features-group-dropdown.component';
import { DisplayMessagesService } from './services/display-messages-service';

@NgModule({
  declarations: [
    AppComponent,
    SearchAdvertsComponent,
    FeaturesGroupDropdownComponent
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
    AdvertService,
    DisplayMessagesService,
    ApiRoutes
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
