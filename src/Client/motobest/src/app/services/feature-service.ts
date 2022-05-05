import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRoutes } from "../network/api-routes";
import { FeatureResponseModel } from "../models/feature-response-model";
import { RequestService } from "../network/request.service";

@Injectable()
export class FeatureService {

    constructor(
        private requestService: RequestService,
        private apiRoutes: ApiRoutes) { }

    getBrands(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allBrands);
    }

    getEngines(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allEngines);
    }

    getTransmissions(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allTransmissions);
    }

    getBodyStyles(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allBodyStyles);
    }

    getConditions(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allConditions);
    }

    getColors(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allColors);
    }

    getRegions(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allRegions);
    }

    getEuroStandards(): Observable<FeatureResponseModel[]> {
        return this.getFeatures(this.apiRoutes.allEuroStandards);
    }

    getPopulatedPlacesByRegion(region: string | null): Observable<FeatureResponseModel[]> {
        return this.getFeatures(`${this.apiRoutes.allPopulatedPlaces}/${region}`);
    }

    getModelsByBrand(brand: string | null): Observable<FeatureResponseModel[]> {
        return this.getFeatures(`${this.apiRoutes.allModels}/${brand}`);
    }

    private getFeatures<T>(url: string): Observable<T> {
        return this.requestService.get(url);
    }
}
