using System;
using System.Threading.Tasks;
using RestSharp.Portable.HttpClient;
using System.Threading;
using RestSharp.Portable;
using System.Text;
using System.Collections.Generic;

namespace APIs
{

	public class HttpProxy : IProxy
	{
		RestClient _client;

		public HttpProxy ()
		{
			_client = new RestClient (new Uri (Constants.Endpoint));
		}

		public async Task<WebResult<TReturn>> Get<TReturn>
		(	string endpoint,
			Method method,
			List<Parameter> parameters = null,
			CancellationToken token = default(CancellationToken))
		{
			if(endpoint == null)
				throw new ArgumentNullException("endpoint");

			if(parameters != null)
				endpoint = GetFinalResult(parameters);

			var request = CreateRestRequestFor(endpoint, method);

			var result = await _client.Execute<TReturn>(request,token);
			
			return new WebResult<TReturn>
			{
				Result = result.Data,
				ContentType = result.ContentType,
				IsSuccess = result.IsSuccess,
				RawBytes = result.RawBytes,
				ResponseUri = result.ResponseUri,
				StatusCode = result.StatusCode
			};
		}

		string GetFinalResult (List<Parameter> parameters)
		{
			var result = new StringBuilder();

			foreach(Parameter parameter in parameters)
			{
				var property = parameter.Property;

				var value = parameter.Value;

				string output = $"{property}={value}&";

				result.Append(output);
			}

			var query = result.ToString();

			if(query.EndsWith("&"))
				query = query.TrimEnd('&');

			return query;
		}

		RestRequest CreateRestRequestFor(string endpoint, Method method)
		{
			RestSharp.Portable.Method requestMethod = RestSharp.Portable.Method.GET;

			switch(method)
			{
			case Method.GET:
				{
					requestMethod = RestSharp.Portable.Method.GET;
				}
				break;
			case Method.POST:
				{
					requestMethod = RestSharp.Portable.Method.POST;
				}
				break;
			case Method.PUT:
				{
					requestMethod = RestSharp.Portable.Method.PUT;
				}
				break;
			case Method.DELETE:
				{
					requestMethod = RestSharp.Portable.Method.DELETE;
				}
				break;
			}

			return new RestRequest(endpoint, requestMethod);
		}
	}
}