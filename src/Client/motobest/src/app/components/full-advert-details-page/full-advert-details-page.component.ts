import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Helpers } from 'src/app/common/helpers';
import { FeatureViewModel } from 'src/app/models/feature.view.model';
import { FullAdvertDetailsResponseModel } from 'src/app/models/full-advert-details.response.model';
import { GetFullAdvertInputModel } from 'src/app/models/get-full-advert.input.model';
import { AdvertService } from 'src/app/services/advert.service';
import { DisplayMessagesService } from 'src/app/services/display-messages.service';

@Component({
  selector: 'app-full-advert-details-page',
  templateUrl: './full-advert-details-page.component.html',
  styleUrls: ['./full-advert-details-page.component.css']
})
export class FullAdvertDetailsPageComponent implements OnInit {

  readonly culture: string = 'bg-BG';

  title: string | null = null;
  description: string | null = null;
  price: string | null = null;
  imageUrls: string[] = [];
  features: FeatureViewModel[] = [];

  constructor(
    private route: ActivatedRoute,
    private advertService: AdvertService,
    public messageService: DisplayMessagesService,
    public helpers: Helpers) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(input => {
      this.advertService
        .getFullAdvert(input as GetFullAdvertInputModel)
        .subscribe(response => {
          this.title = response.title;
          this.description = response.description;
          this.price = response.price ? `${response.price.toLocaleString(this.culture)} ${response.currencyUnit.toUpperCase()}` : null;
          this.imageUrls = response.imageUrls;
          this.features = [
            { name: this.messageService.brand, value: response.brand },
            { name: this.messageService.model, value: response.model },
            { name: this.messageService.bodyStyle, value: response.bodyStyle },
            { name: this.messageService.engine, value: response.engine },
            { name: this.messageService.transmission, value: response.transmission },
            { name: this.messageService.condition, value: response.condition },
            { name: this.messageService.color, value: response.color },
          ];
          this.features.forEach(feat => feat.value = this.helpers.capitalizeFirst(feat.value));
          this.features.push(
            { name: this.messageService.mileage, value: `${response.mileage?.toLocaleString(this.culture)} ${response.mileageUnit}` },
            { name: this.messageService.manufacturedOn, value: `${response.month} ${response.year}` },
            { name: this.messageService.power, value: `${response.power} ${response.powerUnit}` },
          );
        });
    });
  }
}
