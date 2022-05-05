import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Helpers } from 'src/app/common/helpers';
import { GetFullAdvertInputModel } from 'src/app/models/get-full-advert.input.model';
import { SearchAdvertsInputModel } from 'src/app/models/search-adverts.input.model';
import { SearchAdvertsResult } from 'src/app/models/search-adverts-result';
import { AdvertService } from 'src/app/services/advert.service';
import { UnitService } from 'src/app/services/unit.service';

@Component({
  selector: 'app-search-adverts-results-page',
  templateUrl: './search-adverts-results-page.component.html',
  styleUrls: ['./search-adverts-results-page.component.css']
})
export class SearchAdvertsResultsPageComponent implements OnInit {

  private input: GetFullAdvertInputModel = new GetFullAdvertInputModel();
  searchAdvertsResults: SearchAdvertsResult[] = [];
  boxShadowClasses: string[] = [];

  constructor(
    private advertService: AdvertService,
    private route: ActivatedRoute,
    private router: Router,
    private helpers: Helpers,
    private unitService: UnitService) { }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(input => {
        const result = this.advertService.searchAdverts(input as SearchAdvertsInputModel)
        result.subscribe(responseResults => {
          this.searchAdvertsResults = responseResults;
          this.boxShadowClasses = new Array(this.searchAdvertsResults.length).fill('shadow');
          console.log(this.boxShadowClasses)
        });
      });
  }

  viewFullAdvert(selectedAdvertIndex: number) {
    const selectedAdvert = this.searchAdvertsResults[selectedAdvertIndex];
    this.input.id = selectedAdvert.id;
    this.input.currencyUnit = this.unitService.fromName(selectedAdvert.currencyUnit);
    this.input.powerUnit = this.unitService.fromName(selectedAdvert.powerUnit);
    this.input.mileageUnit = this.unitService.fromName(selectedAdvert.mileageUnit);
    this.router.navigate(['adverts'], {
      queryParams: this.helpers.createQueryParamsForRouter(this.input)
    });
  }

  changeStyle($event: any, index: number) {
    this.boxShadowClasses[index] = $event.type == 'mouseover' ? 'shadow-lg' : 'shadow';
  }

  formatModifiedOn(modifiedOn: Date | null): string {
    if (modifiedOn == null) {
      return '';
    }
    return modifiedOn.toString().split("T")[0];
  }
}
