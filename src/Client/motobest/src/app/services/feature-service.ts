import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRoutes } from "../common/api-routes";
import { ModelResultModel } from "../models/model-result-model";
import { PopulatedPlaceResultModel } from "../models/populated-place-result-model";

@Injectable()
export class FeatureService {

    constructor(
        private httpClient: HttpClient,
        private apiRoutes: ApiRoutes) { }

    getBrands(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allBrands);
    }

    getEngines(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allEngines);
    }

    getTransmissions(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allTransmissions);
    }

    getBodyStyles(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allBodyStyles);
    }

    getConditions(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allConditions);
    }

    getColors(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allColors);
    }

    getRegions(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allRegions);
    }

    getEuroStandards(): Observable<string[]> {
        return this.getFeatures(this.apiRoutes.allEuroStandards);
    }

    getPopulatedPlacesByRegion(region: string | null): Observable<PopulatedPlaceResultModel[]> {
        return this.getFeatures(`${this.apiRoutes.allPopulatedPlaces}/${region}`);
    }

    getModelsByBrand(brand: string | null): Observable<ModelResultModel[]> {
        return this.getFeatures(`${this.apiRoutes.allModels}/${brand}`);
    }

    private getFeatures<T>(url: string): Observable<T[]> {
        return this.httpClient.get<T[]>(url);
    }
}
