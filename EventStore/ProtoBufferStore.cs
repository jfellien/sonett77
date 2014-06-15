using System;
using System.Collections.Generic;
using System.IO;
using EventStore.Contracts;
using System.Linq;
using fastJSON;

namespace EventStore
{

	public class ProtoBufferStore : IEventStore
	{
		string _destination;

		public ProtoBufferStore (string destination)
		{
			_destination = destination;
		}

		public void Store (IAmAnEvent @event)
		{
			var bufferDestination = Path.Combine (_destination, @event.EntityId + ".bin");
			var eventBag = @event.ToEventBag ();

			using (var entityBuffer = File.OpenWrite (bufferDestination)) {

				ProtoBuf.Serializer.Serialize<EventBag> (entityBuffer, eventBag);
			}
		}

		public IEnumerable<IAmAnEvent> RetrieveBy (string entityId)
		{
			throw new NotImplementedException ();
		}

		public IEnumerable<string> RetrieveEntities ()
		{
			throw new NotImplementedException ();
		}

	}
}
