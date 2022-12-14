import { $RequestManager } from "@docsvision/webclient/System/$RequestManager";
import { serviceName } from "@docsvision/webclient/System/ServiceUtils";
import { urlStore } from "@docsvision/webclient/System/UrlStore";
import { ICityInfoData } from "../Models/ICityInfoData";

export class CityInfoDataController {
    constructor(private services: $RequestManager) {}

    public GetCityInfo(cityId: string): Promise<ICityInfoData> {
        let url = urlStore.urlResolver.resolveUrl("GetCityInfo", "CityInfoData");
        let data = {
            cityId: cityId
        }
        return this.services.requestManager.post(url, JSON.stringify(data));
    }
}

export type $CityInfoDataController = { CityInfoDataController: CityInfoDataController }
export const $CityInfoDataController = serviceName((s: $CityInfoDataController) => s.CityInfoDataController)