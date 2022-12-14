using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.ObjectModel.Services;
using DocsVision.Platform.WebClient;
using ServerExtension.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerExtension.Services {
    public class SendForAgreementService : ISendForAgreementService {
        public SendForAgreementData GetSendForAgreement(SessionContext context, Guid cardId) {
            BaseCard card = context.ObjectContext.GetObject<BaseCard>(cardId);
            KindsCardKind сardKind = card.SystemInfo.CardKind;

            IList<StatesStateMachineBranch> statesStateMachineBranch = context.ObjectContext.GetService<IStateService>().GetStateMachineBranches(сardKind);
            StatesStateMachineBranch statesStateMachineBranchLines = statesStateMachineBranch.Where(t => t.StartState == card.SystemInfo.State
                && t.BranchType == StatesStateMachineBranchBranchType.Line
                && t.EndState.DefaultName.Equals("WaitingForAgreement")).FirstOrDefault();

            IStateService stateService = context.ObjectContext.GetService<IStateService>();
            stateService.ChangeState(card, statesStateMachineBranchLines);

            SendForAgreementData model = new SendForAgreementData() {
                Message = "Состояние изменено!\nC \"Проект\" на \"На согласование\".\n"
            };
            return model;
        }
    }
}