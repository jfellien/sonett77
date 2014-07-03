using System;
using System.Collections.Generic;
using System.IO;
using EventStore.Contracts;

namespace EventStore
{
	public interface IEventStore
	{
		void Store (String eventId, object @event);

		IEnumerable<object> RetrieveBy (string entityId);

		IEnumerable<String> RetrieveEntities ();

		IEnumerable<object> RetrieveAllEvents ();
	}
}

