import { environment } from "src/environments/environment"


const baseRoutes = {
    units: `Units`,
    features: `Features`,
    adverts: `Adverts`,
}

export class ApiRoutes {

    get allCurrencyUnits(): string {
        return `${baseRoutes.units}/currency/all`;
    }
    
    get allPowerUnits(): string {
        return `${baseRoutes.units}/power/all`;
    }

    get allMileageUnits(): string {
        return `${baseRoutes.units}/mileage/all`;
    }

    get allBrands(): string {
        return `${baseRoutes.features}/brands`;
    }

    get allEngines(): string {
        return `${baseRoutes.features}/engines`;
    }

    get allTransmissions(): string {
        return `${baseRoutes.features}/transmissions`;
    }

    get allBodyStyles(): string {
        return `${baseRoutes.features}/body-styles`;
    }

    get allConditions(): string {
        return `${baseRoutes.features}/conditions`;
    }

    get allColors(): string {
        return `${baseRoutes.features}/colors`;
    }

    get allRegions(): string {
        return `${baseRoutes.features}/regions`;
    }

    get allEuroStandards(): string {
        return `${baseRoutes.features}/euro-standards`;
    }

    get allPopulatedPlaces(): string {
        return `${baseRoutes.features}/populated-places`;
    }

    get allModels(): string {
        return `${baseRoutes.features}/models`;
    }

    get searchAdverts(): string {
        return `${baseRoutes.adverts}/search`;
    }

    get fullAdvertDetails(): string {
        return `${baseRoutes.adverts}`;
    }
}
