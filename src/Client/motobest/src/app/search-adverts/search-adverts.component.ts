import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ModelResultModel } from '../models/model-result-model';
import { PopulatedPlaceResultModel } from '../models/populated-place-result-model';
import { SearchAdvertsInputModel } from '../models/search-adverts-input-model';
import { UnitInfo } from '../models/unit-info';
import { FeatureService } from '../services/feature-service';
import { UnitService } from '../services/unit-service';

@Component({
  selector: 'app-search-adverts',
  templateUrl: './search-adverts.component.html',
  styleUrls: ['./search-adverts.component.css']
})
export class SearchAdvertsComponent implements OnInit {

  readonly anyOptionText = 'Няма';
  readonly anySelectOptionText = 'Без значение';

  input: SearchAdvertsInputModel = new SearchAdvertsInputModel();

  brands: Array<string | null> = [];
  models: Array<ModelResultModel | null> = [];

  engines: Array<string | null> = [];
  transmissions: Array<string | null> = [];

  bodyStyles: Array<string | null> = [];
  conditions: Array<string | null> = [];

  colors: (string | null)[] = [];
  regions: (string | null)[] = [];

  euroStandards: (string | null)[] = [];
  populatedPlaces: (PopulatedPlaceResultModel | null)[] = [];

  years: (number | null)[] = [];
  currencyUnits: UnitInfo[] = [];

  powerUnits: UnitInfo[] = [];
  mileageUnits: UnitInfo[] = [];

  constructor(
    private unitService: UnitService,
    private featureService: FeatureService) { }

  ngOnInit(): void {

    this.initializeYears();

    this.unitService.getPowerUnits().subscribe(res => this.powerUnits = res);
    this.unitService.getCurrencyUnits().subscribe(res => this.currencyUnits = res);
    this.unitService.getMileageUnits().subscribe(res => this.mileageUnits = res);

    const optionsListsEntries = [
      { key: this.brands, value: this.featureService.getBrands() },
      { key: this.engines, value: this.featureService.getEngines() },
      { key: this.transmissions, value: this.featureService.getTransmissions() },
      { key: this.bodyStyles, value: this.featureService.getBodyStyles() },
      { key: this.conditions, value: this.featureService.getConditions() },
      { key: this.colors, value: this.featureService.getColors() },
      { key: this.regions, value: this.featureService.getRegions() },
      { key: this.euroStandards, value: this.featureService.getEuroStandards() },
    ];

    optionsListsEntries.forEach(e => this.initializeOptionsList(e.key, e.value));

    this.loadModelsByBrand();
    this.loadPopulatedPlacesByRegion();
  }

  searchAdverts() {
    console.log(this.input);
  }

  format(option: any): string {
    const optionAsString = option?.toString();
    const formattedValue = `${optionAsString?.toUpperCase().charAt(0)}${optionAsString?.substring(1)}`;
    return option == null ? this.anySelectOptionText : formattedValue;
  }

  loadPopulatedPlacesByRegion() {
    const observablesList = this.featureService.getPopulatedPlacesByRegion(this.input.region);
    this.initializeOptionsList(this.populatedPlaces, observablesList);
  }

  loadModelsByBrand() {
    const observablesList = this.featureService.getModelsByBrand(this.input.brand);
    this.initializeOptionsList(this.models, observablesList);
  }

  private initializeOptionsList<T>(optionsList: (T | null)[], observablesList: Observable<T[]>) {
    optionsList.length = 0;
    optionsList.push(null);
    observablesList.subscribe(responseOptions => {
      responseOptions.forEach(option => optionsList.push(option));
    });
  }

  private initializeYears() {
    const minYear = 1930;
    const maxYear = new Date().getFullYear();
    this.years = [];
    this.years.push(null);
    for (let year = maxYear; year >= minYear; year--) {
      this.years.push(year);
    }
  }
}
