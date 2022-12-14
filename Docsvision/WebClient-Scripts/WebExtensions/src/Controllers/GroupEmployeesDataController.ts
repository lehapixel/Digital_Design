import { $RequestManager } from "@docsvision/webclient/System/$RequestManager";
import { serviceName } from "@docsvision/webclient/System/ServiceUtils";
import { urlStore } from "@docsvision/webclient/System/UrlStore";
import { IGroupEmployeesData } from "../Models/IGroupEmployeesData";

export class GroupEmployeesDataController {
    constructor(private services: $RequestManager) {}

    public GetGroupEmployees(groupName: string): Promise<IGroupEmployeesData> {
        let url = urlStore.urlResolver.resolveUrl("GetGroupEmployees", "GroupEmployeesData");
        let data = {
            groupName: groupName
        }
        return this.services.requestManager.post(url, JSON.stringify(data));
    }
}

export type $GroupEmployeesDataController = { GroupEmployeesDataController: GroupEmployeesDataController }
export const $GroupEmployeesDataController = serviceName((s: $GroupEmployeesDataController) => s.GroupEmployeesDataController)