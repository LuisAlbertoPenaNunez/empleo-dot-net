using System;
using Api.Contract;
using System.Threading.Tasks;
using System.Collections.Generic;
using APIs;
using System.Threading;

namespace Api
{
	public class JobsApiService : IJobsApiService
	{
		IProxy _proxy;

		public JobsApiService (IProxy proxy)
		{
			_proxy = proxy;
		}

		public async Task<JobCardListResponse> GetCardJobs (int page = 1, int pageSize = 25, CancellationToken token = default(CancellationToken))
		{
			var result = await _proxy.Get<JobCardListResponse>(Constants.JobsEndpoint, Method.GET, new List<Parameter>
				{
					new Parameter
					{
						Property = "page",
						Value = page.ToString()
					},
					new Parameter
					{
						Property = "pageSize",
						Value = pageSize.ToString()
					}
				}, token);

			return result.Result;
		}

		public async Task<JobDetailResponse> GetJobDetailForId (string id, CancellationToken token)
		{
			throw new Exception();
		}
	}
}

