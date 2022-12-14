using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.ObjectModel.Services;
using DocsVision.BackOffice.WinForms;
using DocsVision.BackOffice.WinForms.Controls;
using DocsVision.BackOffice.WinForms.Design.LayoutItems;
using DocsVision.BackOffice.WinForms.Design.PropertyControls;
using DocsVision.Platform.CardHost;
using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.ObjectModel;
using DocsVision.Platform.ObjectModel.Search;

namespace BackOffice {
    public class CardDocumentЗаявка_на_командировкуScript : CardDocumentScript {
    
    #region Properties
	
	protected ICustomizableControl CustomizableControl {
		get {
			return this.CardControl as ICustomizableControl;
		}
	}		
	
	protected ObjectContext ObjContext {
		get{
			return CardControl.ObjectContext;
		}
	}
		
    #endregion

    #region Methods

    #endregion

    #region Event Handlers
	
	private void DateFrom_ControlValueChanged(System.Object sender, System.EventArgs e) {
		/* --1.1. При изменении контролов «Даты командировки С:» или «по:» и, если заполнены оба поля необходимо рассчитать кол-во дней в командировке 
		и записать в поле «Кол-во дней в командировке». */
		
		ICustomPropertyItem controlDateFrom = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateFrom");
		ICustomPropertyItem controlDateTo = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateTo");
		ILayoutPropertyItem controlNumberOfDays = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("NumberOfDays");
		ILayoutPropertyItem controlCity = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("City"); //2.3
		ILayoutPropertyItem controlSumOfBusinessTripsMoney = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("SumOfBusinessTripsMoney"); //2.3
		
		if (controlDateFrom == null || controlDateTo == null || controlNumberOfDays == null) {return;}
		
		if (controlDateTo.PropertyControl.ControlValue != null && controlDateFrom.PropertyControl.ControlValue != null) { 
			var dateFrom = DateTime.Parse(controlDateFrom.Control.Text);
	        var dateTo = DateTime.Parse(controlDateTo.Control.Text);
			
			if (dateFrom <= dateTo) {
				TimeSpan time = dateTo - dateFrom;
				
				controlNumberOfDays.ControlValue = time.Days + 1;
				controlNumberOfDays.Commit();
			}
			else {
				controlNumberOfDays.ControlValue = null;
				controlNumberOfDays.Commit();
			}
			
			Guid cityId = (Guid)controlCity.ControlValue; //2.3
			BaseUniversalItem cityItem = ObjContext.GetObject<BaseUniversalItem>(cityId); //2.3

			if (cityItem != null && controlNumberOfDays.ControlValue != null) { //2.3					
				decimal numberOfDays = Convert.ToDecimal(controlNumberOfDays.ControlValue); //2.3
				decimal payPerDay = Convert.ToDecimal(cityItem.ItemCard.MainInfo["PayPerDay"]); //2.3
				controlSumOfBusinessTripsMoney.ControlValue = payPerDay * numberOfDays; //2.3
				controlSumOfBusinessTripsMoney.Commit(); //2.3
			}
		}
		else {
			controlNumberOfDays.ControlValue = null;
			controlNumberOfDays.Commit();
			controlSumOfBusinessTripsMoney.ControlValue = null; //2.3
			controlSumOfBusinessTripsMoney.Commit(); //2.3
		}
		
		/* 1.1. При изменении контролов «Даты командировки С:» или «по:» и, если заполнены оба поля необходимо рассчитать кол-во дней в командировке 
		и записать в поле «Кол-во дней в командировке».-- */
		
		/* --Обработка исключения для 3-го задания */
		ILayoutPropertyItem controlTicketsPrice = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("TicketsPrice"); //Исключение для 3-го задания
		if (controlTicketsPrice == null) {return;} 
		
		controlTicketsPrice.ControlValue = null;
		controlTicketsPrice.Commit();
		
		/* Обработка исключения для 3-го задания-- */
    }
    
	private void DateTo_ControlValueChanged(System.Object sender, System.EventArgs e) {
		/* --1.1. При изменении контролов «Даты командировки С:» или «по:» и, если заполнены оба поля необходимо рассчитать кол-во дней в командировке 
		и записать в поле «Кол-во дней в командировке». */
		
        ICustomPropertyItem controlDateFrom = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateFrom");
		ICustomPropertyItem controlDateTo = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateTo");
		ILayoutPropertyItem controlNumberOfDays = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("NumberOfDays");
		ILayoutPropertyItem controlCity = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("City"); //2.3
		ILayoutPropertyItem controlSumOfBusinessTripsMoney = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("SumOfBusinessTripsMoney"); //2.3
		
		if (controlDateFrom == null || controlDateTo == null || controlNumberOfDays == null) {return;}
		if (controlDateTo.PropertyControl.ControlValue != null && controlDateFrom.PropertyControl.ControlValue != null) { 
			var dateFrom = DateTime.Parse(controlDateFrom.Control.Text);
	        var dateTo = DateTime.Parse(controlDateTo.Control.Text);
			
			if (dateFrom <= dateTo) {
				TimeSpan time = dateTo - dateFrom;
			
				controlNumberOfDays.ControlValue = time.Days + 1;
				controlNumberOfDays.Commit();			
			}
			else {
				controlNumberOfDays.ControlValue = null;
				controlNumberOfDays.Commit();
			}
			Guid cityId = (Guid)controlCity.ControlValue; //2.3
			BaseUniversalItem cityItem = ObjContext.GetObject<BaseUniversalItem>(cityId); //2.3

			if (cityItem != null && controlNumberOfDays.ControlValue != null) { //2.3					
				decimal numberOfDays = Convert.ToDecimal(controlNumberOfDays.ControlValue); //2.3
				decimal payPerDay = Convert.ToDecimal(cityItem.ItemCard.MainInfo["PayPerDay"]); //2.3
				controlSumOfBusinessTripsMoney.ControlValue = payPerDay * numberOfDays; //2.3
				controlSumOfBusinessTripsMoney.Commit(); //2.3
			}
		}
		else {
			controlNumberOfDays.ControlValue = null;
			controlNumberOfDays.Commit();
			controlSumOfBusinessTripsMoney.ControlValue = null; //2.3
			controlSumOfBusinessTripsMoney.Commit(); //2.3
		}
		
		/* 1.1. При изменении контролов «Даты командировки С:» или «по:» и, если заполнены оба поля необходимо рассчитать кол-во дней в командировке 
		и записать в поле «Кол-во дней в командировке».-- */
		
		/* --Обработка исключения для 3-го задания */
		ILayoutPropertyItem controlTicketsPrice = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("TicketsPrice"); //Исключение для 3-го задания
		if (controlTicketsPrice == null) {return;} 
		
		controlTicketsPrice.ControlValue = null;
		controlTicketsPrice.Commit();
		
		/* Обработка исключения для 3-го задания-- */
    }
	
	private void NumberOfDays_ValueChanged(System.Object sender, System.EventArgs e) {
        /* 1.1. При изменении контролов «Даты командировки С:» или «по:» и, если заполнены оба поля необходимо рассчитать кол-во дней в командировке 
		и записать в поле «Кол-во дней в командировке».-- */
		ICustomPropertyItem controlDateFrom = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateFrom");
		ICustomPropertyItem controlDateTo = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateTo");
		ILayoutPropertyItem controlNumberOfDays = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("NumberOfDays");
		
		if (controlDateFrom == null || controlDateTo == null || controlNumberOfDays == null) {return;}
		
		if (controlDateTo.PropertyControl.ControlValue != null && controlDateFrom.PropertyControl.ControlValue != null) { 
			var dateFrom = DateTime.Parse(controlDateFrom.Control.Text);
	        var dateTo = DateTime.Parse(controlDateTo.Control.Text);
			
			if (dateFrom <= dateTo) {
				TimeSpan time = dateTo - dateFrom;
				
				controlNumberOfDays.ControlValue = time.Days + 1;
				controlNumberOfDays.Commit();
			}
			else {
				controlNumberOfDays.ControlValue = null;
				controlNumberOfDays.Commit();
			}	
		}
		else {
			controlNumberOfDays.ControlValue = null;
			controlNumberOfDays.Commit();
		}
		/* 1.1. При изменении контролов «Даты командировки С:» или «по:» и, если заполнены оба поля необходимо рассчитать кол-во дней в командировке 
		и записать в поле «Кол-во дней в командировке».-- */
    }

    private void shortInfo_ItemClick(System.Object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
		/* --1.2. Добавить на ленту кнопку, по нажатию на кнопку выводить сообщение (MessageBox.Show()) с краткой информацией по заявке: 
		«Номер заявки», «Дата создания», «Даты командировки С:», «по:», «Основание для поездки». */
		
		NumeratorBox controlNumber = CustomizableControl.FindPropertyItem<NumeratorBox>("Number");
		ICustomPropertyItem controlDateOfCreation = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateOfCreation");
		ICustomPropertyItem controlDateFrom = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateFrom");
		ICustomPropertyItem controlDateTo = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateTo");
		ILayoutPropertyItem controlReasonForBusinessTrips = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("ReasonForBusinessTrips");
		
		if (controlNumber == null || controlDateOfCreation == null || controlDateFrom == null || controlDateTo == null || controlReasonForBusinessTrips == null) {return;}
		
		string message = string.Format("Номер заявки: {1}{0}Дата создания: {2}{0}Даты командировки С: {3}{0}по: {4}{0}Основание для поездки: {0}{5}", Environment.NewLine, controlNumber.Text , controlDateOfCreation.Control.Text,
									   controlDateFrom.Control.Text, controlDateTo.Control.Text, controlReasonForBusinessTrips.ControlValue);
		MessageBox.Show(message, "Краткая информация");
		
		/* 1.2. Добавить на ленту кнопку, по нажатию на кнопку выводить сообщение (MessageBox.Show()) с краткой информацией по заявке: 
		«Номер заявки», «Дата создания», «Даты командировки С:», «по:», «Основание для поездки».-- */
    }
	
    private void Заявка_на_командировку_StateChanged(System.Object sender, System.EventArgs e) {
		/* --1.3. После смены состояния карточки вывести сообщение (MessageBox) «Состояние изменено» и подсветить жёлтым цветом контрол «Состояние». */
		
		ICustomPropertyItem controlState = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("State");
		
		if (controlState == null) {return;}
		
		Color collor = controlState.Control.BackColor;
		controlState.Control.BackColor = Color.Yellow;
		
		string message = string.Format("Состояние изменено");
		MessageBox.Show(message, "Сообщение");
		
		controlState.Control.BackColor = collor;
		
		/* 1.3. После смены состояния карточки вывести сообщение (MessageBox) «Состояние изменено» и подсветить жёлтым цветом контрол «Состояние».-- */
    }
	
	/* --1.4. Перед сохранением карточки проверить, что заполнен элемент «Название», если он пустой, выдавать предупреждение и отменять сохранение. */
	// так же добавлена обработка исключения для события CardActivated
	
    private void Заявка_на_командировку_Saving(System.Object sender, System.ComponentModel.CancelEventArgs e) {
		ILayoutPropertyItem controlName = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Name");
		if (controlName == null) {return;}
		
		if (controlName.ControlValue == null) {
			e.Cancel = true;
			string message = string.Format("Заполните поле \"Название\"!");
			MessageBox.Show(message, "Ошибка");
		}
		
		/* --Обработка исключения для дат. */
		ICustomPropertyItem controlDateFrom = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateFrom");
		ICustomPropertyItem controlDateTo = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateTo");
		if(controlDateFrom == null || controlDateTo == null){return;}
		
		if (DateTime.Parse(controlDateFrom.PropertyControl.ControlValue.ToString()) > DateTime.Parse(controlDateTo.PropertyControl.ControlValue.ToString())) {
			e.Cancel = true;
			string message = string.Format("Задан некоректный временной промежуток!");
			MessageBox.Show(message, "Ошибка");
		}
		/* Обработка исключения для дат.-- */
    }
	
	private void Name_TextChanged(System.Object sender, System.EventArgs e) {
        ILayoutPropertyItem controlName = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Name");
		ICustomizableControl cust = this.CardControl;
		IPropertyControl controlNumber = cust.FindPropertyItem<IPropertyControl>("Number");
		
		if (controlName.ControlValue == null) {
			controlNumber.AllowEdit = false;
		}
		else {
			controlNumber.AllowEdit = true;
		}
    }
	
	/* 1.4. Перед сохранением карточки проверить, что заполнен элемент «Название», если он пустой, выдавать предупреждение и отменять сохранение.-- */
	// так же добавлена обработка исключения для события CardActivated
	
    private void Seconded_EmployeeChanged(System.Object sender, System.EventArgs e) {	
		/* --2.1. При изменении поля «Командируемый», поля «Руководитель» и «Телефон» необходимо заполнить данными из сотрудника, выбранного в поле. */
		
		ILayoutPropertyItem controlSupervisor = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Supervisor");
		ILayoutPropertyItem controlTelephone = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Telephone");
		if (controlSupervisor == null || controlTelephone == null) {return;}

		Guid managerId = Guid.Empty;
		string phone = "";
		
		ILayoutPropertyItem controlSeconded = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Seconded");
		if (controlSeconded != null) {
			Guid secondedId = (Guid)controlSeconded.ControlValue;
			
			/*Поиск подразделения генерального директора*/
			IStaffService staffService = ObjContext.GetService<IStaffService>();
			string organizationName = "Микоян";
			StaffUnit parentUnit = staffService.FindCompanyByNameOnServer(null, organizationName);
			string ceoUnitName = "Продажи";
			StaffUnit ceoUnit = staffService.FindCompanyByNameOnServer(parentUnit, ceoUnitName);
			/*Поиск подразделения генерального директора*/
			
			StaffEmployee seconded = ObjContext.GetObject<StaffEmployee>(secondedId);
			if (seconded != null) {
				managerId = seconded.Unit.Manager.GetObjectId();
				if (managerId == secondedId) {
					managerId = ceoUnit.Manager.GetObjectId(); // Guid генерального директора
				}
				phone = seconded.Phone;
			}
		}
		controlSupervisor.ControlValue = managerId;	
		controlSupervisor.Commit();
		controlTelephone.ControlValue = phone;	
		controlTelephone.Commit();
		
		/* 2.1. При изменении поля «Командируемый», поля «Руководитель» и «Телефон» необходимо заполнить данными из сотрудника, выбранного в поле.-- */
    }
	
	private void Заявка_на_командировку_CardActivated(System.Object sender, DocsVision.Platform.WinForms.CardActivatedEventArgs e) {	
		// --2.4
		Guid cardId = CardControl.CardData.Id;
		BaseCard card = ObjContext.GetObject<BaseCard>(cardId);
		StatesState cardState = card.SystemInfo.State;
		
		(this.CardControl as ICustomizableControl).RibbonControl.Items["buttonSendForAgreement"].Enabled = false;
		if (cardState.DefaultName == "Project") {
			(this.CardControl as ICustomizableControl).RibbonControl.Items["buttonSendForAgreement"].Enabled = true;
		}
		// 2.4--
		
		// --1.4
		ILayoutPropertyItem controlName = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Name");
		ICustomizableControl cust = this.CardControl;
		IPropertyControl controlNumber = cust.FindPropertyItem<IPropertyControl>("Number");
		
		if (controlName.ControlValue == null) {
			controlNumber.AllowEdit = false;
		}
		else {
			controlNumber.AllowEdit = true;
		}
		// 1.4--
		
		/* --2.2. При первом открытии карточки в поле «Кто оформляет» должны вписываться сотрудники из группы справочника сотрудников «Секретарь». */
        
		if (!e.ActivateFlags.HasFlag(ActivateFlags.New)) {return;}
		
		ILayoutPropertyItem controlWhoDrawsUp = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("WhoDrawsUp");
		
		string groupName = "Секретарь";
		IStaffService staffService = ObjContext.GetService<IStaffService>();
		StaffGroup groupId = staffService.FindGroupByName(null, groupName);

		controlWhoDrawsUp.ControlValue = groupId.EmployeesIds.ToArray();
		controlWhoDrawsUp.Commit();
		
		/* 2.2. При первом открытии карточки в поле «Кто оформляет» должны вписываться сотрудники из группы справочника сотрудников «Секретарь».-- */
	}
	
    private void City_ValueChanged(System.Object sender, System.EventArgs e) {
		/* --2.3. При выборе значения в поле «Город», необходимо получить значение из этого элемента справочника 
		(мы его создавали ранее, поле «Суточные») и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: 
		«Суточные» * значение в поле «Кол-во дней в командировке». */
		
		// Так же добавлены операции для событий задания 1.1
        ILayoutPropertyItem controlCity = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("City");
		ILayoutPropertyItem controlSumOfBusinessTripsMoney = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("SumOfBusinessTripsMoney");
		ILayoutPropertyItem controlNumberOfDays = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("NumberOfDays");
		if (controlCity == null || controlSumOfBusinessTripsMoney == null || controlNumberOfDays == null) {return;}
		
		Guid cityId = (Guid)controlCity.ControlValue;
		BaseUniversalItem cityItem = ObjContext.GetObject<BaseUniversalItem>(cityId);

		if (cityItem != null && controlNumberOfDays.ControlValue != null) {
			decimal numberOfDays = Convert.ToDecimal(controlNumberOfDays.ControlValue);
			decimal payPerDay = Convert.ToDecimal(cityItem.ItemCard.MainInfo["PayPerDay"]);
			controlSumOfBusinessTripsMoney.ControlValue = payPerDay * numberOfDays;
			controlSumOfBusinessTripsMoney.Commit();
		}
		else {
			controlSumOfBusinessTripsMoney.ControlValue = null;
			controlSumOfBusinessTripsMoney.Commit();	
		}
		/* 2.3. При выборе значения в поле «Город», необходимо получить значение из этого элемента справочника 
		(мы его создавали ранее, поле «Суточные») и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: 
		«Суточные» * значение в поле «Кол-во дней в командировке».-- */
		
		/* --Обработка исключения для 3-го задания */
		ILayoutPropertyItem controlTicketsPrice = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("TicketsPrice"); //Исключение для 3-го задания
		if (controlTicketsPrice == null) {return;} 
		
		controlTicketsPrice.ControlValue = null;
		controlTicketsPrice.Commit();
		/* Обработка исключения для 3-го задания-- */
	
    }
	
	private void SumOfBusinessTripsMoney_ValueChanged(System.Object sender, System.EventArgs e) {
		/* --2.3. При выборе значения в поле «Город», необходимо получить значение из этого элемента справочника 
		(мы его создавали ранее, поле «Суточные») и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: 
		«Суточные» * значение в поле «Кол-во дней в командировке». */
		
        ILayoutPropertyItem controlCity = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("City");
		ILayoutPropertyItem controlSumOfBusinessTripsMoney = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("SumOfBusinessTripsMoney");
		ILayoutPropertyItem controlNumberOfDays = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("NumberOfDays");
		if (controlCity == null || controlSumOfBusinessTripsMoney == null || controlNumberOfDays == null) {return;}
		
		Guid cityId = (Guid)controlCity.ControlValue;
		BaseUniversalItem cityItem = ObjContext.GetObject<BaseUniversalItem>(cityId);

		if (cityItem != null && controlNumberOfDays.ControlValue != null) {
			decimal numberOfDays = Convert.ToDecimal(controlNumberOfDays.ControlValue);
			decimal payPerDay = Convert.ToDecimal(cityItem.ItemCard.MainInfo["PayPerDay"]);
			controlSumOfBusinessTripsMoney.ControlValue = payPerDay * numberOfDays;
			controlSumOfBusinessTripsMoney.Commit();
		}
		else {
			controlSumOfBusinessTripsMoney.ControlValue = null;
			controlSumOfBusinessTripsMoney.Commit();	
		}
		/* 2.3. При выборе значения в поле «Город», необходимо получить значение из этого элемента справочника 
		(мы его создавали ранее, поле «Суточные») и вписать в поле «Сумма командировочных», рассчитав по следующей формуле: 
		«Суточные» * значение в поле «Кол-во дней в командировке».-- */
    }
	
    private void buttonSendForAgreement_ItemClick(System.Object sender, DevExpress.XtraBars.ItemClickEventArgs e) {	
		/* --2.4. На ленте карточки должна быть кнопка, переводящая карточку в состояние «На согласование» и доступна только в состоянии «Проект». */
		
		IList<StatesStateMachineBranch> statesStateMachineBranch = CardControl.ObjectContext.GetService<IStateService>().GetStateMachineBranches(BaseObject.SystemInfo.CardKind);
		StatesStateMachineBranch statesStateMachineBranchLines = statesStateMachineBranch.Where(t => t.StartState == base.BaseObject.SystemInfo.State 
			&& t.BranchType == StatesStateMachineBranchBranchType.Line
			&& t.EndState.DefaultName.Equals("WaitingForAgreement")).FirstOrDefault();
		this.CardControl.ChangeState(statesStateMachineBranchLines);
		
		Guid cardId = CardControl.CardData.Id;
		BaseCard card = ObjContext.GetObject<BaseCard>(cardId);
		StatesState cardState = card.SystemInfo.State;
		
		(this.CardControl as ICustomizableControl).RibbonControl.Items["buttonSendForAgreement"].Enabled = false;
		if (cardState.DefaultName == "Project") {
			(this.CardControl as ICustomizableControl).RibbonControl.Items["buttonSendForAgreement"].Enabled = true;
		}
		
		this.CardControl.Close();
		
		/*2.4. На ленте карточки должна быть кнопка, переводящая карточку в состояние «На согласование» и доступна только в состоянии «Проект».-- */
		// так же добавлена обработка исключения для события CardActivated
    }
	
	/* --Третье задание */
    private void RequestTicketPrices_ItemClick(System.Object sender, DevExpress.XtraBars.ItemClickEventArgs e) {	
		ILayoutPropertyItem controlCity = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("City");
        ICustomPropertyItem controlDateFrom = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateFrom");
		ICustomPropertyItem controlDateTo = CustomizableControl.FindPropertyItem<ICustomPropertyItem>("DateTo");
		ILayoutPropertyItem controlTicketsPrice = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("TicketsPrice");
		ILayoutPropertyItem controlTickets = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Tickets");
		if (controlDateFrom == null || controlDateTo == null || controlCity == null || controlTickets == null) {return;}
		
		Guid cityId = (Guid)controlCity.ControlValue;
		BaseUniversalItem cityItem = ObjContext.GetObject<BaseUniversalItem>(cityId);
		
		if (Convert.ToInt32(controlTickets.ControlValue) == 0 && controlTickets.ControlValue != null) {
			if (controlDateTo.PropertyControl.ControlValue != null && controlDateFrom.PropertyControl.ControlValue != null && cityItem != null) {
					
				string destination = Convert.ToString(cityItem.ItemCard.MainInfo["AirportСode"]);
				
				ExtensionMethod method = Session.ExtensionManager.GetExtensionMethod("TicketsSearch", "MinPriceSearch");
				method.Parameters.AddNew("destination", ParameterValueType.String, destination);
				method.Parameters.AddNew("dateFrom", ParameterValueType.String, controlDateFrom.PropertyControl.ControlValue.ToString());
				method.Parameters.AddNew("dateTo", ParameterValueType.String, controlDateTo.PropertyControl.ControlValue.ToString());
				
				if (Convert.ToDecimal(method.Execute()) > 0) { // 0 - значение исключения
					controlTicketsPrice.ControlValue = Convert.ToDecimal(method.Execute());
					controlTicketsPrice.Commit();
				}
				else {
					MessageBox.Show("Билеты не найдены.", "Сообщение");
				}
			}
			else {
				controlTicketsPrice.ControlValue = null;
				controlTicketsPrice.Commit();
				MessageBox.Show("Заполните поля: \"Даты командировки С:\", \"по:\", \"Город:\".", "Ошибка");
			}
		}
		else {
			MessageBox.Show("Стоимость билетов расcчитывается автоматически только для авиаперелётов.", "Сообщение");
		}
    }

    private void Tickets_EditValueChanged(System.Object sender, System.EventArgs e) {
		ILayoutPropertyItem controlTickets = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("Tickets");
        ILayoutPropertyItem controlTicketsPrice = CustomizableControl.FindPropertyItem<ILayoutPropertyItem>("TicketsPrice");
		if (controlTickets == null || controlTicketsPrice == null) {return;}
		controlTicketsPrice.ControlValue = null;
    }
	
	/* Третье задание-- */

    #endregion

    }
}