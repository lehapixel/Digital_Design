import { $RequestManager } from "@docsvision/webclient/System/$RequestManager";
import { serviceName } from "@docsvision/webclient/System/ServiceUtils";
import { urlStore } from "@docsvision/webclient/System/UrlStore";
import { ISendForAgreementData } from "../Models/ISendForAgreementData";

export class SendForAgreementDataController {
    constructor(private services: $RequestManager) {}

    public GetSendForAgreement(cardId: string): Promise<ISendForAgreementData> {
        let url = urlStore.urlResolver.resolveUrl("GetSendForAgreement", "SendForAgreementData");
        let data = {
            cardId : cardId
        }
        return this.services.requestManager.post(url, JSON.stringify(data));
    }
}

export type $SendForAgreementDataController = { SendForAgreementDataController: SendForAgreementDataController }
export const $SendForAgreementDataController = serviceName((s: $SendForAgreementDataController) => s.SendForAgreementDataController)