using System;
using System.Threading.Tasks;
using RestSharp.Portable.HttpClient;
using System.Threading;
using RestSharp.Portable;
using System.Text;
using System.Collections.Generic;

namespace APIs
{
	public interface IProxy
	{
		Task<WebResult<TData>> Get<TData>
		   (string endpoint,
			Method method,
			List<Parameter> parameters = null,
			CancellationToken token = default(CancellationToken));
	}
}