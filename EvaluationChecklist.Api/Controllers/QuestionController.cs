using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessSafe.Data.Repository.SafeCheck;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.Mappers;
using EvaluationChecklist.Models;
using SafeCheckSpike.Models;
using StructureMap;
using System.Linq;
using log4net;

namespace EvaluationChecklist.Controllers
{
    public class QuestionController : ApiController
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICheckListRepository _checklistRepository;
        private readonly BusinessSafe.Domain.RepositoryContracts.IUserForAuditingRepository _userForAuditingRepository;

        public QuestionController()
        {
            _questionRepo = ObjectFactory.GetInstance<IQuestionRepository>();
            _categoryRepo = ObjectFactory.GetInstance<ICategoryRepository>();
            _checklistRepository = ObjectFactory.GetInstance<ICheckListRepository>();
            _userForAuditingRepository = ObjectFactory.GetInstance<BusinessSafe.Domain.RepositoryContracts.IUserForAuditingRepository>();
        }      

        /// <summary>
        /// Returns a list of all questions that are not specific to a client.
        /// </summary>
        /// <returns></returns>
        public List<QuestionViewModel> GetCompleteSetOfQuestions()
        {
            try
            {
                return _questionRepo.GetAllNonClientSpecific().Map();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof (QuestionController)).Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Returns a list of client specific questions. If id not found, returns a 404 error page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientQuestionViewModel GetQuestionsByClient(string id)
        {
            try
            {
                var viewModel = new ClientQuestionViewModel();

                var clientQuestion = _questionRepo.GetByClientAccountNumber(id);

                if (clientQuestion == null || !clientQuestion.Any())
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                viewModel.ClientId = clientQuestion.FirstOrDefault().ClientId;
                viewModel.ClientAccountNumber = clientQuestion.FirstOrDefault().ClientAccountNumber;
                viewModel.Questions = clientQuestion.Select(x => x.Question).Map();
                return viewModel;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(QuestionController)).Error(ex);
                throw;
            }

        }

        /// <summary>
        /// Returns a list of questions grouped by CAN
        /// </summary>
        /// <returns>List of clients and their questions</returns>
        public List<ClientQuestionViewModel> GetAllClientQuestions()
        {
            var viewModel = new List<ClientQuestionViewModel>();

            var clientQuestions = _questionRepo.GetAllByClient();

            if (clientQuestions == null || !clientQuestions.Any())
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            viewModel = clientQuestions.GroupBy(x => x.ClientId)
                .Select(y => new ClientQuestionViewModel
                                 {
                                     ClientId = y.Key,
                                     ClientAccountNumber = clientQuestions.FirstOrDefault().ClientAccountNumber,
                                     Questions = y.Select(q => q.Question).Map()
                                 })
                .ToList();

            return viewModel;
        }


        /// <summary>
        /// We need this for CORS. if this is removed clients will receive a 405 method not allowed http error.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }      
    }
}
