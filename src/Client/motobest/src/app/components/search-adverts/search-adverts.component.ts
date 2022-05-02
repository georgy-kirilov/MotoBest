import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ModelResultModel } from '../../models/model-result-model';
import { PopulatedPlaceResultModel } from '../../models/populated-place-result-model';
import { SearchAdvertsInputModel } from '../../models/search-adverts-input-model';
import { UnitInfo } from '../../models/unit-info';
import { AdvertService } from '../../services/advert-service';
import { DisplayMessagesService } from '../../services/display-messages-service';
import { FeatureService } from '../../services/feature-service';
import { UnitService } from '../../services/unit-service';

@Component({
  selector: 'app-search-adverts',
  templateUrl: './search-adverts.component.html',
  styleUrls: ['./search-adverts.component.css']
})
export class SearchAdvertsComponent implements OnInit {
  
  input: SearchAdvertsInputModel = new SearchAdvertsInputModel();

  years: (number | null)[] = [];

  regions: Observable<string[]> = new Observable();
  brands: Observable<string[]> = new Observable();

  models: (ModelResultModel | null)[] = [];
  populatedPlaces: (PopulatedPlaceResultModel | null)[] = [];
  
  engines: (string | null)[] = [];
  transmissions: (string | null)[] = [];
  bodyStyles: (string | null)[] = [];
  conditions: (string | null)[] = [];
  colors: (string | null)[] = [];
  euroStandards: (string | null)[] = [];

  powerUnits: UnitInfo[] = [];
  currencyUnits: UnitInfo[] = [];
  mileageUnits: UnitInfo[] = [];

  constructor(
    private unitService: UnitService,
    private featureService: FeatureService,
    private advertService: AdvertService,
    public messagesService: DisplayMessagesService) { }

  ngOnInit(): void {

    this.initializeYears();

    this.brands = this.featureService.getBrands();
    this.regions = this.featureService.getRegions();

    this.unitService.getPowerUnits().subscribe(res => this.powerUnits = res);
    this.unitService.getCurrencyUnits().subscribe(res => this.currencyUnits = res);
    this.unitService.getMileageUnits().subscribe(res => this.mileageUnits = res);

    this.featureService.getEngines().subscribe(res => this.reset(this.engines, res));
    this.featureService.getEuroStandards().subscribe(res => this.reset(this.euroStandards, res));

    this.featureService.getTransmissions().subscribe(res => this.reset(this.transmissions, res));
    this.featureService.getColors().subscribe(res => this.reset(this.colors, res));

    this.featureService.getConditions().subscribe(res => this.reset(this.conditions, res));
    this.featureService.getBodyStyles().subscribe(res => this.reset(this.bodyStyles, res));

    this.loadModelsByBrand(null);
    this.loadPopulatedPlacesByRegion(null);
  }

  searchAdverts() {
    console.log(this.input);
    this.advertService.searchAdverts(this.input).subscribe(res => console.log(res));
  }

  format(option: any): string {
    const optionAsString = option?.toString();
    const formattedValue = `${optionAsString?.toUpperCase().charAt(0)}${optionAsString?.substring(1)}`;
    return option == null ? this.messagesService.defaultDropdownListOption : formattedValue;
  }

  loadPopulatedPlacesByRegion(region: string | null) {
    this.input.region = region;
    this.input.populatedPlaceId = null;
    this.featureService
      .getPopulatedPlacesByRegion(this.input.region)
      .subscribe(res => this.reset(this.populatedPlaces, res.sort(this.orderByName)));
  }

  loadModelsByBrand(brand: string | null) {
    this.input.brand = brand;
    this.input.modelId = null;
    if (brand == null) {
      this.reset(this.models, []);
      return;
    }
    this.featureService
      .getModelsByBrand(this.input.brand)
      .subscribe(res => this.reset(this.models, res.sort(this.orderByName)));
  }

  private initializeYears() {
    const minYear = 1930;
    const maxYear = new Date().getFullYear();
    this.reset(this.years, []);
    for (let year = maxYear; year >= minYear; year--) {
      this.years.push(year);
    }
  }

  private reset<T>(destination: (T | null)[], source: T[]) {
    destination.length = 0;
    destination.push(null);
    source.forEach(element => {
      destination.push(element);
    });
  }

  private orderByName(a: any, b: any) {
    return a?.name > b?.name ? 1 : -1;
  }
}
