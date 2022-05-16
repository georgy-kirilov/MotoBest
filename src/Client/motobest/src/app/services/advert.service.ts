import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRoutes } from "../network/api-routes";
import { FullAdvertDetailsResponseModel } from "../models/full-advert-details.response.model";
import { SearchAdvertsInputModel } from "../models/search-adverts.input.model";
import { RequestService } from "../network/request.service";
import { GetFullAdvertInputModel } from "../models/get-full-advert.input.model";

@Injectable()
export class AdvertService {
    
    constructor(
        private requestService: RequestService,
        private apiRoutes: ApiRoutes) { }

    searchAdverts(input: SearchAdvertsInputModel): Observable<any> {
        return this.requestService.get(this.apiRoutes.searchAdverts, input);
    }

    getFullAdvert(input: GetFullAdvertInputModel): Observable<FullAdvertDetailsResponseModel> {
        return this.requestService.get(this.apiRoutes.fullAdvertDetails, input);
    }
}
