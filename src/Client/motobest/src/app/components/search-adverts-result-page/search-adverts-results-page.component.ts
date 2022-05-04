import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SearchAdvertsInputModel } from 'src/app/models/search-adverts-input-model';
import { SearchAdvertsResult } from 'src/app/models/search-adverts-result';
import { AdvertService } from 'src/app/services/advert-service';

@Component({
  selector: 'app-search-adverts-results-page',
  templateUrl: './search-adverts-results-page.component.html',
  styleUrls: ['./search-adverts-results-page.component.css']
})
export class SearchAdvertsResultsPageComponent implements OnInit {

  searchAdvertsResults: SearchAdvertsResult[] = []; 

  constructor(private advertService: AdvertService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(input => {
        this.advertService
          .searchAdverts(input as SearchAdvertsInputModel)
          .subscribe(resultsResponse => this.searchAdvertsResults = resultsResponse);
      });
  }

  formatImageUrl(imageUrl: string | null): string {
    return imageUrl != null ? imageUrl : '/assets/img/default-advert-image.png';
  }
}
