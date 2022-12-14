import * as EventHandlers from "./EventHandlers";
import { extensionManager } from "@docsvision/webclient/System/ExtensionManager";
import { $RequestManager } from "@docsvision/webclient/System/$RequestManager";
import { $CustomEmployeeDataController, CustomEmployeeDataController } from "./Controllers/CustomEmployeeDataController";
import { Service } from "@docsvision/webclient/System/Service";
import { $CityInfoDataController, CityInfoDataController } from "./Controllers/CityInfoDataController";
import { $GroupEmployeesDataController, GroupEmployeesDataController } from "./Controllers/GroupEmployeesDataController";
import { $TicketsSearchDataController, TicketsSearchDataController } from "./Controllers/TicketsSearchDataController";
import { $SendForAgreementDataController, SendForAgreementDataController } from "./Controllers/SendForAgreementDataController";


// Главная входная точка всего расширения
// Данный файл должен импортировать прямо или косвенно все остальные файлы, 
// чтобы rollup смог собрать их все в один бандл.

// Регистрация расширения позволяет корректно установить все
// обработчики событий, сервисы и прочие сущности web-приложения.
extensionManager.registerExtension({
    name: "Request for Business Trip",
    version: "5.5.16",
    globalEventHandlers: [ EventHandlers ],
    layoutServices: [
        Service.fromFactory($CustomEmployeeDataController, (services: $RequestManager) => new CustomEmployeeDataController(services)),
        Service.fromFactory($CityInfoDataController, (services: $RequestManager) => new CityInfoDataController(services)),
        Service.fromFactory($GroupEmployeesDataController, (services: $RequestManager) => new GroupEmployeesDataController(services)),
        Service.fromFactory($TicketsSearchDataController, (services: $RequestManager) => new TicketsSearchDataController(services)),
        Service.fromFactory($SendForAgreementDataController, (services: $RequestManager) => new SendForAgreementDataController(services))
    ]
});