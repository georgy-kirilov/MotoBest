import { Injectable } from "@angular/core";

@Injectable()
export class Helpers {
    createQueryParamsForRouter(input: any): any {
        const queryInput = input;
        const queryParameters = new Map<string, string | null>();
        Object.entries(queryInput).forEach((entry: any) => queryParameters.set(entry[0], entry[1]));
        return Object.fromEntries(queryParameters);
    }
}
