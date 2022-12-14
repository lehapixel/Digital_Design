import { GenModels } from "@docsvision/webclient/Generated/DocsVision.WebClient.Models";

export interface ICustomEmployeeData {
    phone: string,
    unit: GenModels.DepartmentModel,
    manager: GenModels.EmployeeDataModel
}