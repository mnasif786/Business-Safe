using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Constants;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public class InjuryDetailsViewModelFactory : IInjuryDetailsViewModelFactory
    {
        private readonly IInjuryService _injuryService;
        private readonly IBodyPartService _bodyPartService;
        private readonly IAccidentRecordService _accidentRecordService;

        private const string _ROIMessage = "Was the injured person taken to hospital or treated by a registered medical practitioner?";
        private const string _NonROIMessage = "Was the injured person taken to hospital?";
       
        // JurisdictionName mapped to URL
        private Dictionary< string,  string> _GuidanceNotesURL = new Dictionary<string, string>(); 
      
        public InjuryDetailsViewModelFactory(IInjuryService injuryService, IBodyPartService bodyPartService, IAccidentRecordService accidentRecordService)
        {
            _injuryService = injuryService;
            _bodyPartService = bodyPartService;
            _accidentRecordService = accidentRecordService;                      
        }

        public virtual string GetGuidanceNotesDocumentLibraryID(string jurisdiction)
        {
            return ConfigurationSettings.AppSettings["AccidentRecordGuidanceNote.ClientDocumentationID." + jurisdiction];
        }

        public InjuryDetailsViewModel GetViewModel(long accidentRecordId, long companyId)
        {
            var accidentRecord = _accidentRecordService.GetByIdAndCompanyIdWithEverything(accidentRecordId, companyId);
            var injuries = _injuryService.GetAllInjuriesForAccidentRecord(companyId, accidentRecordId).Select(x => new LookupDto(){Id =x.Id, Name = x.Description}).ToList();
            var bodyParts = _bodyPartService.GetAll().Select(x => new LookupDto() { Id = x.Id, Name = x.Description }).ToList();
            return new InjuryDetailsViewModel
                       {
                           AccidentRecordId = accidentRecord.Id,
                           CompanyId = accidentRecord.CompanyId,
                           Injuries = injuries,
                           SelectedInjuries = accidentRecord.AccidentRecordInjuries.Select(x => new LookupDto() { Id = x.Injury.Id, Name = x.Injury.Description }).ToList(),
                           BodyParts = bodyParts,
                           SelectedBodyParts = accidentRecord.AccidentRecordBodyParts.Select(x => new LookupDto() { Id = x.BodyPart.Id, Name = x.BodyPart.Description }).ToList(),
                           SeverityOfInjury = accidentRecord.SeverityOfInjury
                           ,InjuredPersonWasTakenToHospital = accidentRecord.InjuredPersonWasTakenToHospital
                           ,InjuredPersonAbleToCarryOutWork = accidentRecord.InjuredPersonAbleToCarryOutWork
                           ,LengthOfTimeUnableToCarryOutWork = accidentRecord.LengthOfTimeUnableToCarryOutWork
                           ,CustomInjuryDescription = accidentRecord.AdditionalInjuryInformation
                           ,CustomBodyPartyDescription = accidentRecord.AdditionalBodyPartInformation
                           ,
                           TakenToHospitalMessage = (accidentRecord.Jurisdiction != null && accidentRecord.Jurisdiction.Name == JurisdictionNames.ROI) ? _ROIMessage : _NonROIMessage    
                           ,NextStepsVisible = accidentRecord.NextStepsAvailable
                           ,GuidanceNotesId = (accidentRecord.Jurisdiction != null && accidentRecord.Jurisdiction.Name != null) 
                                                        ? GetGuidanceNotesDocumentLibraryID(accidentRecord.Jurisdiction.Name) : String.Empty
                           ,
                           ShowInjuredPersonAbleToCarryOutWorkSection =
                               accidentRecord.PersonInvolved == PersonInvolvedEnum.Employee ||
                               accidentRecord.PersonInvolved == PersonInvolvedEnum.PersonAtWork
                       };
        }      
    }

    public interface IInjuryDetailsViewModelFactory
    {
        InjuryDetailsViewModel GetViewModel(long accidentRecordId, long companyId);
    }
}