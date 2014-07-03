using System;
using System.Collections.Generic;
using System.IO;
using EventStore.Contracts;
using System.Linq;
using fastJSON;

namespace EventStore
{
	public class FileStore : IEventStore
	{
		string _destination;

		public FileStore (string destination)
		{
			this._destination = destination;
		}

		public void Store (String entityId, object @event)
		{
			var eventBag = @event.ToEventBag ();
			eventBag.EntityId = entityId;
			var serializedEventBag = eventBag.Serialize ();

			var eventStore = Path.Combine (_destination, entityId);
			var eventFile = Path.Combine (eventStore, eventBag.EventDate + "--" + eventBag.EventType);

			Directory.CreateDirectory (eventStore);

			File.WriteAllText (eventFile, serializedEventBag);
		}

		public IEnumerable<object> RetrieveBy (string entityId)
		{
			var storeOfEntity = Path.Combine (_destination, entityId);

			var allEventBagsOfEntity = Directory.GetFiles (storeOfEntity);

			// read content of files and transform to EventBag
			// order this list by EventDate
			var eventBags = allEventBagsOfEntity
				.Select (eventBagFile => {
				var fileText = File.ReadAllText (eventBagFile);

				return fileText.ToEventBag ();
			})
				.OrderBy (eventBag => eventBag.EventDate)
				.ToList ();

			// Extract Event of any EventBag
			var events = eventBags.Select (eventBag => eventBag.ExtractEvent ()).ToList ();

			return events;
		}

		public IEnumerable<String> RetrieveEntities ()
		{
			var folders = Directory.GetDirectories (_destination);

			return folders.Select (folder => folder.Replace (_destination + "/", "")).ToList ();
		}

		public IEnumerable<object> RetrieveAllEvents ()
		{
			throw new NotImplementedException ();
		}
	}

}

