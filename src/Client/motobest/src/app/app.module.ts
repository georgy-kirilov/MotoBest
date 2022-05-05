import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SearchAdvertsMenuPageComponent } from './components/search-adverts-menu-page/search-adverts-menu-page.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { ApiRoutes } from './network/api-routes';
import { UnitService } from './services/unit.service';
import { FeatureService } from './services/feature.service';
import { AdvertService } from './services/advert.service';
import { DisplayMessagesService } from './services/display-messages.service';
import { SearchAdvertsCardComponent } from './components/search-adverts-card/search-adverts-card.component';
import { SearchAdvertsResultsPageComponent } from './components/search-adverts-result-page/search-adverts-results-page.component';
import { FullAdvertDetailsPageComponent } from './components/full-advert-details-page/full-advert-details-page.component';
import { RequestService } from './network/request.service';
import { Helpers } from './common/helpers';
import { FeaturesDropdownComponent } from './components/features-dropdown/features-dropdown.component';

@NgModule({
  declarations: [
    AppComponent,
    SearchAdvertsMenuPageComponent,
    SearchAdvertsCardComponent,
    SearchAdvertsResultsPageComponent,
    FullAdvertDetailsPageComponent,
    FeaturesDropdownComponent
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
    RequestService,
    ApiRoutes,
    Helpers,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
