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
		public static IAmAnEvent ToEvent (this Happening source)
		{
			if (source.EventName.Equals ("start-with-school"))
				return new StartWithSchool {
					EntityId = source.EntityId,
					At = source.Year
				};

			if (source.EventName.Equals ("get-a-graduation"))
				return new GetGraduation {
					EntityId = source.EntityId,
					At = source.Year
				};

			if (source.EventName.Equals ("becomes-a-baby"))
				return new BecomesBaby {
					EntityId = source.EntityId,
					At = source.Year
				};

			if (source.EventName.Equals ("marries"))
				return new Marries {
					EntityId = source.EntityId,
					At = source.Year
				};

			return null as IAmAnEvent;
		}
	}
}
