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
using SafeCheckSpike.Models;
using StructureMap;

namespace EvaluationChecklist.Controllers
{    
    public class CategoryController : ApiController
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController()
        {
            _categoryRepo = ObjectFactory.GetInstance<ICategoryRepository>();
        }

        /// <summary>
        /// Returns a list of categories and their associated questions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CategoryViewModel> Get()
        {
            var categories =  _categoryRepo.GetAll()
                .Select(x => new CategoryViewModel()
                                 {
                                     Id = x.Id,
                                     Title = x.Title
                                     ,
                                     Questions = x.Questions.Map()
                                     ,
                                     OrderNumber = x.OrderNumber,
                                     TabTitle = x.TabTitle
                                 }).ToList();

            return categories;
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
