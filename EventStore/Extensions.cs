using System;
using System.Collections.Generic;
using EventStore.Contracts;

namespace EventStore
{
	static class Extensions
	{
		public static EventBag ToEventBag (this IAmAnEvent source)
		{
			var dateTimeParameter = new fastJSON.JSONParameters ();
			dateTimeParameter.UseUTCDateTime = true;

			var serializedSource = fastJSON.JSON.ToJSON (source, dateTimeParameter);

			var eventBag = new EventBag {
				EventId = source.EntityId,
				EventDate = DateTime.Now.ToUniversalTime ().ToString ("O"),
				EventType = source.GetType ().Name,
				EventData = serializedSource
			};

			return eventBag;
		}

		public static string Serialize (this EventBag source)
		{
			var serializedEventBag = fastJSON.JSON.ToJSON (source);

			return serializedEventBag;
		}
	}
}
