import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRoutes } from "../common/api-routes";
import { SearchAdvertsInputModel } from "../models/search-adverts-input-model";

@Injectable()
export class AdvertService {
    
    constructor(
        private httpClient: HttpClient,
        private apiRoutes: ApiRoutes) { }

    searchAdverts(input: SearchAdvertsInputModel): Observable<any> {
        const rawQueryString = this.createSearchAdvertsQueryString(input);
        const encodedQueryString = encodeURI(rawQueryString);
        const url = `${this.apiRoutes.searchAdverts}?${encodedQueryString}`;
        return this.httpClient.get(url);
    }

    private createSearchAdvertsQueryString(input: SearchAdvertsInputModel): string {
        const key = 0, value = 1;
        return Object
            .entries(input)
            .filter((entry: any) => entry[value])
            .map((entry: any) => `${entry[key]}=${entry[value]}`)
            .join('&');
    }
}
