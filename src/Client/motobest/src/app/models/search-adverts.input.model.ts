export class SearchAdvertsInputModel {
    [name: string]: any;
    brand: string | null = null;
    modelId: number | null = null;
    engine: string | null = null;
    transmission: string | null = null;
    bodyStyle: string | null = null;
    condition: string | null = null;
    color: string | null = null;
    region: string | null = null;
    populatedPlaceId: number | null = null;
    euroStandard: string | null = null;
    minYear: number | null = null;
    maxYear: number | null = null;
    minPrice: number | null = null;
    maxPrice: number | null = null;
    minPower: number | null = null;
    maxPower: number | null = null;
    minMileage: number | null = null;
    maxMileage: number | null = null;
    currencyUnit: number = 0;
    powerUnit: number = 0;
    mileageUnit: number = 0;
}
