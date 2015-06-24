using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessSafe.Data.Repository.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.Mappers;
using EvaluationChecklist.Models;
using StructureMap;
using log4net;

namespace EvaluationChecklist.Controllers
{
    public class IndustryController : ApiController
    {

        private readonly IIndustryRepository _industryRepository;

        public IndustryController()
        {
            _industryRepository = ObjectFactory.GetInstance<IIndustryRepository>();
        }

        /// <summary>
        /// Returns a list of industires and their associated questions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<IndustryViewModel> Get()
        {
            try
            {

                var industries = _industryRepository.GetAll()
                    .Select(x => new IndustryViewModel()
                                     {
                                         Id = x.Id,
                                         Title = x.Name,
                                         Questions = x.Questions.Select(q => q.Question).Map()
                                     }).ToList();

                return industries;
            }

            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(IndustryController)).Error(ex);
                throw;
            }

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
