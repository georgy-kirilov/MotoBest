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
        const rawQueryString = Object.entries(input)
            .filter((entry: any) => entry[1] != null && entry[0] != 'euroStandard')
            .map((entry: any) => `${entry[0]}=${entry[1]}`)
            .join('&');
        
        const encodedQueryString = encodeURI(rawQueryString);
        const url = `${this.apiRoutes.searchAdverts}?${encodedQueryString}`;
        return this.httpClient.get(url);
    }
}
