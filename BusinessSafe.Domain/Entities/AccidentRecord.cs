using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Constants;

namespace BusinessSafe.Domain.Entities
{
    public class AccidentRecord : Entity<long>
    {
        public virtual long CompanyId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Reference { get; set; }
        public virtual Jurisdiction Jurisdiction { get; set; }
        public virtual PersonInvolvedEnum? PersonInvolved { get; set; }
        public virtual string PersonInvolvedOtherDescription { get; set; }
        public virtual int? PersonInvolvedOtherDescriptionId { get; set; }
        public virtual Employee EmployeeInjured { get; set; }
        public virtual string NonEmployeeInjuredForename { get; set; }
        public virtual string NonEmployeeInjuredSurname { get; set; }
        public virtual string NonEmployeeInjuredAddress1 { get; set; }
        public virtual string NonEmployeeInjuredAddress2 { get; set; }
        public virtual string NonEmployeeInjuredAddress3 { get; set; }
        public virtual string NonEmployeeInjuredCountyState { get; set; }
        public virtual Country NonEmployeeInjuredCountry { get; set; }
        public virtual string NonEmployeeInjuredPostcode { get; set; }
        public virtual string NonEmployeeInjuredContactNumber { get; set; }
        public virtual string NonEmployeeInjuredOccupation { get; set; }
        public virtual DateTime? DateAndTimeOfAccident { get; set; }
        public virtual Site SiteWhereHappened { get; set; }
        public virtual string OffSiteSpecifics { get; set; }
        public virtual string Location { get; set; }
        public virtual AccidentType AccidentType { get; set; }
        public virtual string AccidentTypeOther { get; set; }
        public virtual CauseOfAccident CauseOfAccident { get; set; }
        public virtual string CauseOfAccidentOther { get; set; }
        public virtual bool? FirstAidAdministered { get; set; }
        public virtual Employee EmployeeFirstAider { get; set; }
        public virtual string NonEmployeeFirstAiderSpecifics { get; set; }
        public virtual string DetailsOfFirstAidTreatment { get; set; }
        public virtual SeverityOfInjuryEnum? SeverityOfInjury { get; set; }
        public virtual IList<AccidentRecordNextStepSection> AccidentRecordNextStepSections { get; set; }
        public virtual bool IsReportable { get; set; }
        public virtual bool Status { get; set; }
        public virtual bool DoNotSendEmailNotification { get; set; }
        public virtual bool EmailNotificationSent { get; set; }

        public virtual string AdditionalInjuryInformation
        {
            get
            {
                var result = string.Empty;
                if (AccidentRecordInjuries.Any(x => x.Injury.Id == Injury.UNKOWN_INJURY))
                {
                    result =
                        AccidentRecordInjuries.First(x => x.Injury.Id == Injury.UNKOWN_INJURY).AdditionalInformation;
                }
                return result;
            }
        }

        public virtual string AdditionalBodyPartInformation
        {
            get
            {
                var result = string.Empty;
                if (AccidentRecordBodyParts.Any(x => x.BodyPart.Id == BodyPart.UNKOWN_BODY_PART))
                {
                    result =
                        AccidentRecordBodyParts.First(x => x.BodyPart.Id == BodyPart.UNKOWN_BODY_PART).
                            AdditionalInformation;
                }
                return result;
            }
        }

        public virtual IList<AccidentRecordInjury> AccidentRecordInjuries
        {
            get { return _accidentRecordInjuries.Where(x => !x.Deleted).ToList(); }
        }

        public virtual IList<AccidentRecordBodyPart> AccidentRecordBodyParts
        {
            get { return _accidentRecordBodyParts.Where(x => !x.Deleted).ToList(); }
        }

        public virtual bool? InjuredPersonWasTakenToHospital { get; set; }
        public virtual YesNoUnknownEnum? InjuredPersonAbleToCarryOutWork { get; set; }
        public virtual LengthOfTimeUnableToCarryOutWorkEnum? LengthOfTimeUnableToCarryOutWork { get; set; }
        public virtual string DescriptionHowAccidentHappened { get; set; }

        public virtual IList<AccidentRecordDocument> AccidentRecordDocuments
        {
            get { return _accidentRecordDocuments.Where(x => !x.Deleted).ToList(); }
        }

        protected IList<AccidentRecordDocument> _accidentRecordDocuments;
        protected IList<AccidentRecordInjury> _accidentRecordInjuries;
        protected IList<AccidentRecordBodyPart> _accidentRecordBodyParts;


        public virtual string InjuredPersonFullName
        {
            get
            {
                if (PersonInvolved == PersonInvolvedEnum.Employee && EmployeeInjured != null)
                {
                    return EmployeeInjured.FullName;
                }

                string fullName = NonEmployeeInjuredForename;

                if (!String.IsNullOrEmpty(NonEmployeeInjuredForename) &&
                    !String.IsNullOrEmpty(NonEmployeeInjuredSurname))
                    fullName += " ";

                fullName += NonEmployeeInjuredSurname;
                return fullName;
            }
        }

        public AccidentRecord()
        {
            _accidentRecordDocuments = new List<AccidentRecordDocument>();
            _accidentRecordInjuries = new List<AccidentRecordInjury>();
            _accidentRecordBodyParts = new List<AccidentRecordBodyPart>();
            AccidentRecordNextStepSections = new List<AccidentRecordNextStepSection>();
        }

        public static AccidentRecord Create(string title, string reference, Jurisdiction jurisdiction, long companyId,
                                            UserForAuditing user)
        {
            var accidentRecord = new AccidentRecord
                                     {
                                         CompanyId = companyId,
                                         Title = title,
                                         Reference = reference,
                                         Jurisdiction = jurisdiction,
                                         CreatedBy = user,
                                         CreatedOn = DateTime.Now,
                                         Deleted = false,
                                         Status = true
                                     };

            accidentRecord.CalculateDerivedFields(user);
            return accidentRecord;
        }

        protected void AddAndRemoveRequiredNextSteps(UserForAuditing currentUser)
        {
            var nextStepsSections = CalculateRequiredNextStepsSections();

            foreach (var nextStep in nextStepsSections)
            {
                if (
                    !AccidentRecordNextStepSections.Where(x => !x.Deleted).Select(x => x.NextStepsSection).Contains(
                        nextStep))
                {
                    AddNextStepSection(currentUser, nextStep);
                }
            }

            foreach (var accidentRecordNextStepSection in AccidentRecordNextStepSections)
            {
                if (!nextStepsSections.Contains(accidentRecordNextStepSection.NextStepsSection))
                {
                    accidentRecordNextStepSection.MarkForDelete(currentUser);
                }
            }
        }

        protected List<NextStepsSectionEnum> CalculateRequiredNextStepsSections()
        {
            var nextStepsSections = new List<NextStepsSectionEnum>();

            nextStepsSections.Add(NextStepsSectionEnum.Section1);

            if (Jurisdiction.Name == JurisdictionNames.GB
                || Jurisdiction.Name == JurisdictionNames.IoM
                || Jurisdiction.Name == JurisdictionNames.Guernsey
                || Jurisdiction.Name == JurisdictionNames.Jersey)
            {
                nextStepsSections.Add(NextStepsSectionEnum.Section2_1);
                nextStepsSections.Add(NextStepsSectionEnum.Section3_1);
            }

            if (Jurisdiction.Name == JurisdictionNames.ROI)
            {
                nextStepsSections.Add(NextStepsSectionEnum.Section2_2);
                nextStepsSections.Add(NextStepsSectionEnum.Section3_2);
            }

            if (Jurisdiction.Name == JurisdictionNames.NI)
            {
                nextStepsSections.Add(NextStepsSectionEnum.Section2_3);
                nextStepsSections.Add(NextStepsSectionEnum.Section3_3);
            }

            if (IsReportable)
            {
                if (Jurisdiction.Name == JurisdictionNames.GB)
                {
                    nextStepsSections.Add(NextStepsSectionEnum.Section4_1);
                }

                if (Jurisdiction.Name == JurisdictionNames.NI)
                {
                    nextStepsSections.Add(NextStepsSectionEnum.Section4_2);
                }

                if (Jurisdiction.Name == JurisdictionNames.ROI)
                {
                    nextStepsSections.Add(NextStepsSectionEnum.Section4_3);
                }

                if (Jurisdiction.Name == JurisdictionNames.Guernsey)
                {
                    nextStepsSections.Add(NextStepsSectionEnum.Section4_4);
                }

                if (Jurisdiction.Name == JurisdictionNames.Jersey)
                {
                    nextStepsSections.Add(NextStepsSectionEnum.Section4_5);
                }

                if (Jurisdiction.Name == JurisdictionNames.IoM)
                {
                    nextStepsSections.Add(NextStepsSectionEnum.Section4_6);
                }
            }

            return nextStepsSections;
        }

        protected void AddNextStepSection(UserForAuditing currentUser, NextStepsSectionEnum nextStepSection)
        {
            AccidentRecordNextStepSections.Add(new AccidentRecordNextStepSection()
                                                   {
                                                       AccidentRecord = this,
                                                       NextStepsSection = nextStepSection,
                                                       CreatedBy = currentUser,
                                                       CreatedOn = DateTime.Now
                                                   });
        }

        public virtual void UpdateInjuredPerson(PersonInvolvedEnum? personInvolved,
                                                string personInvolvedOtherDescription,
                                                Employee employeeInjured,
                                                string nonEmployeeInjuredForename,
                                                string nonEmployeeInjuredSurname,
                                                string nonEmployeeInjuredAddress1,
                                                string nonEmployeeInjuredAddress2,
                                                string nonEmployeeInjuredAddress3,
                                                string nonEmployeeInjuredCountyState,
                                                Country nonEmployeeInjuredCountry,
                                                string nonEmployeeInjuredPostcode,
                                                string nonEmployeeInjuredContactNumber,
                                                string nonEmployeeInjuredOccupation,
                                                int? personInvolvedOtherDescriptionId,

                                                UserForAuditing currentUser
            )
        {
            PersonInvolved = personInvolved;
            PersonInvolvedOtherDescription = personInvolvedOtherDescription;
            PersonInvolvedOtherDescriptionId = personInvolvedOtherDescriptionId;
            EmployeeInjured = employeeInjured;
            NonEmployeeInjuredForename = nonEmployeeInjuredForename;
            NonEmployeeInjuredSurname = nonEmployeeInjuredSurname;
            NonEmployeeInjuredAddress1 = nonEmployeeInjuredAddress1;
            NonEmployeeInjuredAddress2 = nonEmployeeInjuredAddress2;
            NonEmployeeInjuredAddress3 = nonEmployeeInjuredAddress3;
            NonEmployeeInjuredCountyState = nonEmployeeInjuredCountyState;
            NonEmployeeInjuredCountry = nonEmployeeInjuredCountry;
            NonEmployeeInjuredPostcode = nonEmployeeInjuredPostcode;
            NonEmployeeInjuredContactNumber = nonEmployeeInjuredContactNumber;
            NonEmployeeInjuredOccupation = nonEmployeeInjuredOccupation;
            SetLastModifiedDetails(currentUser);
        }

        public virtual void UpdateAccidentDetails(DateTime? dateOfAccident, Site site, string offSiteName,
                                                  string location,
                                                  AccidentType accidentType, string otherAccidentType,
                                                  CauseOfAccident accidentCause, string otherAccidentCause,
                                                  Employee firstAider,
                                                  string nonEmployeeFirstAiderName, string detailsOfFirstAid,
                                                  UserForAuditing user)
        {
            DateAndTimeOfAccident = dateOfAccident;
            SiteWhereHappened = site;
            OffSiteSpecifics = offSiteName;
            Location = location;
            AccidentType = accidentType;
            AccidentTypeOther = otherAccidentType;
            CauseOfAccident = accidentCause;
            CauseOfAccidentOther = otherAccidentCause;
            EmployeeFirstAider = firstAider;
            NonEmployeeFirstAiderSpecifics = nonEmployeeFirstAiderName;
            FirstAidAdministered = firstAider != null || !string.IsNullOrEmpty(nonEmployeeFirstAiderName);
            DetailsOfFirstAidTreatment = detailsOfFirstAid;
            SetLastModifiedDetails(user);
        }

        public virtual void AddAccidentDocumentRecord(AccidentRecordDocument accidentRecordDocument)
        {
            accidentRecordDocument.AccidentRecord = this;
            _accidentRecordDocuments.Add(accidentRecordDocument);
            SetLastModifiedDetails(accidentRecordDocument.CreatedBy);
        }

        public virtual void RemoveAccidentDocumentRecord(long documentLibraryId, UserForAuditing user)
        {
            if (!_accidentRecordDocuments.Any(x => x.DocumentLibraryId == documentLibraryId)) return;
            var document = _accidentRecordDocuments.Single(x => x.DocumentLibraryId == documentLibraryId);
            document.Deleted = true;
            document.SetLastModifiedDetails(user);
            SetLastModifiedDetails(user);
        }

        public virtual void SetSummaryDetails(string title, string reference, Jurisdiction jurisdiction,
                                              UserForAuditing user)
        {
            Title = title;
            Reference = reference;
            Jurisdiction = jurisdiction;
            SetLastModifiedDetails(user);
            CalculateDerivedFields(user);
        }

        /// <summary>
        /// indicates whether sufficient information has been entered to allow the users to view next steps
        /// </summary>
        /// <returns></returns>
        public virtual bool NextStepsAvailable
        {
            get
            {
                if (SeverityOfInjury.HasValue && SeverityOfInjury.Value == SeverityOfInjuryEnum.Fatal)
                {
                    return true;
                }

                if (!SeverityOfInjury.HasValue
                    || !InjuredPersonWasTakenToHospital.HasValue
                    || !PersonInvolved.HasValue)
                {
                    return false;
                }

                switch (PersonInvolved)
                {
                    case PersonInvolvedEnum.Employee:
                    case PersonInvolvedEnum.PersonAtWork:
                        
                        if (InjuredPersonAbleToCarryOutWork.HasValue)
                        {
                            switch (InjuredPersonAbleToCarryOutWork)
                            {
                              case YesNoUnknownEnum.No:
                                    return LengthOfTimeUnableToCarryOutWork.HasValue;
                                default:
                                    return true;
                            }
                       }
                        break;
                    case PersonInvolvedEnum.Visitor:
                    case PersonInvolvedEnum.Other:
                        return true;
                }

                return false;
            }
        }

        protected void CalculateDerivedFields(UserForAuditing userForAuditing)
        {
            CalculateIsReportable();
            AddAndRemoveRequiredNextSteps(userForAuditing);
        }

        protected void CalculateIsReportable()
        {
            if (!PersonInvolved.HasValue ||
                (PersonInvolved.Value == PersonInvolvedEnum.PersonAtWork ||
                 PersonInvolved.Value == PersonInvolvedEnum.Employee))
            {
                if (SeverityOfInjury.HasValue && SeverityOfInjury.Value == SeverityOfInjuryEnum.Fatal)
                {
                    if (Jurisdiction.Name == JurisdictionNames.GB
                        || Jurisdiction.Name == JurisdictionNames.NI
                        || Jurisdiction.Name == JurisdictionNames.ROI
                        || Jurisdiction.Name == JurisdictionNames.IoM
                        || Jurisdiction.Name == JurisdictionNames.Guernsey)
                    {
                        IsReportable = true;
                        return;
                    }
                }

                if (SeverityOfInjury.HasValue && SeverityOfInjury.Value == SeverityOfInjuryEnum.Major)
                {
                    if (Jurisdiction.Name == JurisdictionNames.GB
                        || Jurisdiction.Name == JurisdictionNames.NI
                        || Jurisdiction.Name == JurisdictionNames.IoM)
                    {
                        IsReportable = true;
                        return;
                    }
                }
            }

            if (PersonInvolved.HasValue &&
                (PersonInvolved.Value == PersonInvolvedEnum.Visitor || PersonInvolved.Value == PersonInvolvedEnum.Other))
            {
                if (SeverityOfInjury.HasValue && SeverityOfInjury.Value == SeverityOfInjuryEnum.Fatal)
                {
                    if (Jurisdiction.Name == JurisdictionNames.GB
                        || Jurisdiction.Name == JurisdictionNames.NI
                        || Jurisdiction.Name == JurisdictionNames.ROI
                        || Jurisdiction.Name == JurisdictionNames.IoM
                        || Jurisdiction.Name == JurisdictionNames.Guernsey)
                    {
                        IsReportable = true;
                        return;
                    }
                }

                if (SeverityOfInjury.HasValue && SeverityOfInjury.Value == SeverityOfInjuryEnum.Major)
                {
                    if (Jurisdiction.Name == JurisdictionNames.IoM)
                    {
                        IsReportable = true;
                        return;
                    }
                }
            }

            if (InjuredPersonWasTakenToHospital.HasValue && InjuredPersonWasTakenToHospital.Value)
            {
                if (Jurisdiction.Name == JurisdictionNames.GB
                    || Jurisdiction.Name == JurisdictionNames.NI
                    || Jurisdiction.Name == JurisdictionNames.ROI
                    || Jurisdiction.Name == JurisdictionNames.Guernsey)
                {
                    IsReportable = true;
                    return;
                }
            }

            if (PersonInvolved.HasValue &&
                (PersonInvolved.Value == PersonInvolvedEnum.PersonAtWork ||
                 PersonInvolved.Value == PersonInvolvedEnum.Employee))
            {

                if (LengthOfTimeUnableToCarryOutWork.HasValue &&
                    (LengthOfTimeUnableToCarryOutWork.Value == LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays
                     || LengthOfTimeUnableToCarryOutWork == LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays))
                {
                    if (Jurisdiction.Name == JurisdictionNames.IoM
                        || Jurisdiction.Name == JurisdictionNames.NI
                        || Jurisdiction.Name == JurisdictionNames.ROI
                        || Jurisdiction.Name == JurisdictionNames.Guernsey)
                    {
                        IsReportable = true;
                        return;
                    }
                }

                if (LengthOfTimeUnableToCarryOutWork.HasValue &&
                    (LengthOfTimeUnableToCarryOutWork == LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays))
                {
                    if (Jurisdiction.Name == JurisdictionNames.GB)
                    {
                        IsReportable = true;
                        return;
                    }
                }
            }

            IsReportable = false;
        }

        public virtual void SetAccidentRecordOverviewDetails(string description, bool doNotSendEmailNotification, UserForAuditing user)
        {
            DescriptionHowAccidentHappened = description;
            DoNotSendEmailNotification = doNotSendEmailNotification;
            SetLastModifiedDetails(user);
        }

        public virtual void AddInjury(Injury injury, UserForAuditing user)
        {
            if (_accidentRecordInjuries != null && _accidentRecordInjuries.All(x => x.Injury.Id != injury.Id))
            {
                _accidentRecordInjuries.Add(AccidentRecordInjury.Create(this, injury, user));
            }
        }

        public virtual void RemoveInjury(Injury injury, UserForAuditing user)
        {
            if (_accidentRecordInjuries != null && _accidentRecordInjuries.Any(x => x.Injury.Id == injury.Id))
            {
                var accidentRecordInjury = _accidentRecordInjuries.First(x => x.Injury.Id == injury.Id);
                accidentRecordInjury.MarkForDelete(user);
            }
        }

        public virtual void AddBodyPartThatWasInjured(BodyPart bodyPart, UserForAuditing user)
        {
            if (_accidentRecordBodyParts == null || !_accidentRecordBodyParts.All(x => x.BodyPart.Id != bodyPart.Id)) return;
            _accidentRecordBodyParts.Add(AccidentRecordBodyPart.Create(this, bodyPart, user));
        }

        public virtual void RemoveBodyPartThatWasInjured(BodyPart bodyPart, UserForAuditing user)
        {
            if (_accidentRecordBodyParts == null || !_accidentRecordBodyParts.Any(x => x.BodyPart.Id == bodyPart.Id)) return;
            var accidentRecordBodyPart = _accidentRecordBodyParts.First(x => x.BodyPart.Id == bodyPart.Id);
            accidentRecordBodyPart.MarkForDelete(user);
        }

        public virtual void UpdateInjuryDetails(SeverityOfInjuryEnum? severity, List<BodyPart> bodyParts,
                                                string customBodyPartDescription, List<Injury> injuries,
                                                string customInjuryDescription, bool? injuredPersonWasTakenToHospital,
                                                YesNoUnknownEnum? injuredPersonAbleToCarryOutWork,
                                                LengthOfTimeUnableToCarryOutWorkEnum? lengthOfTimeUnableToCarryOutWork,
                                                UserForAuditing user)
        {
            SetLastModifiedDetails(user);
            SeverityOfInjury = severity;
            InjuredPersonWasTakenToHospital = injuredPersonWasTakenToHospital;
            InjuredPersonAbleToCarryOutWork = injuredPersonAbleToCarryOutWork;
            LengthOfTimeUnableToCarryOutWork = lengthOfTimeUnableToCarryOutWork;

            if (bodyParts != null)
            {
                bodyParts.ForEach(x => AddBodyPartThatWasInjured(x, user));

                //remove the body part that are on the accident record but not on the request object
                var bodyPartIds = bodyParts.Select(bp => bp.Id);
                var bodyPartsToRemove = AccidentRecordBodyParts
                    .Where(x => !bodyPartIds.Contains(x.BodyPart.Id))
                    .Select(x => x.BodyPart);

                bodyPartsToRemove.ToList()
                    .ForEach(bodyPart => RemoveBodyPartThatWasInjured(bodyPart, user));

                var accidentBodyPart = AccidentRecordBodyParts.FirstOrDefault(x => x.BodyPart.Id == BodyPart.UNKOWN_BODY_PART);

                if (accidentBodyPart != null)
                {
                    accidentBodyPart.AdditionalInformation = customBodyPartDescription;
                    accidentBodyPart.SetLastModifiedDetails(user);
                }
            }

            if (injuries != null)
            {
                injuries.ForEach(x => AddInjury(x, user));

                //remove the injuries that are on the accident record but not on the request object
                var injuryIds = injuries.Select(injury => injury.Id);
                var injuriesToRemove = AccidentRecordInjuries
                    .Where(x => !injuryIds.Contains(x.Injury.Id))
                    .Select(x => x.Injury);

                injuriesToRemove.ToList()
                    .ForEach(injury => RemoveInjury(injury, user));

                var accidentInjury = AccidentRecordInjuries.FirstOrDefault(x => x.Injury.Id == Injury.UNKOWN_INJURY);

                if (accidentInjury != null)
                {
                    accidentInjury.AdditionalInformation = customInjuryDescription;
                    accidentInjury.SetLastModifiedDetails(user);
                }
            }

            CalculateDerivedFields(user);
        }

        public virtual void UpdateAccidentRecordStatus(AccidentRecordStatusEnum status, UserForAuditing user)
        {
            Status = status == AccidentRecordStatusEnum.Open;
            SetLastModifiedDetails(user);
        }

        public virtual void UpdateEmailNotificationSentStatus(bool status, UserForAuditing user)
        {
            EmailNotificationSent = status;
            SetLastModifiedDetails(user);
        }
    }
}
