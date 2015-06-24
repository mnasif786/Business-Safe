using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class RiskAssessmentAttachmentService : IRiskAssessmentAttachmentService
    {
        private readonly IPeninsulaLog _log;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly INonEmployeeRepository _nonEmployeeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RiskAssessmentAttachmentService(
            IRiskAssessmentRepository riskAssessmentRepository, 
            IUserForAuditingRepository userForAuditingRepository, 
            IDocumentTypeRepository documentTypeRepository, 
            INonEmployeeRepository nonEmployeeRepository, 
            IPeninsulaLog log, 
            IEmployeeRepository employeeRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _documentTypeRepository = documentTypeRepository;
            _log = log;
            _employeeRepository = employeeRepository;
            _nonEmployeeRepository = nonEmployeeRepository;
        }

        public List<DocumentDto> AttachDocumentsToRiskAssessment(AttachDocumentsToRiskAssessmentRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

                var documents = CreateRiskAssessmentDocumentsToAttach(request, user);
                riskAssessment.AttachDocumentToRiskAssessment(documents, user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);

                //flush so that the new Task documents are given ids
                _riskAssessmentRepository.Flush();

                var selectAddedFiles = riskAssessment.Documents
                    .Where(d => documents.Select(dp => dp.DocumentLibraryId).Contains(d.DocumentLibraryId));

                return new DocumentDtoMapper().Map(selectAddedFiles).ToList();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void DetachDocumentsToRiskAssessment(DetachDocumentsFromRiskAssessmentRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

                riskAssessment.DetachDocumentFromRiskAssessment(request.DocumentsToDetach, user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<DocumentDto> GetRiskAssessmentAttachedDocuments(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            try
            {
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
                return new DocumentDtoMapper().Map(riskAssessment.Documents);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void AttachNonEmployeeToRiskAssessment(AttachNonEmployeeToRiskAssessmentRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                var nonEmployee = _nonEmployeeRepository.GetByIdAndCompanyId(request.NonEmployeeToAttachId, request.CompanyId);

                riskAssessment.AttachNonEmployeeToRiskAssessment(nonEmployee, user);
                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void DetachNonEmployeeFromRiskAssessment(DetachNonEmployeeFromRiskAssessmentRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);

                DetachRequestedNonEmployees(request, riskAssessment, user);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void AttachEmployeeToRiskAssessment(AttachEmployeeRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);

                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                riskAssessment.AttachEmployeeToRiskAssessment(employee, user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void DetachEmployeeFromRiskAssessment(DetachEmployeeRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                var employees = _employeeRepository.GetByIds(request.EmployeeIds);

                riskAssessment.DetachEmployeesFromRiskAssessment(employees, user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }

        private void DetachRequestedNonEmployees(DetachNonEmployeeFromRiskAssessmentRequest request, RiskAssessment riskAssessment, UserForAuditing user)
        {
            foreach (var nonEmployeesToDetachId in request.NonEmployeesToDetachIds)
            {
                var nonEmployee = _nonEmployeeRepository.GetByIdAndCompanyId(nonEmployeesToDetachId, request.CompanyId);

                riskAssessment.DetachNonEmployeeFromRiskAssessment(nonEmployee, user);
            }
            _riskAssessmentRepository.SaveOrUpdate(riskAssessment);
        }


        private IEnumerable<RiskAssessmentDocument> CreateRiskAssessmentDocumentsToAttach(AttachDocumentsToRiskAssessmentRequest request, UserForAuditing user)
        {
            var documents = (from createDocumentRequest in request.DocumentsToAttach
                             let documentType = _documentTypeRepository.GetById((long)createDocumentRequest.DocumentType)
                             select RiskAssessmentDocument.Create(new CreateDocumentParameters
                             {
                                 ClientId = request.CompanyId,
                                 DocumentLibraryId =
                                     createDocumentRequest.DocumentLibraryId,
                                 Filename = createDocumentRequest.Filename,
                                 Extension = createDocumentRequest.Extension,
                                 FilesizeByte = createDocumentRequest.FilesizeByte,
                                 Description = createDocumentRequest.Description,
                                 DocumentType = documentType,
                                 CreatedOn = DateTime.Now,
                                 CreatedBy = user
                             })).ToList();
            return documents;
        }
    }
}