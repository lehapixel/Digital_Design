import { DirectoryDesignerRow } from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";
import { Employee } from "@docsvision/webclient/BackOffice/Employee";
import { MultipleEmployees } from "@docsvision/webclient/BackOffice/MultipleEmployees";
import { Numerator } from "@docsvision/webclient/BackOffice/Numerator";
import { MessageBox } from "@docsvision/webclient/Helpers/MessageBox/MessageBox";
import { CustomButton } from "@docsvision/webclient/Platform/CustomButton";
import { DateTimePicker } from "@docsvision/webclient/Platform/DateTimePicker";
import { Dropdown } from "@docsvision/webclient/Platform/Dropdown";
import { NumberControl } from "@docsvision/webclient/Platform/Number";
import { TextArea } from "@docsvision/webclient/Platform/TextArea";
import { TextBox } from "@docsvision/webclient/Platform/TextBox";
import { $RouterNavigation, IS_BACK_PARAMETER } from "@docsvision/webclient/System/$Router";
import { CancelableEventArgs } from "@docsvision/webclient/System/CancelableEventArgs";
import { ICardSavingEventArgs } from "@docsvision/webclient/System/ICardSavingEventArgs";
import { Layout } from "@docsvision/webclient/System/Layout";
import moment from "moment";
import { $CityInfoDataController } from "./Controllers/CityInfoDataController";
import { $CustomEmployeeDataController } from "./Controllers/CustomEmployeeDataController";
import { $GroupEmployeesDataController } from "./Controllers/GroupEmployeesDataController";
import { $SendForAgreementDataController } from "./Controllers/SendForAgreementDataController";
import { $TicketsSearchDataController } from "./Controllers/TicketsSearchDataController";

export async function dateFromTo_DataChanged (sender: DateTimePicker) {
    /* --1.2.	В разметке на «редактирование»: при изменении контролов «Даты командировки С:» или «по:» и, 
    если заполнены оба поля необходимо рассчитать кол-во дней в командировке и записать в поле «Кол-во дней в командировке». */

    let layout = sender.layout;
    let dateFromControl = layout.controls.tryGet<DateTimePicker>("dateFrom");
    let dateToControl = layout.controls.tryGet<DateTimePicker>("dateTo");
    let numberOfDaysToControl = layout.controls.tryGet<NumberControl>("numberOfDays");
    if (!dateFromControl || !dateToControl || !numberOfDaysToControl) { return;}
    if (dateFromControl.params.value != null && dateToControl.params.value != null) {
        var dateFrom = dateFromControl.params.value;
        var dateTo  = dateToControl.params.value;

        if (dateFrom <= dateTo) {
            var time = ((dateTo.valueOf() - dateFrom.valueOf()) / (1000 * 3600 * 24)) + 1;
            numberOfDaysToControl.params.value = time;
        }
        else {
            numberOfDaysToControl.params.value = null;
        }
    }
    else {
        numberOfDaysToControl.params.value = null;
    }
      
    /* 1.2.	В разметке на «редактирование»: при изменении контролов «Даты командировки С:» или «по:» и, 
    если заполнены оба поля необходимо рассчитать кол-во дней в командировке и записать в поле «Кол-во дней в командировке».-- 
    Так же есть обработка исключений в 2.2.*/
    
    /* --2.3 */
    let directoryCityControl = layout.controls.tryGet<DirectoryDesignerRow>("directoryCity");
    let sumOfBusinessTripsMoneyControl = layout.controls.tryGet<NumberControl>("sumOfBusinessTripsMoney");
    
    if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) { return; }
    if (directoryCityControl.hasValue() && numberOfDaysToControl != null) {
        let cityInfoService = layout.getService($CityInfoDataController);
        let model = await cityInfoService.GetCityInfo(directoryCityControl.params.value.id);
        if (model) {
            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
        }
        else {
            sumOfBusinessTripsMoneyControl.params.value = null;
        }
    }
    else {
        sumOfBusinessTripsMoneyControl.params.value = null;
    }
    /* 2.3-- */

    // --2.5
    let ticketsPriceControl = layout.controls.tryGet<NumberControl>("ticketsPrice");
    ticketsPriceControl.params.value = null;
    // 2.5--
}

export async function numberOfDays_DataChanged (sender: NumberControl) {
    /* --1.2 */
    let layout = sender.layout;
    let dateFromControl = layout.controls.tryGet<DateTimePicker>("dateFrom");
    let dateToControl = layout.controls.tryGet<DateTimePicker>("dateTo");
    let numberOfDaysToControl = layout.controls.tryGet<NumberControl>("numberOfDays");
    if (!dateFromControl || !dateToControl || !numberOfDaysToControl) { return;}

    if (dateFromControl.params.value != null && dateToControl.params.value != null) {
        var dateFrom = dateFromControl.params.value;
        var dateTo  = dateToControl.params.value;

        if (dateFrom <= dateTo) {
            var time = ((dateTo.valueOf() - dateFrom.valueOf()) / (1000 * 3600 * 24)) + 1;
            numberOfDaysToControl.params.value = time;
        }
        else {
            numberOfDaysToControl.params.value = null;
        }
    }
    else {
        numberOfDaysToControl.params.value = null;
    }
    /* 1.2-- */
    /* --2.3 */
    let directoryCityControl = layout.controls.tryGet<DirectoryDesignerRow>("directoryCity");
    let sumOfBusinessTripsMoneyControl = layout.controls.tryGet<NumberControl>("sumOfBusinessTripsMoney");
    
    if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) { return; }
    if (directoryCityControl.hasValue() && numberOfDaysToControl != null) {
        let cityInfoService = layout.getService($CityInfoDataController);
        let model = await cityInfoService.GetCityInfo(directoryCityControl.params.value.id);
        if (model) {
            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
        }
        else {
            sumOfBusinessTripsMoneyControl.params.value = null;
        }
    }
    else {
        sumOfBusinessTripsMoneyControl.params.value = null;
    }
    /* 2.3-- */    
}


export async function shortInfoButton_clik(sender: CustomButton) {
    /* --1.3. В разметке на «чтение»: добавить на ленту кнопку, по нажатию на кнопку выводить сообщение (MessageBox.ShowInfo) 
    с краткой информацией по заявке: «Номер заявки», «Дата создания», «Даты командировки С:», «по:», «Основание для поездки». */

    let layout = sender.layout;
    let numeratorControl = layout.controls.tryGet<Numerator>("number");
    let dateOfCreationControl = layout.controls.tryGet<DateTimePicker>("dateOfCreation");
    let dateFromControl = layout.controls.tryGet<DateTimePicker>("dateFrom");
    let datedateToControl = layout.controls.tryGet<DateTimePicker>("dateTo");
    let reasonForBusinessTripsToControl = layout.controls.tryGet<TextArea>("reasonForBusinessTrips");

    if (!numeratorControl || !dateOfCreationControl || !dateFromControl || !datedateToControl || !reasonForBusinessTripsToControl) { return; }
    let message = "Номер заявки: {0}\nДата создания: {1}\nДаты командировки С: {2}\nпо: {3}\nОснование для поездки: \n{4}"
        .format(
            (numeratorControl.hasValue() ? numeratorControl.params.value.number : "не задан"),
            (dateOfCreationControl.hasValue() ? dateOfCreationControl.params.value.toLocaleDateString() : "не задана"),
            (dateFromControl.hasValue() ? dateFromControl.params.value.toLocaleDateString() : "не задана"),
            (datedateToControl.hasValue() ? datedateToControl.params.value.toLocaleDateString() : "не задана"),
            (reasonForBusinessTripsToControl.hasValue() ? reasonForBusinessTripsToControl.params.value.toString() : "не задано"),
    );
    MessageBox.ShowInfo(message);
    
    /* 1.3. В разметке на «чтение»: добавить на ленту кнопку, по нажатию на кнопку выводить сообщение (MessageBox.ShowInfo) 
    с краткой информацией по заявке: «Номер заявки», «Дата создания», «Даты командировки С:», «по:», «Основание для поездки».-- */
}

export function onCardSaving(sender: Layout, args: CancelableEventArgs<ICardSavingEventArgs>) {
    /* --1.4. В разметке на «редактирование»: перед сохранением карточки проверить, 
    что заполнен элемент «Номер заявки» и «Название», если он пустой, 
    выдавать предупреждение и отменять сохранение. */

    let layout = sender.layout;
    let nameControl = layout.controls.tryGet<TextBox>("name");
    let numberControl = layout.controls.tryGet<Numerator>("number");
    if (!nameControl || !numberControl) { return; }

    if (nameControl.params.value == null || numberControl.params.value == null) {
        if (nameControl.params.value != null && numberControl.params.value == null) {
            MessageBox.ShowError("Заполните поле \"Номер заявки\".");
        }
        else if (nameControl.params.value == null && numberControl.params.value != null) {
            MessageBox.ShowError("Заполните поле \"Название\".");
        }
        else {
            MessageBox.ShowError("Заполните поля \"Название\" и \"Номер заявки\".");
        }
        args.cancel();  
    }

    /* 1.4. В разметке на «редактирование»: перед сохранением карточки проверить, 
    что заполнен элемент «Номер заявки» и «Название», если он пустой, 
    выдавать предупреждение и отменять сохранение.-- */
}
export async function seconded_EmployeeChanged(sender: Employee) {
    /* --2.1. В разметке на «редактирование»: при изменении поля «Командируемый», 
    поля «Руководитель» и «Телефон» необходимо заполнить данными из сотрудника, выбранного в поле. */

    let layout = sender.layout;
    let supervisorControl = layout.controls.tryGet<Employee>("supervisor");
    let telephoneControl = layout.controls.tryGet<TextBox>("telephone");
    if (!supervisorControl || !telephoneControl) { return; }
    if (sender.hasValue()) {
        let customEmployeeService = layout.getService($CustomEmployeeDataController);
        let model = await customEmployeeService.GetEmployeeData(sender.params.value.id);
        if (model) {
            telephoneControl.params.value = model.phone;
            if (sender.params.value.id != model.manager.id) {
                supervisorControl.params.value = model.manager;
            }
            else {
                let modelCEO = await customEmployeeService.GetEmployeeData("57158509-b8c3-467c-a4b9-69727ccfe5cd"); // Guid генерального директора
                supervisorControl.params.value = modelCEO.manager;
            }
        }
    }
    else {
        telephoneControl.params.value = null;
        supervisorControl.params.value = null; 
    }

    /* 2.1. В разметке на «редактирование»: при изменении поля «Командируемый», 
    поля «Руководитель» и «Телефон» необходимо заполнить данными из сотрудника, выбранного в поле.-- */
}

export async function afterOpeningTheCard(sender: Layout) {
    /* --2.2. В разметке на «редактирование»: при первом открытии карточки в поле «Кто оформляет» 
    должны вписываться сотрудники из группы справочника сотрудников - «Секретарь». */

    let layout = sender.layout;

    let whoDrawsUpControl = layout.controls.tryGet<MultipleEmployees>("multipleEmployeesWhoDrawsUp");
    if (!whoDrawsUpControl) { return; }
    let service = sender.layout.getService($GroupEmployeesDataController);
    let model = await service.GetGroupEmployees("Секретарь");
    whoDrawsUpControl.params.value = model.employees;

    /* 2.2. В разметке на «редактирование»: при первом открытии карточки в поле «Кто оформляет» 
    должны вписываться сотрудники из группы справочника сотрудников - «Секретарь».-- */

    /* --1.2 */
    let dateFromControl = layout.controls.tryGet<DateTimePicker>("dateFrom");
    let dateToControl = layout.controls.tryGet<DateTimePicker>("dateTo");
    let numberOfDaysToControl = layout.controls.tryGet<NumberControl>("numberOfDays");
    if (!dateFromControl || !dateToControl || !numberOfDaysToControl) { return; }
    if (dateFromControl.params.value != null && dateToControl.params.value != null) {
        var dateFrom = dateFromControl.params.value;
        var dateTo  = dateToControl.params.value;

        if (dateFrom <= dateTo) {
            var time = ((dateTo.valueOf() - dateFrom.valueOf()) / (1000 * 3600 * 24)) + 1;
            numberOfDaysToControl.params.value = time;
        }
    }
    /* 1.2-- */    
}

export async function directoryCitySelection(sender: DirectoryDesignerRow) {
    /* 2.3. В разметке на «редактирование»: при выборе значения в поле «Город», 
    необходимо получить значение из этого элемента справочника (мы его создавали ранее, поле «Суточные») 
    и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: «Суточные» * значение в поле «Кол-во дней в командировке».*/    
    
    let layout = sender.layout;
    let directoryCityControl = layout.controls.tryGet<DirectoryDesignerRow>("directoryCity");
    let numberOfDaysToControl = layout.controls.tryGet<NumberControl>("numberOfDays");
    let sumOfBusinessTripsMoneyControl = layout.controls.tryGet<NumberControl>("sumOfBusinessTripsMoney");

    if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) { return; }
    if (sender.hasValue() && numberOfDaysToControl != null) {
        let cityInfoService = layout.getService($CityInfoDataController);
        let model = await cityInfoService.GetCityInfo(sender.params.value.id);
        if (model) {
            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
        }
    }
    else {
        sumOfBusinessTripsMoneyControl.params.value = null;
    }
    /* 2.3. В разметке на «редактирование»: при выборе значения в поле «Город», 
    необходимо получить значение из этого элемента справочника (мы его создавали ранее, поле «Суточные») 
    и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: «Суточные» * значение в поле «Кол-во дней в командировке».--
    Так же есть обработка исключений в 1.2. */   
    
    
    // --2.5
    let ticketsPriceControl = layout.controls.tryGet<NumberControl>("ticketsPrice");
    ticketsPriceControl.params.value = null;
    // 2.5--
}

export async function sumOfBusinessTripsMoneyControlDataChanged(sender: NumberControl) {
    /* 2.3. В разметке на «редактирование»: при выборе значения в поле «Город», 
    необходимо получить значение из этого элемента справочника (мы его создавали ранее, поле «Суточные») 
    и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: «Суточные» * значение в поле «Кол-во дней в командировке».*/    
    
    let layout = sender.layout;
    let directoryCityControl = layout.controls.tryGet<DirectoryDesignerRow>("directoryCity");
    let numberOfDaysToControl = layout.controls.tryGet<NumberControl>("numberOfDays");
    let sumOfBusinessTripsMoneyControl = layout.controls.tryGet<NumberControl>("sumOfBusinessTripsMoney");
    if (!directoryCityControl || !numberOfDaysToControl || !sumOfBusinessTripsMoneyControl) { return; }

    if (numberOfDaysToControl.params.value != null && directoryCityControl != null) {
        let cityInfoService = layout.getService($CityInfoDataController);
        let model = await cityInfoService.GetCityInfo(directoryCityControl.params.value.id);
        if (model) {
            sumOfBusinessTripsMoneyControl.params.value = model.payPerDay * numberOfDaysToControl.params.value;
        }
    }
    else {
        sumOfBusinessTripsMoneyControl.params.value = null;
    }
    
    /* 2.3. В разметке на «редактирование»: при выборе значения в поле «Город», 
    необходимо получить значение из этого элемента справочника (мы его создавали ранее, поле «Суточные») 
    и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: «Суточные» * значение в поле «Кол-во дней в командировке».-- */   
}

/* --2.4. В разметке на «чтение»: добавить кнопку на форму карточки, 
переводящую карточку в состояние «На согласование» и доступна только в состоянии «Проект». */
export async function buttonSendForAgreement_clik(sender: CustomButton) {
    let layout = sender.layout;
    let sendForAgreementService = layout.getService($SendForAgreementDataController);
    let model = await sendForAgreementService.GetSendForAgreement(layout.cardInfo.id);
    
    MessageBox.ShowInfo(model.message);
    
    let cancelService = layout.getService($RouterNavigation);
    cancelService.back();
}
/* 2.4. В разметке на «чтение»: добавить кнопку на форму карточки, 
переводящую карточку в состояние «На согласование» и доступна только в состоянии «Проект».-- */

/* 2.5. В разметке на «редактирование»: добавить кнопку «Запросить стоимость билетов». */
//let ticketsControl = layout.controls.tryGet<Dropdown>("tickets"); 

export async function requestTicketPricesButton_clik(sender: CustomButton) {
    let layout = sender.layout;
    let directoryCityControl = layout.controls.tryGet<DirectoryDesignerRow>("directoryCity");
    let dateFromControl = layout.controls.tryGet<DateTimePicker>("dateFrom");
    let dateToControl = layout.controls.tryGet<DateTimePicker>("dateTo");
    let ticketsPriceControl = layout.controls.tryGet<NumberControl>("ticketsPrice");
    let ticketsControl = layout.controls.tryGet<Dropdown>("tickets");
    if (!directoryCityControl || !dateFromControl || !dateToControl || !ticketsPriceControl || !ticketsControl) { return; }
    
    if (ticketsControl.valueCode == 0) {
        if (dateFromControl.params.value != null && dateToControl.params.value != null && ticketsControl.params.value != null) {
            let cityInfoService = directoryCityControl.layout.getService($CityInfoDataController);
            let cityInfoModel = await cityInfoService.GetCityInfo(directoryCityControl.params.value.id);
            if (cityInfoModel) {
                var destination = cityInfoModel.airportСode;
                var dateFrom = moment(dateFromControl.params.value.toString()).format('DD.MM.YYYY');
                var dateTo = moment(dateToControl.params.value.toString()).format('DD.MM.YYYY');
                
                let ticketsSearchService = layout.getService($TicketsSearchDataController);
                let ticketsSearchModel = await ticketsSearchService.GetTicketsSearch(destination, dateFrom, dateTo);
                if (Number(ticketsSearchModel.ticketsPrice) > 0) {
                    ticketsPriceControl.params.value = Number(ticketsSearchModel.ticketsPrice); 
                } 
                else {
                    ticketsPriceControl.params.value = null;
                    MessageBox.ShowError("Билеты не найдены.");
                }      
            }
        }
        else {
            ticketsPriceControl.params.value = null;
            MessageBox.ShowError("Билеты не найдены.");
        }
    }
    else {
        MessageBox.ShowError("Стоимость билетов рассчитывается автоматически только для авиаперелётов.");
    }
}

export async function ticketsSelection(sender: Dropdown) {
    // --2.5
    let layout = sender.layout;
    let ticketsControl = layout.controls.tryGet<Dropdown>("tickets");
    let ticketsPriceControl = layout.controls.tryGet<NumberControl>("ticketsPrice");
    if (!ticketsControl || !ticketsPriceControl) { return; }

    if (ticketsControl.valueCode != 0) {
        ticketsPriceControl.params.value = null;
    }
    // 2.5--
}

//Отмена редактирования даты создания
export async function dateOfCreationControlDataChanged(sender: DateTimePicker) {
    let layout = sender.layout;
    let dateOfCreation = layout.controls.tryGet<DateTimePicker>("dateOfCreation");
    var today = new Date();
    dateOfCreation.params.value = today;
}