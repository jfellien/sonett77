using System;
using Nancy;

namespace SuperDuperWebClient
{
	public class Index : NancyModule
	{
		public Index ()
		{
			Get ["/"] = parameters => {
				return View ["index"];
			};

			Get ["/index"] = parameters => {
				return View ["index"];
			};
		}
	}
}

