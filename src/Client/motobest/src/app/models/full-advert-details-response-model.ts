export class FullAdvertDetailsResponseModel {
    price: number | null = null;
    power: number | null = null;
    mileage: number | null = null;
    currencyUnit: string = '';
    powerUnit: string = '';
    mileageUnit: string = '';
    originalAdvertUrl: string | null = null;
    title: string | null = null;
    description: string | null = null;
    brand: string | null = null;
    model: string | null = null;
    engine: string | null = null;
    color: string | null = null;
    condition: string | null = null;
    bodyStyle: string | null = null;
    transmission: string | null = null;
    region: string | null = null;
    populatedPlace: string | null = null;
    month: string | null = null;
    year: number | null = null;
    imageUrls: string[] = [];
}
