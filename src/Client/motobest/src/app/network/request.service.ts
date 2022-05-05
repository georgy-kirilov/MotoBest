import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class RequestService {

    constructor(private http: HttpClient) { }

    get<T>(route: string, queryObject?: any | null): Observable<T> {
        const queryString = queryObject != null ? `?${this.createQueryStringFrom(queryObject)}` : '';
        return this.http.get<T>(`${environment.BASE_URL}/${route}${queryString}`);
    }

    private createQueryStringFrom(queryObject: any): string {
        return encodeURI(Object
            .entries(queryObject)
            .filter((entry: any) => entry[1])
            .map((entry: any) => `${entry[0]}=${entry[1]}`)
            .join('&'));
    }
}