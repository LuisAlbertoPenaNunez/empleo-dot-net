using System;
using System.Threading.Tasks;
using System.Net;

namespace APIs
{
	public class WebResult<TData>
	{
		public TData Result { get; set; }

		public string ContentType { get; set; }

		public string Headers { get; set; }

		public bool IsSuccess { get; set; }

		public byte[] RawBytes { get; set; }

		public string Request { get; set; }

		public Uri ResponseUri { get; set; }

		public HttpStatusCode StatusCode { get; set; }
	}

}