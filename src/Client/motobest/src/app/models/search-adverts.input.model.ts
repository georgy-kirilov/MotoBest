export class SearchAdvertsInputModel {
    [name: string]: any;
    brandId: number | null = null;
    modelId: number | null = null;
    engineId: number | null = null;
    transmissionId: number | null = null;
    bodyStyleId: number | null = null;
    conditionId: number | null = null;
    colorId: number | null = null;
    regionId: number | null = null;
    populatedPlaceId: number | null = null;
    euroStandardId: number | null = null;
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
