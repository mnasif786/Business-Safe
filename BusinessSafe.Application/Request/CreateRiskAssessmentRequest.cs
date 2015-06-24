using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.Application.Request
{
    public class CopyRiskAssessmentRequest
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentToCopyId { get; set; }
        public string Reference { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }

    public interface ICreateRiskAssessmentRequest
    {
        long CompanyId { get; set; }
        string Title { get; set; }
        string Reference { get; set; }
    }

    public class CreateRiskAssessmentRequest : ICreateRiskAssessmentRequest
    {
        public long CompanyId { get; set; }
        [Required(ErrorMessage = "Please enter Risk Assessment Title.")]
        [StringLength(200)]
        public string Title { get; set; }
        [Display(Name = "Ref")]
        [StringLength(50)]
        public string Reference { get; set; }
        public Guid UserId { get; set; }
        public string Location { get; set; }
        public string TaskProcessDescription { get; set; }
        public long? SiteId { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public long? RiskAssessorId { get; set; }
    }

    public class CreatePersonalRiskAssessmentRequest: CreateRiskAssessmentRequest
    {
        public bool IsSensitive { get; set; }
    }

    public class SaveRiskAssessmentPremisesInformationRequest
    {
        public string TaskProcessDescription { get; set; }
        public string LocationAreaDepartment { get; set; }
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }

    public interface ISaveRiskAssessmentSummaryRequest
    {
        long Id { get; set; }
        long CompanyId { get; set; }
        string Title { get; set; }
        string Reference { get; set; }
    }

    public class SaveRiskAssessmentSummaryRequest : ISaveRiskAssessmentSummaryRequest
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "Please enter Risk Assessment Title.")]
        [StringLength(200)]
        public string Title { get; set; }

        [Display(Name = "Ref")]
        [StringLength(50)]
        public string Reference { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public long? RiskAssessorId { get; set; }
        public Guid UserId { get; set; }
        public string PersonAppointed { get; set; }
        public long? SiteId { get; set; }
    }

    public class SaveGeneralRiskAssessmentRequest : ISaveRiskAssessmentSummaryRequest
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Please enter Risk Assessment Title.")]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        public DateTime? AssessmentDate { get; set; }
        public long? SiteId { get; set; }
        public long? RiskAssessorId { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(500)]
        public string TaskProcessDescription { get; set; }
    }

    public class SavePersonalRiskAssessmentRequest : ISaveRiskAssessmentSummaryRequest
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Please enter Risk Assessment Title.")]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        public DateTime? AssessmentDate { get; set; }
        public long? SiteId { get; set; }
        public long? RiskAssessorId { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(500)]
        public string TaskProcessDescription { get; set; }

        public bool Sensitive { get; set; }
    }
}