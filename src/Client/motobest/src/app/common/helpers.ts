import { Injectable } from "@angular/core";

@Injectable()
export class Helpers {
    createQueryParamsForRouter(input: any): any {
        const queryInput = input;
        const queryParameters = new Map<string, string | null>();
        Object.entries(queryInput).forEach((entry: any) => queryParameters.set(entry[0], entry[1]));
        return Object.fromEntries(queryParameters);
    }

    capitalizeFirst(text: string | null): string | null {
        return text ? `${text.toUpperCase().charAt(0)}${text.substring(1)}` : null;
    }

    reduceStringLength(text: string | null, newLength: number): string | null {
        return text ? `${text.substring(0, newLength)}...` : null;
    }
}
