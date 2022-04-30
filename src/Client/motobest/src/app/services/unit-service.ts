import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRoutes } from "../common/api-routes";
import { UnitInfo } from "../models/unit-info";

@Injectable()
export class UnitService {

    constructor(private httpClient: HttpClient, private apiRoutes: ApiRoutes) { }

    getCurrencyUnits(): Observable<UnitInfo[]> {
        return this.httpClient.get<UnitInfo[]>(this.apiRoutes.allCurrencyUnits);
    }

    getPowerUnits(): Observable<UnitInfo[]> {
        return this.httpClient.get<UnitInfo[]>(this.apiRoutes.allPowerUnits);
    }

    getMileageUnits(): Observable<UnitInfo[]> {
        return this.httpClient.get<UnitInfo[]>(this.apiRoutes.allMileageUnits);
    }
}
