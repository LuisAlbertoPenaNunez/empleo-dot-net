using EmpleoDotNet.Core.Domain;
using EmpleoDotNet.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpleoDotNet.Core.Dto;

namespace EmpleadoDotNet.MobileAPI.Controllers
{
    public class JobOpportunityController : ApiController
    {
        private readonly IJobOpportunityRepository _jobOpportunityRepository;
        
        public JobOpportunityController(IJobOpportunityRepository jobOpportunityRepository)
        {
            _jobOpportunityRepository = jobOpportunityRepository;
        }

        [Route("cardJobs")]
        public HttpResponseMessage Get([FromUri]JobOpportunityPagingParameter parameter)
        {
            var jobs = _jobOpportunityRepository.GetAllJobOpportunitiesPagedByFilters(parameter);

            if (jobs == null || jobs.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, jobs);
        }

        [Route("cardJobs/detail")]
        public HttpResponseMessage Get(string id)
        {
            var specifiedId = GetIdFromTitle(id);

            var jobs = _jobOpportunityRepository.GetJobOpportunityById(specifiedId);

            return jobs == null ? Request.CreateResponse(HttpStatusCode.NotFound) :
                Request.CreateResponse(HttpStatusCode.OK, "Hi");
        }

        private static int GetIdFromTitle(string title)
        {
            int id;
            var url = title.Split('-');

            if (string.IsNullOrEmpty(title) || url.Length == 0 || !int.TryParse(url[0], out id))
                return 0;

            return id;
        }
    }
}
