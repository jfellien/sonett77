using System;
using System.Collections.Generic;
using System.IO;
using EventStore.Contracts;

namespace EventStore
{
	public interface IEventStore
	{
		void Store (IAmAnEvent @event);

		IEnumerable<IAmAnEvent> RetrieveBy (string entityId);

		IEnumerable<String> RetrieveEntities ();

		IEnumerable<IAmAnEvent> RetrieveAllEvents ();
	}
}

