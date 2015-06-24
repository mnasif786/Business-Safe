using System;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class TaskDocument : Document
    {
        public virtual Task Task { get; set; }
        public virtual DocumentOriginType DocumentOriginType { get; set; }

        public static TaskDocument Create(CreateDocumentParameters parameters, Task task)
        {
            return new TaskDocument
                       {
                           ClientId = parameters.ClientId,
                           DocumentLibraryId = parameters.DocumentLibraryId,
                           Filename = parameters.Filename,
                           Title = (parameters.Filename != null && parameters.Filename.Length > 100 ? parameters.Filename.Substring(0,100) : parameters.Filename),
                           Extension = parameters.Extension,
                           FilesizeByte = parameters.FilesizeByte,
                           Description = parameters.Description,
                           DocumentType = parameters.DocumentType,
                           CreatedBy = parameters.CreatedBy,
                           CreatedOn = parameters.CreatedOn,
                           Deleted = false,
                           DocumentOriginType = parameters.DocumentOriginType,
                           Task = task
                       };
        }

        public override string DocumentReference
        {
            get
            {
                if (Task == null)
                    throw new ApplicationException(string.Format("Task Document {0} does not have a task.", Id));

                var result = string.Empty;

                if (Task is MultiHazardRiskAssessmentFurtherControlMeasureTask)
                {
                    var riskAssessment = Task.RiskAssessment;

                    result = string.Format("{0} : {1}", riskAssessment.PreFix, riskAssessment.Reference);
                }
                else if (Task is ResponsibilityTask)
                {
                    var responsibility = ((ResponsibilityTask) Task).Responsibility;
                    result = string.Format("{0}", responsibility.Title);
                }
                return result;
            }
        }

        public override string SiteReference
        {
            get
            {
                if (Task == null)
                    throw new ApplicationException(string.Format("Task Document {0} does not have a task.", Id));

                var result = string.Empty;
                if (Task is MultiHazardRiskAssessmentFurtherControlMeasureTask)
                {
                    var riskAssessment = Task.RiskAssessment;

                    result = riskAssessment.RiskAssessmentSite != null ? riskAssessment.RiskAssessmentSite.Name : string.Empty;
                }
                else if (Task is ResponsibilityTask)
                {
                    var responsibility = ((ResponsibilityTask)Task).Responsibility;
                    result = responsibility.Site != null ? responsibility.Site.Name : string.Empty;
                }
                return result;
            }
        }

        public virtual TaskDocument CloneForReoccurring(UserForAuditing user)
        {
                return new TaskDocument()
                           {
                               DocumentLibraryId = DocumentLibraryId,
                               Filename = Filename,
                               Extension = Extension,
                               FilesizeByte = FilesizeByte,
                               Description = Description,
                               DocumentType = DocumentType,
                               CreatedBy = user,
                               CreatedOn = DateTime.Now,
                               DocumentOriginType = DocumentOriginType
                           };
        }
    }
}