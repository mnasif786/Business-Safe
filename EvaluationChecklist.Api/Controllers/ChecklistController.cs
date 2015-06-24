using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Data.Repository.SafeCheck;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.Messages.Emails.Commands;
using EvaluationChecklist.ClientDetails;
using EvaluationChecklist.Helpers;
using EvaluationChecklist.Mappers;
using EvaluationChecklist.Models;
using System.Linq;
using NServiceBus;
using log4net;
using IQuestionRepository = BusinessSafe.Domain.RepositoryContracts.SafeCheck.IQuestionRepository;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;
using CompanyDetails = EvaluationChecklist.ClientDetails.Models.CompanyDetails;
using Question = BusinessSafe.Domain.Entities.SafeCheck.Question;

namespace EvaluationChecklist.Controllers
{

    public class ChecklistController : ApiController
    {
        private readonly ICheckListRepository _checklistRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionResponseRepository _questionResponseRepository;
        private readonly IClientDetailsService _clientDetailsService;
        private readonly IGetChecklistsQuery _getChecklistsQuery;
        private readonly IChecklistQuestionRepository _checklistQuestionRepository;
        private readonly IEmployeeRepository _employeeRespository;
        private readonly IImpressionTypeRepository _impressionRespository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITimescaleRepository _timescaleRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IChecklistPdfCreator _checklistPdfCreator;
        private readonly IClientDocumentationChecklistPdfWriter _clientDocumentationChecklistPdfWriter;
        private readonly IPeninsulaLog _log;
        private readonly IQaAdvisorRepository _qaAdvisorRepository;
        private readonly IIndustryRepository _industryRepository;
        private readonly IBus _bus;

        private static object _lock = new object();

		public ChecklistController(IDependencyFactory dependencyFactory)
		{
			_checklistRepository = dependencyFactory.GetInstance<ICheckListRepository>();
			_questionRepository = dependencyFactory.GetInstance<IQuestionRepository>();
			_questionResponseRepository = dependencyFactory.GetInstance<IQuestionResponseRepository>();
			_clientDetailsService = dependencyFactory.GetInstance<IClientDetailsService>();
			_getChecklistsQuery = dependencyFactory.GetInstance<IGetChecklistsQuery>();
			_checklistQuestionRepository = dependencyFactory.GetInstance<IChecklistQuestionRepository>();
			_employeeRespository = dependencyFactory.GetInstance<IEmployeeRepository>();
			_impressionRespository = dependencyFactory.GetInstance<IImpressionTypeRepository>();
			_categoryRepository = dependencyFactory.GetInstance<ICategoryRepository>();
			_timescaleRepository = dependencyFactory.GetInstance<ITimescaleRepository>();
			_userForAuditingRepository = dependencyFactory.GetInstance<IUserForAuditingRepository>();
			_siteRepository = dependencyFactory.GetInstance<ISiteRepository>();
			_checklistPdfCreator = dependencyFactory.GetInstance<IChecklistPdfCreator>();
			_clientDocumentationChecklistPdfWriter =
				dependencyFactory.GetInstance<IClientDocumentationChecklistPdfWriter>();
			_log = dependencyFactory.GetInstance<IPeninsulaLog>();
			_qaAdvisorRepository = dependencyFactory.GetInstance<IQaAdvisorRepository>();
            _industryRepository = dependencyFactory.GetInstance<IIndustryRepository>();
		    _bus = dependencyFactory.GetInstance<IBus>();
		}

		/// <summary>
		/// Returns a checklist given an Id. Will return 404 error if not found.
		/// </summary>
		/// <param name="id">integer</param>
		/// <returns></returns>
		public ChecklistViewModel GetChecklist(Guid id)
		{
             try
             {
                 _log.Add(new[] {id});

                 var checklist = _checklistRepository.GetById(id);

                 if (checklist == null)
                 {
                     throw new HttpResponseException(HttpStatusCode.NotFound);
                 }

                 var model = new ChecklistViewModel();
                 model.Id = checklist.Id;
                 model.ClientId = checklist.ClientId;
                 model.SiteId = checklist.SiteId.HasValue ? (int?) checklist.SiteId.Value : null;
                 model.Site = checklist.SiteId.HasValue ? new SiteViewModel() {Id = checklist.SiteId.Value} : null;
                 model.CoveringLetterContent = checklist.CoveringLetterContent;
                 model.LastModifiedOn = checklist.LastModifiedOn;

                 model.CreatedOn = checklist.CreatedOn;
                 model.Status = checklist.Status;
                 model.ImmediateRiskNotifications = ImmediateRiskNotificationViewModelMapper.Map(checklist.ImmediateRiskNotifications);

                 model.SiteVisit = new SiteVisitViewModel()
                                       {
                                           VisitBy = string.IsNullOrEmpty(checklist.VisitBy) ? string.Empty : checklist.VisitBy,
                                           VisitType = checklist.VisitType,
                                           VisitDate = checklist.VisitDate.HasValue ? checklist.VisitDate.Value.ToShortDateString() : string.Empty,
                                           AreasNotVisited = checklist.AreasNotVisited,
                                           AreasVisited = checklist.AreasVisited,
                                           EmailAddress = checklist.EmailAddress,
                                           PersonSeen = new PersonSeenViewModel()
                                                            {
                                                                JobTitle = checklist.PersonSeenJobTitle, 
                                                                Name = checklist.PersonSeenName, 
                                                                Salutation = checklist.PersonSeenSalutation, 
                                                                Id = checklist.PersonSeenId
                                                            },
                                       };

                 if (checklist.ImpressionType != null)
                 {
                     model.SiteVisit.SelectedImpressionType = new ImpressionTypeViewModel()
                                                                  {
                                                                      Id = checklist.ImpressionType.Id,
                                                                      Comments = checklist.ImpressionType.Comments,
                                                                      Title = checklist.ImpressionType.Title
                                                                  };
                 }

                 model.Categories = checklist.Questions.DistinctListOfCategories();

                 model.Questions = checklist.Questions.Where(q => !q.Deleted).Select(q =>
                                                                                         {
                                                                                             var correspondingAnswer = checklist.Answers.FirstOrDefault(a => a.Question.Id == q.Question.Id);
                                                                                             return new QuestionAnswerViewModel()
                                                                                                        {
                                                                                                            Question = q.Question.Map(),
                                                                                                            Answer = Map(correspondingAnswer, q.Question),
                                                                                                            QuestionNumber = (q.QuestionNumber == null) ? 0 : q.QuestionNumber.Value,
                                                                                                            CategoryNumber = (q.CategoryNumber == null) ? 0 : q.CategoryNumber.Value
                                                                                                        };
                                                                                         }).Distinct().ToList();

                 model.CreatedOn = checklist.ChecklistCreatedOn;
                 model.CreatedBy = checklist.ChecklistCreatedBy;
                 model.CompletedOn = checklist.ChecklistCompletedOn;
                 model.CompletedBy = checklist.ChecklistCompletedBy;
                 model.SubmittedOn = checklist.ChecklistSubmittedOn;
                 model.SubmittedBy = checklist.ChecklistSubmittedBy;
                 model.IndustryId = checklist.Industry != null ? checklist.Industry.Id : (Guid?) null;
                 model.QAComments = checklist.QAComments;
                 model.EmailReportToPerson = checklist.EmailReportToPerson;
                 model.EmailReportToOthers = checklist.EmailReportToOthers;
                 model.PostReport = checklist.PostReport;
                 model.OtherEmailAddresses = checklist.OtherEmailAddresses;
				 model.UpdatesRequired = checklist.UpdatesRequired;

                return model;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                throw;
            }
        }

        private static AnswerViewModel Map(ChecklistAnswer answer, Question question)
        {
            if (answer != null)
            {
                AnswerViewModel model = new AnswerViewModel()
                {
                    SupportingEvidence = answer.SupportingEvidence,
                    ActionRequired = answer.ActionRequired,
                    SelectedResponseId = answer.Response != null ? (Guid?) answer.Response.Id : null,
                    QuestionId = answer.Question.Id,
                    GuidanceNotes = answer.GuidanceNotes,
                    Timescale =
                        answer.Timescale == null
                            ? null
                            : new TimescaleViewModel() {Id = answer.Timescale.Id, Name = answer.Timescale.Name},
                    AssignedTo = null,
                    QaComments = answer.QaComments,
                    QaSignedOffBy = answer.QaSignedOffBy,
                    ReportLetterStatement = answer.Response != null ? answer.Response.ReportLetterStatement : ""
                };

                if (answer.AssignedTo != null)
                {
                    model.AssignedTo = new AssignedToViewModel()
                    {
                        Id = (Guid?) answer.AssignedTo.Id,
                        NonEmployeeName = answer.AssignedTo.FullName
                    };
                }
                else if (!string.IsNullOrEmpty(answer.EmployeeNotListed))
                {
                    model.AssignedTo = new AssignedToViewModel()
                    {
                        Id = (Guid?) Guid.Empty,
                        NonEmployeeName = answer.EmployeeNotListed
                    };
                }



                return model;
            }
            else
            {
                return new AnswerViewModel()
                {
                    QuestionId = question.Id
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteChecklist(Guid id)
        {
            try
            {
                var checklist = _checklistRepository.GetById(id);

                if (checklist != null)
                {
                    var user = _userForAuditingRepository.GetSystemUser();
                    checklist.MarkForDelete(user);
                    _checklistRepository.Save(checklist);
                }

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Creates/Updates the checklist. The id is a Guid. To create a new checklist generate a new Guid. 
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostChecklist(Guid id, ChecklistViewModel model)
        {
            lock (_lock)
            {
                try
                {
                    LogManager.GetLogger(typeof (ChecklistController))
                        .Info("Starting PostChecklist on Thread " + Thread.CurrentThread.ManagedThreadId);
                    _log.Add(model);

                    if (!ModelState.IsValid || model == null)
                    {
                        var errorList = ModelState.Keys.ToList()
                            .Select(k => new {PropertyName = k, Errors = ModelState[k].Errors})
                            .Where(e => e.Errors.Count > 0);
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, errorList));
                    }

                    if (HttpContext.Current != null)
                    {
                        model.ContentPath = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme,
                            HttpContext.Current.Request.Url.Authority, "/content");
                    }

                    var user = _userForAuditingRepository.GetSystemUser();

                    var checklist = _checklistRepository.GetById(id);

                    var createUpdateChecklistParameters = CreateUpdateChecklistParameters(model, user);

                    if (checklist == null)
                    {
                        checklist = Checklist.Create(createUpdateChecklistParameters);
                    }
                    else
                    {
                        checklist.UpdateChecklistDetails(createUpdateChecklistParameters);
                    }


                    UpdateQuestions(checklist, model, user);

                    //add answers.
                    var allResponses =
                        checklist.Questions.SelectMany(checklistQuestion => checklistQuestion.Question.PossibleResponses)
                            .Distinct()
                            .ToList();
                    var allQuestions =
                        checklist.Questions.Select(checklistQuestion => checklistQuestion.Question).Distinct().ToList();

                    var checklistAnswers = ChecklistAnswers(model, allResponses, allQuestions, user);

                    checklist.UpdateAnswers(checklistAnswers, user);

                    if (checklist.ActionPlan != null)
                    {
                        checklist.ActionPlan.CreateActions(checklistAnswers);
                        // Convert the IRN's saved on the checklist to actions for BSO
                        checklist.ActionPlan.CreateImmediateRiskNotifications();
                    }

                    //Save.
                    _checklistRepository.SaveOrUpdate(checklist);
                    _categoryRepository.Flush();

                    if (model.Submit)
                    {
                        _checklistPdfCreator.ChecklistViewModel = model;
                        //var filename = _checklistPdfCreator.GetFileName(model.Site.Address1, model.Site.Postcode, model.SiteVisit.VisitDate);
                        var pdfBytes = _checklistPdfCreator.GetBytes();
                        _clientDocumentationChecklistPdfWriter.WriteToClientDocumentation(
                            checklist.Title.Replace("/", "") + ".pdf", pdfBytes, model.ClientId.Value);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger(typeof (ChecklistController))
                        .Error("Error on thread" + Thread.CurrentThread.ManagedThreadId, ex);
                    LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                    throw;
                }
                finally
                {
                    LogManager.GetLogger(typeof (ChecklistController))
                        .Info("Exiting PostChecklist on thread " + Thread.CurrentThread.ManagedThreadId);
                }
            }
        }

        private List<ChecklistAnswer> ChecklistAnswers(ChecklistViewModel model, List<QuestionResponse> allResponses,
            List<Question> allQuestions, UserForAuditing user)
        {
            var checklistAnswers = new List<ChecklistAnswer>();

            foreach (var questionAnswerViewModel in model.Questions.Where(x => x.Answer != null))
            {
                var checklistAnswer = new ChecklistAnswer();
                checklistAnswer.SupportingEvidence = questionAnswerViewModel.Answer.SupportingEvidence;
                checklistAnswer.ActionRequired = questionAnswerViewModel.Answer.ActionRequired;
                checklistAnswer.GuidanceNotes = questionAnswerViewModel.Answer.GuidanceNotes;

                checklistAnswer.Timescale = questionAnswerViewModel.Answer.Timescale != null
                    ? _timescaleRepository.GetById(questionAnswerViewModel.Answer.Timescale.Id)
                    : null;

                checklistAnswer.Response =
                    allResponses.SingleOrDefault(
                        response => response.Id == questionAnswerViewModel.Answer.SelectedResponseId);
                checklistAnswer.Question =
                    allQuestions.Single(question => question.Id == questionAnswerViewModel.Question.Id);

                checklistAnswer.AssignedTo = (questionAnswerViewModel.Answer.AssignedTo != null) &&
                                             (questionAnswerViewModel.Answer.AssignedTo.Id.HasValue) &&
                                             (questionAnswerViewModel.Answer.AssignedTo.Id.Value != Guid.Empty)
                    ? _employeeRespository.GetById((questionAnswerViewModel.Answer.AssignedTo.Id.GetValueOrDefault()))
                    : null;

                checklistAnswer.EmployeeNotListed = (questionAnswerViewModel.Answer.AssignedTo != null) &&
                                                    (questionAnswerViewModel.Answer.AssignedTo.Id.HasValue) &&
                                                    (questionAnswerViewModel.Answer.AssignedTo.Id.Value == Guid.Empty)
                    ? questionAnswerViewModel.Answer.AssignedTo.NonEmployeeName
                    : null;

                checklistAnswer.QaComments = questionAnswerViewModel.Answer.QaComments;

                if (questionAnswerViewModel.Answer.QaSignedOffBy != null)
                {
                    checklistAnswer.QaSignedOffBy = questionAnswerViewModel.Answer.QaSignedOffBy + "_" +
                                                    DateTime.Now.Date;
                }

                checklistAnswer.CreatedBy = user;
                checklistAnswer.CreatedOn = DateTime.Now;

                checklistAnswer.LastModifiedBy = user;
                checklistAnswer.LastModifiedOn = DateTime.Now;

                checklistAnswers.Add(checklistAnswer);
            }
            return checklistAnswers;
        }

        private CreateUpdateChecklistParameters CreateUpdateChecklistParameters(ChecklistViewModel model,
            UserForAuditing user)
        {
            var createUpdateChecklistParameters = new CreateUpdateChecklistParameters();
            createUpdateChecklistParameters.Id = model.Id;
            createUpdateChecklistParameters.ClientId = model.ClientId;
            createUpdateChecklistParameters.SiteId = model.SiteId;
            createUpdateChecklistParameters.CoveringLetterContent = model.CoveringLetterContent;
            createUpdateChecklistParameters.Status = model.Status;
            createUpdateChecklistParameters.Submit = model.Submit;
            createUpdateChecklistParameters.User = user;
            createUpdateChecklistParameters.Site = _siteRepository.GetByPeninsulaSiteId((long) model.SiteId);

            createUpdateChecklistParameters.CreatedOn = model.CreatedOn;
            createUpdateChecklistParameters.CreatedBy = model.CreatedBy;
            createUpdateChecklistParameters.CompletedOn = model.CompletedOn;
            createUpdateChecklistParameters.CompletedBy = model.CompletedBy;
            createUpdateChecklistParameters.SubmittedOn = model.SubmittedOn;
            createUpdateChecklistParameters.SubmittedBy = model.SubmittedBy;
            createUpdateChecklistParameters.PostedBy = model.PostedBy;
            createUpdateChecklistParameters.Industry = model.IndustryId.HasValue ? _industryRepository.LoadById(model.IndustryId.Value): null;
		    createUpdateChecklistParameters.QAComments = model.QAComments;
		    createUpdateChecklistParameters.LastModifiedOn = model.LastModifiedOn;
            createUpdateChecklistParameters.EmailReportToPerson = model.EmailReportToPerson.HasValue && model.EmailReportToPerson.Value;
            createUpdateChecklistParameters.EmailReportToOthers = model.EmailReportToOthers.HasValue && model.EmailReportToOthers.Value;
            createUpdateChecklistParameters.PostReport = model.PostReport.HasValue && model.PostReport.Value;
		    createUpdateChecklistParameters.OtherEmailAddresses = model.OtherEmailAddresses;

            if (model.SiteVisit != null)
            {
                createUpdateChecklistParameters.VisitDate = model.SiteVisit.VisitDate;
                createUpdateChecklistParameters.VisitBy = model.SiteVisit.VisitBy;
                createUpdateChecklistParameters.VisitType = model.SiteVisit.VisitType;

                if (model.SiteVisit.PersonSeen != null)
                {
                    createUpdateChecklistParameters.PersonSeenJobTitle = model.SiteVisit.PersonSeen.JobTitle;
                    createUpdateChecklistParameters.PersonSeenName = model.SiteVisit.PersonSeen.Name;
                    createUpdateChecklistParameters.PersonSeenSalutation = model.SiteVisit.PersonSeen.Salutation;
                    createUpdateChecklistParameters.PersonSeenId = model.SiteVisit.PersonSeen.Id;
                }

                createUpdateChecklistParameters.AreasNotVisited = model.SiteVisit.AreasNotVisited;
                createUpdateChecklistParameters.AreasVisited = model.SiteVisit.AreasVisited;
                createUpdateChecklistParameters.EmailAddress = model.SiteVisit.EmailAddress;


                createUpdateChecklistParameters.ImpressionType = (model.SiteVisit.SelectedImpressionType == null ||
                                                                  model.SiteVisit.SelectedImpressionType.Id ==
                                                                  Guid.Empty)
                    ? null
                    : _impressionRespository.GetById(
                        model.SiteVisit.SelectedImpressionType.Id);

                createUpdateChecklistParameters.SiteAddress1 = model.Site == null ? String.Empty : model.Site.Address1;
                createUpdateChecklistParameters.SiteAddressPostcode = model.Site == null
                    ? String.Empty
                    : model.Site.Postcode;
            }

            foreach (var immediateRiskNotificationViewModel in model.ImmediateRiskNotifications)
            {
                // This part updates the IRN's for storage on the checklist, retrieved in SafeCheck when not submitted
                createUpdateChecklistParameters.ImmediateRiskNotifications.Add(new AddImmediateRiskNotificationParameters
                {
                    Id = immediateRiskNotificationViewModel.Id,
                    Reference = immediateRiskNotificationViewModel.Reference,
                    Title = immediateRiskNotificationViewModel.Title,
                    SignificantHazardIdentified = immediateRiskNotificationViewModel.SignificantHazard,
                    RecommendedImmediateAction = immediateRiskNotificationViewModel.RecommendedImmediate
                });
            }
            return createUpdateChecklistParameters;
        }

        private void UpdateQuestions(Checklist checklist, ChecklistViewModel model, UserForAuditing systemUser)
        {
            var newQuestionAnswerViewModels = model.Questions
                .Where(
                    questionAnswerViewModel =>
                        !checklist.Questions.Any(
                            checklistQuestion => checklistQuestion.Question.Id == questionAnswerViewModel.Question.Id));

            var existingQuestions = model.Questions
                .Where(
                    questionAnswerViewModel =>
                        checklist.Questions.Any(
                            checklistQuestion => checklistQuestion.Question.Id == questionAnswerViewModel.Question.Id));

            var removedQuestions = checklist.Questions
                .Where(
                    checklistQuestion =>
                        !model.Questions.Any(qaVwModel => qaVwModel.Question.Id == checklistQuestion.Question.Id));

            newQuestionAnswerViewModels.ToList()
                .ForEach(
                    questionAnswerViewModel => AddQuestionToChecklist(checklist, questionAnswerViewModel, systemUser));

            existingQuestions.ToList()
                .ForEach(
                    questionAnswerViewModel => AddQuestionToChecklist(checklist, questionAnswerViewModel, systemUser));

            RemoveQuestions(removedQuestions, systemUser);
        }

        private void RemoveQuestions(IEnumerable<ChecklistQuestion> removedQuestions, UserForAuditing systemUser)
        {
            foreach (ChecklistQuestion q in removedQuestions)
            {
                q.MarkForDelete(systemUser);
            }
        }

        private void AddQuestionToChecklist(Checklist checklist, QuestionAnswerViewModel questionAnswerViewModel,
            UserForAuditing systemUser)
        {
            var question = _questionRepository.GetById(questionAnswerViewModel.Question.Id);

            if (question == null)
            {
                // adding a bespoke question
                var category = _categoryRepository.GetById(questionAnswerViewModel.Question.CategoryId);

                if (category == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest,
                        String.Format("Unknown Category - Id {0}",
                            questionAnswerViewModel.
                                Question.CategoryId.
                                ToString())));
                }

                question = Question.Create(
                    questionAnswerViewModel.Question.Id,
                    questionAnswerViewModel.Question.Text,
                    category,
                    true, // is custom (bespoke) question
                    questionAnswerViewModel.Question.SpecificToClientId,
                    systemUser);

            }
            else if (question.CustomQuestion && question.Title != questionAnswerViewModel.Question.Text)
            {
                // update bespoke question text
                question.Title = questionAnswerViewModel.Question.Text;
            }

            if (questionAnswerViewModel.Question.PossibleResponses != null)
            {
                foreach (var possibleResponse in questionAnswerViewModel.Question.PossibleResponses)
                {
                    var questionResponse = _questionResponseRepository.GetById(possibleResponse.Id);
                    if (questionResponse == null)
                    {
                        questionResponse = new QuestionResponse
                        {
                            Id = possibleResponse.Id,
                            Title = possibleResponse.Title,
                            SupportingEvidence = possibleResponse.SupportingEvidence,
                            ActionRequired = possibleResponse.ActionRequired,
                            ResponseType = possibleResponse.ResponseType,
                            GuidanceNotes = possibleResponse.GuidanceNotes,
                            CreatedBy = systemUser,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = systemUser,
                            LastModifiedOn = DateTime.Now,
                            ReportLetterStatement = possibleResponse.ReportLetterStatement
                        };
                    }
                    else
                    {
                        questionResponse.ReportLetterStatement = possibleResponse.ReportLetterStatement;
                    }

                    question.AddQuestionResponse(questionResponse);
                }
            }


            ChecklistQuestion checklistQuestion = new ChecklistQuestion()
            {
                Id = Guid.NewGuid(),
                Checklist = checklist,
                Question = question,
                CreatedBy = systemUser,
                CreatedOn = DateTime.Now,
                CategoryNumber = questionAnswerViewModel.CategoryNumber,
                QuestionNumber = questionAnswerViewModel.QuestionNumber
            };

            checklist.UpdateQuestion(checklistQuestion, systemUser);

        }

        /// <summary>
        /// Returns a list of checklist for a given clientId
        /// </summary>
        /// <param name="clientId">integer</param>
        /// <returns></returns>
        [HttpGet]
        public List<ChecklistIndexViewModel> GetByClientId(int clientId)
        {
            var clientSites = _clientDetailsService.GetSites(clientId);
            var checklists = _checklistRepository.GetByClientId(clientId);
            return checklists.Select(x =>
            {
                var visitSite = clientSites.Any(site => site.Id == x.SiteId)
                    ? clientSites.First(site => site.Id == x.SiteId)
                    : null;

                return new ChecklistIndexViewModel()
                {
                    Id = x.Id,
                    Title = "Title",
                    VisitDate = x.VisitDate,
                    VisitBy = x.VisitBy,
                    CreatedOn = x.CreatedOn,
                    Site = visitSite != null
                        ? new SiteViewModel()
                        {
                            Address1 = visitSite.Address1,
                            Postcode = visitSite.Postcode,
                            SiteName = visitSite.SiteName
                        }
                        : null
                    //,ImmediateRiskNotifications = ImmediateRiskNotificationViewModelMapper.Map(x.ImmediateRiskNotifications)
                };
            }).ToList();

        }

        /// <summary>
        /// Query the submitted checklists
        /// </summary>
        /// <param name="clientAccountNumber"></param>
        /// <param name="checklistCreatedBy"></param>
        /// <param name="visitDate"></param>
        /// <param name="status"></param>
        /// <param name="includeDeleted"/>
        /// <returns></returns>
        [HttpGet]
        public List<ChecklistIndexViewModel> Query(string clientAccountNumber, string checklistCreatedBy,
            string visitDate, string status, bool includeDeleted)
        {
            try
            {
                int? clientDetailId = null;
                if (!string.IsNullOrEmpty(clientAccountNumber))
                {
                    //if client not found then return empty list
                    var clientDetail = _clientDetailsService.GetByClientAccountNumber(clientAccountNumber);

                    if (clientDetail == null || clientDetail.Id == -1)
                    {
                        return new List<ChecklistIndexViewModel>();
                    }

                    clientDetailId = (int?) clientDetail.Id;
                }


                var qaAdvisorToSearchFor = checklistCreatedBy != null
                                               ? _qaAdvisorRepository.GetByFullname(checklistCreatedBy)
                                               : null;

                var checklists = _checklistRepository.Search(clientDetailId, checklistCreatedBy, visitDate, status,
                    includeDeleted, qaAdvisorToSearchFor != null ? qaAdvisorToSearchFor.Id : (Guid?) null);

                if (checklists != null)
                {

                    //store the site details and customer details retrieved from the Client Details Service so that we don't have to make multiple requests for the same ids
                    var visitSites = new List<SiteAddressResponse>();
                    var clientDetails = new List<CompanyDetails>();

                    return checklists.Select(x =>
                    {
                        var can = GetClientAccountNumber(clientDetails, x);
                        var visitSite = GetVisitSiteAddress(visitSites, x);
                        return x.Map(visitSite, can, x.QaAdvisor);
                    }).ToList();
                }

                return new List<ChecklistIndexViewModel>();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                throw;
            }
        }

        /// <summary>
        /// optimised version of query checklist. Requires testing ALP
        /// </summary>
        /// <param name="clientAccountNumber"></param>
        /// <param name="checklistCreatedBy"></param>
        /// <param name="visitDate"></param>
        /// <param name="status"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ChecklistIndexViewModel> QueryUsingQueryObject(string clientAccountNumber, string checklistCreatedBy,
            string visitDate, string status, bool includeDeleted)
        {
            try
            {
                DateTime validVisitDate;
                DateTime? nullableValidVisitDate = null;
                if (!string.IsNullOrEmpty(visitDate) && DateTime.TryParse(visitDate, out validVisitDate))
                {
                    nullableValidVisitDate = validVisitDate;
                }


                var clientDetail = _clientDetailsService.GetByClientAccountNumber(clientAccountNumber);
                var clientDetailId = (clientDetail != null && clientDetail.Id != -1) ? (int?) clientDetail.Id : null;
                var checklists = _getChecklistsQuery.Execute(clientDetailId, checklistCreatedBy, nullableValidVisitDate,
                    status, null);


                if (checklists != null)
                {

                    //store the site details and customer details retrieved from the Client Details Service so that we don't have to make multiple requests for the same ids
                    var visitSites = new List<SiteAddressResponse>();
                    var clientDetails = new List<CompanyDetails>();

                    return checklists.Select(x =>
                    {
                        var can = GetClientAccountNumber(clientDetails, x.ClientId);
                        var visitSite = GetVisitSiteAddress(visitSites, x.ClientId, x.SiteId);
                        var qaAdvisor = (x.QaAdvisorId.HasValue) ? _qaAdvisorRepository.GetById(x.QaAdvisorId.Value) : null;

                        var checklistViewModel = new ChecklistIndexViewModel()
                        {
                            Id = x.Id,
                            Title = "Title",
                            VisitDate = x.VisitDate,
                            VisitBy = x.VisitBy,
                            CreatedOn = x.CreatedOn,
                            CreatedBy = x.CreatedBy,
                            Site = new SiteViewModel() {Id = x.SiteId.HasValue ? x.SiteId.Value : -1},
                            Status = x.Status,
                            CAN = can
                            ,
                            Deleted = x.Deleted
                            ,
                            HasQaComments = x.HasQaComments
                            ,
                            QaAdvisor = qaAdvisor != null
                                ? new QaAdvisorViewModel()
                                {
                                    Id = qaAdvisor.Id,
                                    Forename = qaAdvisor.Forename,
                                    Email = qaAdvisor.Email,
                                    Fullname = qaAdvisor.Forename + ' ' + qaAdvisor.Surname,
                                    Initials = qaAdvisor.Forename + ' ' + qaAdvisor.Surname.Substring(0, 1)
                                }
                                : null
                            ,
                            ClientName = ""
                        };
                        checklistViewModel.Site.Id = visitSite != null ? (int) visitSite.Id : -1;
                        checklistViewModel.Site.Postcode = visitSite != null ? visitSite.Postcode : "";
                        checklistViewModel.Site.SiteName = visitSite != null ? visitSite.SiteName : "";
                        checklistViewModel.Site.Address1 = visitSite != null ? visitSite.Address1 : "";
                        checklistViewModel.Site.Address2 = visitSite != null ? visitSite.Address2 : "";
                        checklistViewModel.Site.Address3 = visitSite != null ? visitSite.Address3 : "";
                        checklistViewModel.Site.Address4 = visitSite != null ? visitSite.Address4 : "";

                        return checklistViewModel;
                    }).ToList();
                }

                return new List<ChecklistIndexViewModel>();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                throw;
            }
        }

        private SiteAddressResponse GetVisitSiteAddress(List<SiteAddressResponse> visitSites, Checklist checklist)
        {
            return GetVisitSiteAddress(visitSites, checklist.ClientId, checklist.SiteId);
        }

        private SiteAddressResponse GetVisitSiteAddress(List<SiteAddressResponse> visitSites, int? clientId, int? siteId)
        {
            SiteAddressResponse visitSite = null;
            if (clientId.HasValue && clientId.Value != -1 && siteId.HasValue && siteId.Value != -1)
            {
                if (!visitSites.Any(sites => sites.Id == siteId))
                {
                    visitSite = _clientDetailsService.GetSite(clientId.Value, siteId.Value);
                    visitSites.Add(visitSite);
                }

                visitSite = visitSites.FirstOrDefault(sites => sites.Id == siteId.Value);
            }
            return visitSite;
        }

        private string GetClientAccountNumber(List<CompanyDetails> clientDetails, Checklist checklist)
        {
            return GetClientAccountNumber(clientDetails, checklist.ClientId);
        }

        private string GetClientAccountNumber(List<CompanyDetails> clientDetails, int? clientId)
        {

            string can = null;
            if (clientId.HasValue && clientId.Value != -1)
            {
                if (!clientDetails.Any(clients => clients.Id == clientId.Value))
                {
                    clientDetails.Add(_clientDetailsService.Get(clientId.Value));
                }

                var cachedClientDetail = clientDetails.FirstOrDefault(clients => clients.Id == clientId.Value);
                if (cachedClientDetail != null)
                {
                    can = cachedClientDetail.CAN;
                }
            }
            return can;
        }

        /// <summary>
        /// we need this for CORS. if this is removed clients will receive a 405 method not allowed http error
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Deletes a question from a checklist with the specified Id.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DeleteChecklistQuestion(Guid checklistQuestionId)
        {
            var checklistQuestion = _checklistQuestionRepository.GetById(checklistQuestionId);
            checklistQuestion.MarkForDelete(null);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Returns the impression types from the database
        /// </summary>
        /// <returns>Returns Ienumerable list of impression Types</returns>
        public IEnumerable<ImpressionTypeViewModel> GetImpressionTypes()
        {
            return _impressionRespository.GetAll().Select(i =>
                new ImpressionTypeViewModel
                {
                    Id = i.Id,
                    Title = i.Title,
                    Comments = i.Comments
                }
                ).ToList();
        }

        /// <summary>
        /// Returns a list of users who have created a checklist
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDistinctCreatedBy()
        {
            try
            {
                return _checklistRepository.GetDistinctCreatedBy();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage AssignChecklistToQaAdvisor(Guid id, QaAdvisorViewModel model)
        {
            try
            {
                var checklist = _checklistRepository.GetById(id);

                if (checklist == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,
                        "Checklist: " + id + "Not Found."));
                }

                var qaAdvisor = _qaAdvisorRepository.GetById(model.Id);

                checklist.QaAdvisor = qaAdvisor;
                checklist.Status = "Assigned";

                //Save.
                _checklistRepository.SaveOrUpdate(checklist);
                _checklistRepository.Flush();


                var sendChecklistAssignedEmail = new SendChecklistAssignedEmail
                {
                    ChecklistId = id,
                    AssignedToId = model.Id
                };

                if (checklist.ClientId.HasValue)
                {
                    var clientDetail = _clientDetailsService.Get(checklist.ClientId.Value);

                    var site = checklist.SiteId.HasValue
                        ? _clientDetailsService.GetSite(checklist.ClientId.Value, checklist.SiteId.Value)
                        : null;

                    var postcode = site != null ? site.Postcode : "";

                    sendChecklistAssignedEmail.Can = clientDetail.CAN;
                    sendChecklistAssignedEmail.SiteName = site != null ? site.SiteName : "";
                    sendChecklistAssignedEmail.Address1 = site != null ? site.Address1 : "";
                    sendChecklistAssignedEmail.Address2 = site != null ? site.Address2 : "";
                    sendChecklistAssignedEmail.Address3 = site != null ? site.Address3 : "";
                    sendChecklistAssignedEmail.Address4 = site != null ? site.Address4 : "";
                    sendChecklistAssignedEmail.Address5 = site != null ? site.Address5 : "";
                    sendChecklistAssignedEmail.Postcode = postcode;
                }
                else
                {
                    sendChecklistAssignedEmail.Can = "Not specified";
                    sendChecklistAssignedEmail.Postcode = "Not specified";
                    ;
                }

                _bus.Send(sendChecklistAssignedEmail);
            }

            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                throw;
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage SendUpdateRequiredEmailNotification(Guid id)
        {
            try
            {
                var checklist = _checklistRepository.GetById(id);

                if (checklist == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,
                        "Checklist: " + id + "Not Found."));
                }

                var sendUpdateRequiredEmail = new SendUpdateRequiredEmail()
                {
                    ChecklistId = checklist.Id,
                };

                if (checklist.ClientId.HasValue)
                {
                    var clientDetail = _clientDetailsService.Get(checklist.ClientId.Value);

                    var site = checklist.SiteId.HasValue
                        ? _clientDetailsService.GetSite(checklist.ClientId.Value, checklist.SiteId.Value)
                        : null;

                    var postcode = site != null ? site.Postcode : "";

                    sendUpdateRequiredEmail.Can = clientDetail.CAN;
                    sendUpdateRequiredEmail.Postcode = postcode;
                }
                else
                {
                    sendUpdateRequiredEmail.Can = "Not specified";
                    sendUpdateRequiredEmail.Postcode = "Not specified";
                }

                _bus.Send(sendUpdateRequiredEmail);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (ChecklistController)).Error(ex);
                throw;
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage SendChecklistCompleteEmailNotification(Guid id)
        {
            try
            {
                var checklist = _checklistRepository.GetById(id);

                if (checklist == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound,
                        "Checklist: " + id + "Not Found."));
                }

                var sendChecklistCompleteEmail = new SendSafeCheckChecklistCompletedEmail()
                {
                     ChecklistId = checklist.Id
                };

                if (checklist.ClientId.HasValue)
                {
                    var clientDetail = _clientDetailsService.Get(checklist.ClientId.Value);

                    var site = checklist.SiteId.HasValue
                        ? _clientDetailsService.GetSite(checklist.ClientId.Value, checklist.SiteId.Value)
                        : null;

                    var postcode = site != null ? site.Postcode : "";

                    sendChecklistCompleteEmail.Can = clientDetail.CAN;
                    sendChecklistCompleteEmail.Postcode = postcode;
                }
                else
                {
                    sendChecklistCompleteEmail.Can = "Not specified";
                    sendChecklistCompleteEmail.Postcode = "Not specified";
                }

                _bus.Send(sendChecklistCompleteEmail);
            }

            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(ChecklistController)).Error(ex);
                throw;
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
