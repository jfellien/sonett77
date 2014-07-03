using System;
using System.Collections.Generic;
using System.IO;
using EventStore.Contracts;

namespace EventStore
{
	public interface IEventStore
	{
		// TODO: erweitern der Storemethode, um damit das Interface zu entfernen
		void Store (IAmAnEvent @event);

		IEnumerable<IAmAnEvent> RetrieveBy (string entityId);

		IEnumerable<String> RetrieveEntities ();

		IEnumerable<IAmAnEvent> RetrieveAllEvents ();
	}
}

