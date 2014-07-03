using System;
using System.Collections.Generic;
using System.IO;
using EventStore.Contracts;
using System.Linq;
using fastJSON;

namespace EventStore
{
	public class InMemoryStore : IEventStore
	{
		Dictionary<String,List<String>> _events;

		public InMemoryStore ()
		{
			_events = new Dictionary<String, List<String>> ();
		}

		public void Store (String entityId, object @event)
		{
			var eventBag = @event.ToEventBag ();

			var serializedEventBag = eventBag.Serialize ();

			if (_events.ContainsKey (entityId)) {
				_events [entityId].Add (serializedEventBag);
			} else {
				_events.Add (entityId, new List<String>{ serializedEventBag });
			}
		}

		public IEnumerable<object> RetrieveBy (string entityId)
		{
			var eventBagsOfEntity = _events [entityId];

			foreach (var serializedEventBag in eventBagsOfEntity) {

				var eventBag = serializedEventBag.ToEventBag ();
				var someEvent = eventBag.ExtractEvent ();

				yield return someEvent;
			}
		}

		public IEnumerable<string> RetrieveEntities ()
		{
			return _events.Keys;
		}

		public IEnumerable<object> RetrieveAllEvents ()
		{
			throw new NotImplementedException ();
		}
	}
}
