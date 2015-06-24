using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;

namespace BusinessSafe.Domain.Entities
{
    public abstract class RiskAssessment : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual string Reference { get; set; }
        public virtual DateTime? AssessmentDate { get; set; }
        public virtual long CompanyId { get; set; }
        public virtual RiskAssessor RiskAssessor { get; set; }
        public virtual RiskAssessmentStatus Status { get; set; }
        public virtual SiteStructureElement RiskAssessmentSite { get; set; }
        public virtual IList<RiskAssessmentEmployee> Employees { get; protected set; }
        public virtual IList<RiskAssessmentNonEmployee> NonEmployees { get; protected set; }
        public virtual IList<RiskAssessmentReview> Reviews { get; set; }
        public virtual IList<RiskAssessmentDocument> Documents { get; set; }

        public RiskAssessment()
        {
            Employees = new List<RiskAssessmentEmployee>();
            NonEmployees = new List<RiskAssessmentNonEmployee>();
            Reviews = new List<RiskAssessmentReview>();
            Documents = new List<RiskAssessmentDocument>();
        }

        public abstract string PreFix { get; }
        public abstract bool HasUndeletedTasks();
        public abstract bool HasUncompletedTasks();
        public abstract bool HasAnyReviews();
        public abstract IEnumerable<FurtherControlMeasureTask> GetAllUncompleteFurtherControlMeasureTasks();

        protected DateTime? _nextReviewDate;
        public virtual DateTime? NextReviewDate
        {
            get { return _nextReviewDate; }
        }

        public virtual void AttachNonEmployeeToRiskAssessment(NonEmployee nonEmployee, UserForAuditing modifyingUser)
        {
            if (!NonEmployees.Any(x => x.NonEmployee == nonEmployee))
            {
                NonEmployees.Add(new RiskAssessmentNonEmployee
                {
                    NonEmployee = nonEmployee,
                    RiskAssessment = this,
                    CreatedOn = DateTime.Now,
                    CreatedBy = modifyingUser,
                    Deleted = false
                });

                SetLastModifiedDetails(modifyingUser);
            }

           
        }

        public virtual void DetachNonEmployeeFromRiskAssessment(NonEmployee nonEmployee, UserForAuditing modifyingUser)
        {
            if (NonEmployees.Count(x => x.NonEmployee == nonEmployee) == 0)
            {
                throw new NonEmployeeNotAttachedToRiskAssessmentException(this, nonEmployee);
            }

            var existingNonEmployee = NonEmployees.Single(x => x.NonEmployee == nonEmployee);
            existingNonEmployee.Deleted = true;
            existingNonEmployee.LastModifiedBy = modifyingUser;
            existingNonEmployee.LastModifiedOn = DateTime.Now;
            SetLastModifiedDetails(modifyingUser);
        }

        public virtual void AttachEmployeeToRiskAssessment(Employee employee, UserForAuditing modifyingUser)
        {
            if (!Employees.Any(x => x.Employee == employee))
            {
                Employees.Add(new RiskAssessmentEmployee
                {
                    Employee = employee,
                    RiskAssessment = this,
                    CreatedOn = DateTime.Now,
                    CreatedBy = modifyingUser,
                    Deleted = false
                });

                SetLastModifiedDetails(modifyingUser);
            }

          
        }

        public virtual void DetachEmployeesFromRiskAssessment(IEnumerable<Employee> employees, UserForAuditing modifyingUser)
        {
            foreach (var employee in employees)
            {
                if (Employees.Any(x => x.Employee == employee))
                {
                    var existingEmployee = Employees.Single(x => x.Employee == employee);
                    existingEmployee.Deleted = true;
                    existingEmployee.LastModifiedBy = modifyingUser;
                    existingEmployee.LastModifiedOn = DateTime.Now;
                }
            }

            SetLastModifiedDetails(modifyingUser);
        }

        public virtual void MarkAsArchived(UserForAuditing user)
        {
            SetLastModifiedDetails(user);
            Status = RiskAssessmentStatus.Archived;
        }

        public virtual void MarkAsDraft(UserForAuditing user)
        {
            SetLastModifiedDetails(user);
            Status = RiskAssessmentStatus.Draft;
        }

        public virtual void MarkAsLive(UserForAuditing user)
        {
            SetLastModifiedDetails(user);
            Status = RiskAssessmentStatus.Live;
        }

        public virtual void ReinstateRiskAssessmentAsNotDeleted(UserForAuditing user)
        {
            SetLastModifiedDetails(user);
            Deleted = false;
        }

        public virtual void AttachDocumentToRiskAssessment(IEnumerable<RiskAssessmentDocument> documents, UserForAuditing user)
        {
            foreach (var document in documents)
            {
                AttachDocumentToRiskAssessment(document, user);
            }
        }

        public virtual void AttachDocumentToRiskAssessment(RiskAssessmentDocument document, UserForAuditing user)
        {
            if (Documents.Count(x => x.DocumentLibraryId == document.DocumentLibraryId) > 0)
            {
                throw new DocumentAlreadyAttachedToRiskAssessmentException(this, document);
            }
            document.RiskAssessment = this;
            Documents.Add(document);
            SetLastModifiedDetails(user);
        }

        public virtual void DetachDocumentFromRiskAssessment(IEnumerable<long> documentsToDetachIds, UserForAuditing user)
        {
            foreach (var documentsToDetachId in documentsToDetachIds)
            {
                DetachDocumentFromRiskAssessment(documentsToDetachId, user);
            }
        }

        private void DetachDocumentFromRiskAssessment(long documentsToDetachId, UserForAuditing user)
        {
            if (Documents.Count(h => h.DocumentLibraryId == documentsToDetachId) == 0)
            {
                throw new RiskAssessmentDocumentDoesNotExistInRiskAssessmentException(this, documentsToDetachId);
            }

            var document = Documents.Single(x => x.DocumentLibraryId == documentsToDetachId);
            document.SetLastModifiedDetails(user);

            //Documents.Remove(document);
            document.Deleted = true;
            document.SetLastModifiedDetails(user);
            SetLastModifiedDetails(user);
        }

        public virtual DateTime? GetDefaultDateOfNextReview()
        {
            if (!AssessmentDate.HasValue)
            {
                return null;
            }

            if (Reviews == null || !Reviews.Any())
            {
                return AssessmentDate.Value.AddYears(1);
            }

            if (!Reviews.Last().CompletedDate.HasValue)
            {
                return Reviews.Last().CompletionDueDate.Value.AddYears(1);
            }

            return Reviews.Last().CompletedDate.Value.AddYears(1);
        }

        public virtual RiskAssessment Self
        {
            get { return this; }
        }

        protected bool IsDifferentRiskAssessor(RiskAssessor riskAssessor)
        {
            return (RiskAssessor != null && riskAssessor != null
                   && RiskAssessor.Id != riskAssessor.Id)
                   || (RiskAssessor == null && riskAssessor != null)
                   || (RiskAssessor != null && riskAssessor == null);
        }

        public abstract RiskAssessment Copy(string newTitle, string newReference, UserForAuditing user);

        public virtual List<RiskAssessment> CopyForMultipleSites(string newTitle, IEnumerable<Site> sites, UserForAuditing user)
        {
            var riskAssessments = new List<RiskAssessment>();

            foreach(var site in sites)
            {
                var riskAssessment = Copy(newTitle, null, user);
                riskAssessment.RiskAssessmentSite = site;
                riskAssessments.Add(riskAssessment);
            }

            return riskAssessments;
        }

        public virtual void AddReview(RiskAssessmentReview review)
        {
            review.RiskAssessment = this;
            Reviews.Add(review);
            RecalculateNextReviewDate();
        }

        public virtual void RecalculateNextReviewDate()
        {
            _nextReviewDate = CalculateNextReviewDate(Reviews);
        }

        static DateTime? CalculateNextReviewDate(IList<RiskAssessmentReview> reviews)
        {
            return reviews.Any()
                           ? reviews.Where(review => review.Deleted == false && review.CompletedDate == null ).Min(review => review.CompletionDueDate)
                           : null;
        }
    }
}