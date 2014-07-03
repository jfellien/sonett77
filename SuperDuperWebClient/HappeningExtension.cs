using System;
using EventStore;
using EventStore.Contracts;
using EventStore.Contracts.Events;
using EventStore.Contracts.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using SampleDomain;

namespace SuperDuperWebClient
{
	static class HappeningExtension
	{
		public static object ToEvent (this Happening source)
		{
			if (source.EventName.Equals ("start-with-school"))
				return new StartWithSchool {
					At = source.Year
				};

			if (source.EventName.Equals ("get-a-graduation"))
				return new GetGraduation {
					At = source.Year
				};

			if (source.EventName.Equals ("becomes-a-baby"))
				return new BecomesBaby {
					At = source.Year
				};

			if (source.EventName.Equals ("marries"))
				return new Marries {
					FamilyName = source.FamilyName,
					At = source.Year
				};

			return null;
		}
	}
}
