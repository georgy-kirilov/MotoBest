import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Helpers } from 'src/app/common/helpers';
import { FeatureResponseModel } from 'src/app/models/feature.response.model';
import { SearchAdvertsInputModel } from '../../models/search-adverts.input.model';
import { UnitInfo } from '../../models/unit-info';
import { DisplayMessagesService } from '../../services/display-messages.service';
import { FeatureService } from '../../services/feature.service';
import { UnitService } from '../../services/unit.service';

@Component({
  selector: 'app-search-adverts',
  templateUrl: './search-adverts-menu-page.component.html',
  styleUrls: ['./search-adverts-menu-page.component.css']
})
export class SearchAdvertsMenuPageComponent implements OnInit {
  
  input: SearchAdvertsInputModel = new SearchAdvertsInputModel();
  
  years: (number | null)[] = [];

  regions: Observable<FeatureResponseModel[]> = new Observable();
  brands: Observable<FeatureResponseModel[]> = new Observable();
  colors: Observable<FeatureResponseModel[]> = new Observable();

  models: Observable<FeatureResponseModel[]> = new Observable();
  populatedPlaces: Observable<FeatureResponseModel[]> = new Observable();
  
  engines: Observable<FeatureResponseModel[]> = new Observable();
  transmissions: Observable<FeatureResponseModel[]> = new Observable();
  bodyStyles: Observable<FeatureResponseModel[]> = new Observable();

  conditions: Observable<FeatureResponseModel[]> = new Observable();
  euroStandards: Observable<FeatureResponseModel[]> = new Observable();

  powerUnits: UnitInfo[] = [];
  currencyUnits: UnitInfo[] = [];
  mileageUnits: UnitInfo[] = [];

  constructor(
    private unitService: UnitService,
    private featureService: FeatureService,
    private router: Router,
    private helpers: Helpers,
    public messagesService: DisplayMessagesService) { }

  ngOnInit(): void {

    this.initializeYears();

    this.unitService.getPowerUnits().subscribe(res => this.powerUnits = res);
    this.unitService.getCurrencyUnits().subscribe(res => this.currencyUnits = res);
    this.unitService.getMileageUnits().subscribe(res => this.mileageUnits = res);

    this.brands = this.featureService.getBrands();
    this.regions = this.featureService.getRegions();

    this.colors = this.featureService.getColors();
    this.engines = this.featureService.getEngines();

    this.euroStandards = this.featureService.getEuroStandards();
    this.transmissions = this.featureService.getTransmissions();

    this.conditions = this.featureService.getConditions();
    this.bodyStyles = this.featureService.getBodyStyles();

    this.loadModelsByBrand();
    this.loadPopulatedPlacesByRegion();
  }

  searchAdverts() {
    this.router.navigate(['/search/results'], {
      queryParams: this.helpers.createQueryParamsForRouter(this.input)
    });
  }

  onFeaturesDropdownChange($event: any) {
    this.input[$event.propertyName] = $event.option;
  }

  format(option: any): string {
    const optionAsString = option?.toString();
    const formattedValue = `${optionAsString?.toUpperCase().charAt(0)}${optionAsString?.substring(1)}`;
    return option == null ? this.messagesService.defaultDropdownListOption : formattedValue;
  }

  loadPopulatedPlacesByRegion($event?: any | null) {
    this.input.populatedPlaceId = null;
    this.input.region = $event != null ? $event.option : null;
    this.populatedPlaces = this.featureService.getPopulatedPlacesByRegion(this.input.region);
  }

  loadModelsByBrand($event?: any | null) {
    this.input.modelId = null;
    this.input.brand = $event != null ? $event.option : null;
    this.models = this.featureService.getModelsByBrand(this.input.brand);
  }

  private initializeYears() {
    const minYear = 1930;
    const maxYear = new Date().getFullYear();
    this.years.length = 0;
    this.years.push(null);
    for (let year = maxYear; year >= minYear; year--) {
      this.years.push(year);
    }
  }
}
