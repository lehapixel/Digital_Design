define(['tslib', '@docsvision/webclient/Helpers/MessageBox/MessageBox', '@docsvision/webclient/System/$Router', 'moment', '@docsvision/webclient/System/ServiceUtils', '@docsvision/webclient/System/UrlStore', '@docsvision/webclient/System/ExtensionManager', '@docsvision/webclient/System/Service'], (function (tslib, MessageBox, $Router, moment, ServiceUtils, UrlStore, ExtensionManager, Service) { 'use strict';

    function _interopDefaultLegacy (e) { return e && typeof e === 'object' && 'default' in e ? e : { 'default': e }; }

    var moment__default = /*#__PURE__*/_interopDefaultLegacy(moment);

    var CityInfoDataController = /** @class */ (function () {
        function CityInfoDataController(services) {
            this.services = services;
        }
        CityInfoDataController.prototype.GetCityInfo = function (cityId) {
            var url = UrlStore.urlStore.urlResolver.resolveUrl("GetCityInfo", "CityInfoData");
            var data = {
                cityId: cityId
            };
            return this.services.requestManager.post(url, JSON.stringify(data));
        };
        return CityInfoDataController;
    }());
    var $CityInfoDataController = ServiceUtils.serviceName(function (s) { return s.CityInfoDataController; });

    var CustomEmployeeDataController = /** @class */ (function () {
        function CustomEmployeeDataController(services) {
            this.services = services;
        }
        CustomEmployeeDataController.prototype.GetEmployeeData = function (employeeId) {
            var url = UrlStore.urlStore.urlResolver.resolveUrl("GetEmployeeData", "CustomEmployeeData");
            var data = {
                employeeId: employeeId
            };
            return this.services.requestManager.post(url, JSON.stringify(data));
        };
        return CustomEmployeeDataController;
    }());
    var $CustomEmployeeDataController = ServiceUtils.serviceName(function (s) { return s.CustomEmployeeDataController; });

    var GroupEmployeesDataController = /** @class */ (function () {
        function GroupEmployeesDataController(services) {
            this.services = services;
        }
        GroupEmployeesDataController.prototype.GetGroupEmployees = function (groupName) {
            var url = UrlStore.urlStore.urlResolver.resolveUrl("GetGroupEmployees", "GroupEmployeesData");
            var data = {
                groupName: groupName
            };
            return this.services.requestManager.post(url, JSON.stringify(data));
        };
        return GroupEmployeesDataController;
    }());
    var $GroupEmployeesDataController = ServiceUtils.serviceName(function (s) { return s.GroupEmployeesDataController; });

    var SendForAgreementDataController = /** @class */ (function () {
        function SendForAgreementDataController(services) {
            this.services = services;
        }
        SendForAgreementDataController.prototype.GetSendForAgreement = function (cardId) {
            var url = UrlStore.urlStore.urlResolver.resolveUrl("GetSendForAgreement", "SendForAgreementData");
            var data = {
                cardId: cardId
            };
            return this.services.requestManager.post(url, JSON.stringify(data));
        };
        return SendForAgreementDataController;
    }());
    var $SendForAgreementDataController = ServiceUtils.serviceName(function (s) { return s.SendForAgreementDataController; });

    var TicketsSearchDataController = /** @class */ (function () {
        function TicketsSearchDataController(services) {
            this.services = services;
        }
        TicketsSearchDataController.prototype.GetTicketsSearch = function (destination, dateFrom, dateTo) {
            var url = UrlStore.urlStore.urlResolver.resolveUrl("GetTicketsSearch", "TicketsSearchData");
            var data = {
                destination: destination,
                dateFrom: dateFrom,
                dateTo: dateTo
            };
            return this.services.requestManager.post(url, JSON.stringify(data));
        };
        return TicketsSearchDataController;
    }());
    var $TicketsSearchDataController = ServiceUtils.serviceName(function (s) { return s.TicketsSearchDataController; });

    function dateFromTo_DataChanged(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, dateFromControl, dateToControl, numberOfDaysToControl, dateFrom, dateTo, time, directoryCityControl, sumOfBusinessTripsMoneyControl, cityInfoService, model, ticketsPriceControl;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        dateFromControl = layout.controls.tryGet("dateFrom");
                        dateToControl = layout.controls.tryGet("dateTo");
                        numberOfDaysToControl = layout.controls.tryGet("numberOfDays");
                        if (!dateFromControl || !dateToControl || !numberOfDaysToControl) {
                            return [2 /*return*/];
                        }
                        if (dateFromControl.params.value != null && dateToControl.params.value != null) {
                            dateFrom = dateFromControl.params.value;
                            dateTo = dateToControl.params.value;
                            if (dateFrom <= dateTo) {
                                time = ((dateTo.valueOf() - dateFrom.valueOf()) / (1000 * 3600 * 24)) + 1;
                                numberOfDaysToControl.params.value = time;
                            }
                            else {
                                numberOfDaysToControl.params.value = null;
                            }
                        }
                        else {
                            numberOfDaysToControl.params.value = null;
                        }
                        directoryCityControl = layout.controls.tryGet("directoryCity");
                        sumOfBusinessTripsMoneyControl = layout.controls.tryGet("sumOfBusinessTripsMoney");
                        if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) {
                            return [2 /*return*/];
                        }
                        if (!(directoryCityControl.hasValue() && numberOfDaysToControl != null)) return [3 /*break*/, 2];
                        cityInfoService = layout.getService($CityInfoDataController);
                        return [4 /*yield*/, cityInfoService.GetCityInfo(directoryCityControl.params.value.id)];
                    case 1:
                        model = _a.sent();
                        if (model) {
                            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
                        }
                        else {
                            sumOfBusinessTripsMoneyControl.params.value = null;
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        sumOfBusinessTripsMoneyControl.params.value = null;
                        _a.label = 3;
                    case 3:
                        ticketsPriceControl = layout.controls.tryGet("ticketsPrice");
                        ticketsPriceControl.params.value = null;
                        return [2 /*return*/];
                }
            });
        });
    }
    function numberOfDays_DataChanged(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, dateFromControl, dateToControl, numberOfDaysToControl, dateFrom, dateTo, time, directoryCityControl, sumOfBusinessTripsMoneyControl, cityInfoService, model;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        dateFromControl = layout.controls.tryGet("dateFrom");
                        dateToControl = layout.controls.tryGet("dateTo");
                        numberOfDaysToControl = layout.controls.tryGet("numberOfDays");
                        if (!dateFromControl || !dateToControl || !numberOfDaysToControl) {
                            return [2 /*return*/];
                        }
                        if (dateFromControl.params.value != null && dateToControl.params.value != null) {
                            dateFrom = dateFromControl.params.value;
                            dateTo = dateToControl.params.value;
                            if (dateFrom <= dateTo) {
                                time = ((dateTo.valueOf() - dateFrom.valueOf()) / (1000 * 3600 * 24)) + 1;
                                numberOfDaysToControl.params.value = time;
                            }
                            else {
                                numberOfDaysToControl.params.value = null;
                            }
                        }
                        else {
                            numberOfDaysToControl.params.value = null;
                        }
                        directoryCityControl = layout.controls.tryGet("directoryCity");
                        sumOfBusinessTripsMoneyControl = layout.controls.tryGet("sumOfBusinessTripsMoney");
                        if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) {
                            return [2 /*return*/];
                        }
                        if (!(directoryCityControl.hasValue() && numberOfDaysToControl != null)) return [3 /*break*/, 2];
                        cityInfoService = layout.getService($CityInfoDataController);
                        return [4 /*yield*/, cityInfoService.GetCityInfo(directoryCityControl.params.value.id)];
                    case 1:
                        model = _a.sent();
                        if (model) {
                            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
                        }
                        else {
                            sumOfBusinessTripsMoneyControl.params.value = null;
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        sumOfBusinessTripsMoneyControl.params.value = null;
                        _a.label = 3;
                    case 3: return [2 /*return*/];
                }
            });
        });
    }
    function shortInfoButton_clik(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, numeratorControl, dateOfCreationControl, dateFromControl, datedateToControl, reasonForBusinessTripsToControl, message;
            return tslib.__generator(this, function (_a) {
                layout = sender.layout;
                numeratorControl = layout.controls.tryGet("number");
                dateOfCreationControl = layout.controls.tryGet("dateOfCreation");
                dateFromControl = layout.controls.tryGet("dateFrom");
                datedateToControl = layout.controls.tryGet("dateTo");
                reasonForBusinessTripsToControl = layout.controls.tryGet("reasonForBusinessTrips");
                if (!numeratorControl || !dateOfCreationControl || !dateFromControl || !datedateToControl || !reasonForBusinessTripsToControl) {
                    return [2 /*return*/];
                }
                message = "Номер заявки: {0}\nДата создания: {1}\nДаты командировки С: {2}\nпо: {3}\nОснование для поездки: \n{4}"
                    .format((numeratorControl.hasValue() ? numeratorControl.params.value.number : "не задан"), (dateOfCreationControl.hasValue() ? dateOfCreationControl.params.value.toLocaleDateString() : "не задана"), (dateFromControl.hasValue() ? dateFromControl.params.value.toLocaleDateString() : "не задана"), (datedateToControl.hasValue() ? datedateToControl.params.value.toLocaleDateString() : "не задана"), (reasonForBusinessTripsToControl.hasValue() ? reasonForBusinessTripsToControl.params.value.toString() : "не задано"));
                MessageBox.MessageBox.ShowInfo(message);
                return [2 /*return*/];
            });
        });
    }
    function onCardSaving(sender, args) {
        /* --1.4. В разметке на «редактирование»: перед сохранением карточки проверить,
        что заполнен элемент «Номер заявки» и «Название», если он пустой,
        выдавать предупреждение и отменять сохранение. */
        var layout = sender.layout;
        var nameControl = layout.controls.tryGet("name");
        var numberControl = layout.controls.tryGet("number");
        if (!nameControl || !numberControl) {
            return;
        }
        if (nameControl.params.value == null || numberControl.params.value == null) {
            if (nameControl.params.value != null && numberControl.params.value == null) {
                MessageBox.MessageBox.ShowError("Заполните поле \"Номер заявки\".");
            }
            else if (nameControl.params.value == null && numberControl.params.value != null) {
                MessageBox.MessageBox.ShowError("Заполните поле \"Название\".");
            }
            else {
                MessageBox.MessageBox.ShowError("Заполните поля \"Название\" и \"Номер заявки\".");
            }
            args.cancel();
        }
        /* 1.4. В разметке на «редактирование»: перед сохранением карточки проверить,
        что заполнен элемент «Номер заявки» и «Название», если он пустой,
        выдавать предупреждение и отменять сохранение.-- */
    }
    function seconded_EmployeeChanged(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, supervisorControl, telephoneControl, customEmployeeService, model, modelCEO;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        supervisorControl = layout.controls.tryGet("supervisor");
                        telephoneControl = layout.controls.tryGet("telephone");
                        if (!supervisorControl || !telephoneControl) {
                            return [2 /*return*/];
                        }
                        if (!sender.hasValue()) return [3 /*break*/, 5];
                        customEmployeeService = layout.getService($CustomEmployeeDataController);
                        return [4 /*yield*/, customEmployeeService.GetEmployeeData(sender.params.value.id)];
                    case 1:
                        model = _a.sent();
                        if (!model) return [3 /*break*/, 4];
                        telephoneControl.params.value = model.phone;
                        if (!(sender.params.value.id != model.manager.id)) return [3 /*break*/, 2];
                        supervisorControl.params.value = model.manager;
                        return [3 /*break*/, 4];
                    case 2: return [4 /*yield*/, customEmployeeService.GetEmployeeData("57158509-b8c3-467c-a4b9-69727ccfe5cd")];
                    case 3:
                        modelCEO = _a.sent();
                        supervisorControl.params.value = modelCEO.manager;
                        _a.label = 4;
                    case 4: return [3 /*break*/, 6];
                    case 5:
                        telephoneControl.params.value = null;
                        supervisorControl.params.value = null;
                        _a.label = 6;
                    case 6: return [2 /*return*/];
                }
            });
        });
    }
    function afterOpeningTheCard(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, whoDrawsUpControl, service, model, dateFromControl, dateToControl, numberOfDaysToControl, dateFrom, dateTo, time;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        whoDrawsUpControl = layout.controls.tryGet("multipleEmployeesWhoDrawsUp");
                        if (!whoDrawsUpControl) {
                            return [2 /*return*/];
                        }
                        service = sender.layout.getService($GroupEmployeesDataController);
                        return [4 /*yield*/, service.GetGroupEmployees("Секретарь")];
                    case 1:
                        model = _a.sent();
                        whoDrawsUpControl.params.value = model.employees;
                        dateFromControl = layout.controls.tryGet("dateFrom");
                        dateToControl = layout.controls.tryGet("dateTo");
                        numberOfDaysToControl = layout.controls.tryGet("numberOfDays");
                        if (!dateFromControl || !dateToControl || !numberOfDaysToControl) {
                            return [2 /*return*/];
                        }
                        if (dateFromControl.params.value != null && dateToControl.params.value != null) {
                            dateFrom = dateFromControl.params.value;
                            dateTo = dateToControl.params.value;
                            if (dateFrom <= dateTo) {
                                time = ((dateTo.valueOf() - dateFrom.valueOf()) / (1000 * 3600 * 24)) + 1;
                                numberOfDaysToControl.params.value = time;
                            }
                        }
                        return [2 /*return*/];
                }
            });
        });
    }
    function directoryCitySelection(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, directoryCityControl, numberOfDaysToControl, sumOfBusinessTripsMoneyControl, cityInfoService, model, ticketsPriceControl;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        directoryCityControl = layout.controls.tryGet("directoryCity");
                        numberOfDaysToControl = layout.controls.tryGet("numberOfDays");
                        sumOfBusinessTripsMoneyControl = layout.controls.tryGet("sumOfBusinessTripsMoney");
                        if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) {
                            return [2 /*return*/];
                        }
                        if (!(sender.hasValue() && numberOfDaysToControl != null)) return [3 /*break*/, 2];
                        cityInfoService = layout.getService($CityInfoDataController);
                        return [4 /*yield*/, cityInfoService.GetCityInfo(sender.params.value.id)];
                    case 1:
                        model = _a.sent();
                        if (model) {
                            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        sumOfBusinessTripsMoneyControl.params.value = null;
                        _a.label = 3;
                    case 3:
                        ticketsPriceControl = layout.controls.tryGet("ticketsPrice");
                        ticketsPriceControl.params.value = null;
                        return [2 /*return*/];
                }
            });
        });
    }
    function sumOfBusinessTripsMoneyControlDataChanged(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, directoryCityControl, numberOfDaysToControl, sumOfBusinessTripsMoneyControl, cityInfoService, model;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        directoryCityControl = layout.controls.tryGet("directoryCity");
                        numberOfDaysToControl = layout.controls.tryGet("numberOfDays");
                        sumOfBusinessTripsMoneyControl = layout.controls.tryGet("sumOfBusinessTripsMoney");
                        if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) {
                            return [2 /*return*/];
                        }
                        if (!(numberOfDaysToControl.params.value != null && directoryCityControl != null)) return [3 /*break*/, 2];
                        cityInfoService = layout.getService($CityInfoDataController);
                        return [4 /*yield*/, cityInfoService.GetCityInfo(directoryCityControl.params.value.id)];
                    case 1:
                        model = _a.sent();
                        if (model) {
                            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        sumOfBusinessTripsMoneyControl.params.value = null;
                        _a.label = 3;
                    case 3: return [2 /*return*/];
                }
            });
        });
    }
    /* --2.4. В разметке на «чтение»: добавить кнопку на форму карточки,
    переводящую карточку в состояние «На согласование» и доступна только в состоянии «Проект». */
    function buttonSendForAgreement_clik(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, sendForAgreementService, model, cancelService;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        sendForAgreementService = layout.getService($SendForAgreementDataController);
                        return [4 /*yield*/, sendForAgreementService.GetSendForAgreement(layout.cardInfo.id)];
                    case 1:
                        model = _a.sent();
                        MessageBox.MessageBox.ShowInfo(model.message);
                        cancelService = layout.getService($Router.$RouterNavigation);
                        cancelService.back();
                        return [2 /*return*/];
                }
            });
        });
    }
    /* 2.4. В разметке на «чтение»: добавить кнопку на форму карточки,
    переводящую карточку в состояние «На согласование» и доступна только в состоянии «Проект».-- */
    /* 2.5. В разметке на «редактирование»: добавить кнопку «Запросить стоимость билетов». */
    //let ticketsControl = layout.controls.tryGet<Dropdown>("tickets"); 
    function requestTicketPricesButton_clik(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, directoryCityControl, dateFromControl, dateToControl, ticketsPriceControl, ticketsControl, cityInfoService, cityInfoModel, destination, dateFrom, dateTo, ticketsSearchService, ticketsSearchModel;
            return tslib.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        layout = sender.layout;
                        directoryCityControl = layout.controls.tryGet("directoryCity");
                        dateFromControl = layout.controls.tryGet("dateFrom");
                        dateToControl = layout.controls.tryGet("dateTo");
                        ticketsPriceControl = layout.controls.tryGet("ticketsPrice");
                        ticketsControl = layout.controls.tryGet("tickets");
                        if (!directoryCityControl || !dateFromControl || !dateToControl || !ticketsPriceControl || !ticketsControl) {
                            return [2 /*return*/];
                        }
                        if (!(ticketsControl.valueCode == 0)) return [3 /*break*/, 6];
                        if (!(dateFromControl.params.value != null && dateToControl.params.value != null && ticketsControl.params.value != null)) return [3 /*break*/, 4];
                        cityInfoService = directoryCityControl.layout.getService($CityInfoDataController);
                        return [4 /*yield*/, cityInfoService.GetCityInfo(directoryCityControl.params.value.id)];
                    case 1:
                        cityInfoModel = _a.sent();
                        if (!cityInfoModel) return [3 /*break*/, 3];
                        destination = cityInfoModel.airportСode;
                        dateFrom = moment__default["default"](dateFromControl.params.value.toString()).format('DD.MM.YYYY');
                        dateTo = moment__default["default"](dateToControl.params.value.toString()).format('DD.MM.YYYY');
                        ticketsSearchService = layout.getService($TicketsSearchDataController);
                        return [4 /*yield*/, ticketsSearchService.GetTicketsSearch(destination, dateFrom, dateTo)];
                    case 2:
                        ticketsSearchModel = _a.sent();
                        if (Number(ticketsSearchModel.ticketsPrice) > 0) {
                            ticketsPriceControl.params.value = Number(ticketsSearchModel.ticketsPrice);
                        }
                        else {
                            ticketsPriceControl.params.value = null;
                            MessageBox.MessageBox.ShowError("Билеты не найдены.");
                        }
                        _a.label = 3;
                    case 3: return [3 /*break*/, 5];
                    case 4:
                        ticketsPriceControl.params.value = null;
                        MessageBox.MessageBox.ShowError("Билеты не найдены.");
                        _a.label = 5;
                    case 5: return [3 /*break*/, 7];
                    case 6:
                        MessageBox.MessageBox.ShowError("Стоимость билетов рассчитывается автоматически только для авиаперелётов.");
                        _a.label = 7;
                    case 7: return [2 /*return*/];
                }
            });
        });
    }
    function ticketsSelection(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, ticketsControl, ticketsPriceControl;
            return tslib.__generator(this, function (_a) {
                layout = sender.layout;
                ticketsControl = layout.controls.tryGet("tickets");
                ticketsPriceControl = layout.controls.tryGet("ticketsPrice");
                if (!ticketsControl || !ticketsPriceControl) {
                    return [2 /*return*/];
                }
                if (ticketsControl.valueCode != 0) {
                    ticketsPriceControl.params.value = null;
                }
                return [2 /*return*/];
            });
        });
    }
    //Отмена редактирования даты создания
    function dateOfCreationControlDataChanged(sender) {
        return tslib.__awaiter(this, void 0, void 0, function () {
            var layout, dateOfCreation, today;
            return tslib.__generator(this, function (_a) {
                layout = sender.layout;
                dateOfCreation = layout.controls.tryGet("dateOfCreation");
                today = new Date();
                dateOfCreation.params.value = today;
                return [2 /*return*/];
            });
        });
    }

    var EventHandlers = /*#__PURE__*/Object.freeze({
        __proto__: null,
        dateFromTo_DataChanged: dateFromTo_DataChanged,
        numberOfDays_DataChanged: numberOfDays_DataChanged,
        shortInfoButton_clik: shortInfoButton_clik,
        onCardSaving: onCardSaving,
        seconded_EmployeeChanged: seconded_EmployeeChanged,
        afterOpeningTheCard: afterOpeningTheCard,
        directoryCitySelection: directoryCitySelection,
        sumOfBusinessTripsMoneyControlDataChanged: sumOfBusinessTripsMoneyControlDataChanged,
        buttonSendForAgreement_clik: buttonSendForAgreement_clik,
        requestTicketPricesButton_clik: requestTicketPricesButton_clik,
        ticketsSelection: ticketsSelection,
        dateOfCreationControlDataChanged: dateOfCreationControlDataChanged
    });

    // Главная входная точка всего расширения
    // Данный файл должен импортировать прямо или косвенно все остальные файлы, 
    // чтобы rollup смог собрать их все в один бандл.
    // Регистрация расширения позволяет корректно установить все
    // обработчики событий, сервисы и прочие сущности web-приложения.
    ExtensionManager.extensionManager.registerExtension({
        name: "Request for Business Trip",
        version: "5.5.16",
        globalEventHandlers: [EventHandlers],
        layoutServices: [
            Service.Service.fromFactory($CustomEmployeeDataController, function (services) { return new CustomEmployeeDataController(services); }),
            Service.Service.fromFactory($CityInfoDataController, function (services) { return new CityInfoDataController(services); }),
            Service.Service.fromFactory($GroupEmployeesDataController, function (services) { return new GroupEmployeesDataController(services); }),
            Service.Service.fromFactory($TicketsSearchDataController, function (services) { return new TicketsSearchDataController(services); }),
            Service.Service.fromFactory($SendForAgreementDataController, function (services) { return new SendForAgreementDataController(services); })
        ]
    });

}));
//# sourceMappingURL=extension.js.map
