using System;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class RiskAssessmentDocument : Document
    {
        public virtual RiskAssessment RiskAssessment { get; set; }

        public static RiskAssessmentDocument Create(CreateDocumentParameters parameters)
        {
            return new RiskAssessmentDocument
                       {
                           ClientId = parameters.ClientId,
                           DocumentLibraryId = parameters.DocumentLibraryId,
                           Filename = parameters.Filename,
                           Title = parameters.Filename,
                           Extension = parameters.Extension,
                           FilesizeByte = parameters.FilesizeByte,
                           Description = parameters.Description,
                           DocumentType = parameters.DocumentType,
                           CreatedBy = parameters.CreatedBy,
                           CreatedOn = parameters.CreatedOn,
                           Deleted = false
                       };
        }

        public virtual RiskAssessmentDocument CloneForRiskAssessmentTemplating(UserForAuditing user)
        {
            var result = new RiskAssessmentDocument()
                             {
                                 ClientId = ClientId,
                                 DocumentLibraryId = DocumentLibraryId,
                                 Filename = Filename,
                                 Extension = Extension,
                                 FilesizeByte = FilesizeByte,
                                 Description = Description,
                                 DocumentType = DocumentType,
                                 CreatedBy = user,
                                 CreatedOn = DateTime.Now,
                                 Deleted = false
                             };
            return result;
        }

        public override string DocumentReference
        {
            get
            {
                if (RiskAssessment == null)
                    throw new ApplicationException("Risk Assessment Document does not have a risk assessment.");

                return string.Format("{0} : {1}", RiskAssessment.PreFix,  RiskAssessment.Reference);
            }
        }

        public override string SiteReference
        {
            get
            {
                if (RiskAssessment == null)
                    throw new ApplicationException("Risk Assessment Document does not have a risk assessment.");

                return RiskAssessment.RiskAssessmentSite != null ? RiskAssessment.RiskAssessmentSite.Name : string.Empty;
            }
        }
    }
}