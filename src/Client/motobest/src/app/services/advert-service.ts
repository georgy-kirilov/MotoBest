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
        const queryString = Object.entries(input)
            .filter((entry: any) => entry.value != null)
            .map((entry: any) => `${entry.key}=${entry.value}`)
            .join('&');
        
        const url = `${this.apiRoutes.searchAdverts}?${queryString}`;
        return this.httpClient.get(url);
    }
}
