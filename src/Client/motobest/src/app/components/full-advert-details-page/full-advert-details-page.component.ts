import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FullAdvertDetailsResponseModel } from 'src/app/models/full-advert-details-response-model';
import { GetFullAdvertInputModel } from 'src/app/models/get-full-advert.input.model';
import { AdvertService } from 'src/app/services/advert.service';

@Component({
  selector: 'app-full-advert-details-page',
  templateUrl: './full-advert-details-page.component.html',
  styleUrls: ['./full-advert-details-page.component.css']
})
export class FullAdvertDetailsPageComponent implements OnInit {

  private advert: FullAdvertDetailsResponseModel = new FullAdvertDetailsResponseModel();

  constructor(
    private route: ActivatedRoute,
    private advertService: AdvertService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(input => {
      this.advertService
        .getFullAdvert(input as GetFullAdvertInputModel)
        .subscribe(responseModel => {
          this.advert = responseModel
          console.log(this.advert);
        });
    });
  }
}
