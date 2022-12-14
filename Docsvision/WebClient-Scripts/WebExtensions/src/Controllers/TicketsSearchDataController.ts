import { $RequestManager } from "@docsvision/webclient/System/$RequestManager";
import { serviceName } from "@docsvision/webclient/System/ServiceUtils";
import { urlStore } from "@docsvision/webclient/System/UrlStore";
import { ITicketsSearchData } from "../Models/ITicketsSearchData";

export class TicketsSearchDataController {
    constructor(private services: $RequestManager) {}

    public GetTicketsSearch(destination: string, dateFrom: string, dateTo: string,): Promise<ITicketsSearchData> {
        let url = urlStore.urlResolver.resolveUrl("GetTicketsSearch", "TicketsSearchData");
        let data = {
            destination : destination,
            dateFrom : dateFrom,
            dateTo : dateTo
        }
        return this.services.requestManager.post(url, JSON.stringify(data));
    }
}

export type $TicketsSearchDataController = { TicketsSearchDataController: TicketsSearchDataController }
export const $TicketsSearchDataController = serviceName((s: $TicketsSearchDataController) => s.TicketsSearchDataController)