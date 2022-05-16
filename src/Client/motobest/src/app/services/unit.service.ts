import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiRoutes } from "../network/api-routes";
import { UnitInfo } from "../models/unit-info";
import { RequestService } from "../network/request.service";

@Injectable()
export class UnitService {

    private readonly unitValuesByNames: Map<string, number> = new Map([
        ["bgn", 0],
        ["usd", 1],
        ["eur", 2],
        ["km", 0],
        ["mi", 1],
        ["hp", 0],
        ["kw", 1],
        ["ps", 2],
    ]);

    constructor(
        private requestService: RequestService,
        private apiRoutes: ApiRoutes) { }

    getCurrencyUnits(): Observable<UnitInfo[]> {
        return this.requestService.get(this.apiRoutes.allCurrencyUnits);
    }

    getPowerUnits(): Observable<UnitInfo[]> {
        return this.requestService.get(this.apiRoutes.allPowerUnits);
    }

    getMileageUnits(): Observable<UnitInfo[]> {
        return this.requestService.get(this.apiRoutes.allMileageUnits);
    }

    fromName(unitName: string): number {
        unitName = unitName.toLowerCase();
        const valueByName = this.unitValuesByNames.get(unitName);
        return this.unitValuesByNames.has(unitName) ? valueByName as number : 0;
    }
}
