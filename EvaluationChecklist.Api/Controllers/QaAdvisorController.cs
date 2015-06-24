using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessSafe.Data.Repository.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.Helpers;
using EvaluationChecklist.Mappers;
using EvaluationChecklist.Models;
using RestSharp.Extensions;
using StructureMap;
using log4net;

namespace EvaluationChecklist.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class QaAdvisorController : ApiController
    {
        private readonly IQaAdvisorRepository _qaAdvisorRepository;

        /// <summary>
        /// 
        /// </summary>
        public QaAdvisorController(IDependencyFactory dependencyFactory)
        {
            _qaAdvisorRepository = dependencyFactory.GetInstance<IQaAdvisorRepository>();
        }

        /// <summary>
        /// Returns a list of industires and their associated questions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<QaAdvisorViewModel> Get()
        {
            try
            {
                return RetrieveListOfQaAdvisors();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QaAdvisorViewModel> RetrieveListOfQaAdvisors()
        {
            try
            {
                var advisors = _qaAdvisorRepository.GetAll()
                    .Where(x => x.Id != Guid.Parse("3A204FB3-1956-4EFC-BE34-89F7897570DB"))
                    .Select(x => new QaAdvisorViewModel()
                    {
                        Id = x.Id,
                        Forename = x.Forename,
                        Surname = x.Surname,
                        Fullname = x.Forename + ' ' + (x.Surname.HasValue() ? x.Surname : ""),
                        Initials = x.Forename + ' ' + (x.Surname.HasValue() ? x.Surname.Substring(0, 1) : ""),
                        Email = x.Email,
                    }).ToList();

                var emailPool = _qaAdvisorRepository.GetById(Guid.Parse("3A204FB3-1956-4EFC-BE34-89F7897570DB"));

                if (emailPool != null)
                {
                    advisors.Add(new QaAdvisorViewModel()
                    {
                        Id = emailPool.Id,
                        Forename = emailPool.Forename,
                        Surname = emailPool.Surname,
                        Fullname = emailPool.Forename + ' ' + (emailPool.Surname.HasValue() ? emailPool.Surname : ""),
                        Email = emailPool.Email,
                        Initials = emailPool.Forename + ' ' + (emailPool.Surname.HasValue() ? emailPool.Surname : "")
                    });
                }
                return advisors;
            }

            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(QaAdvisorController)).Error(ex);
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
