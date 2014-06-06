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

		public void Store (IAmAnEvent @event)
		{
			var eventBag = @event.ToEventBag ();

			var serializedEventBag = eventBag.Serialize ();

			var eventStore = Path.Combine (_destination, @event.EntityId);
			var eventFile = Path.Combine (eventStore, eventBag.EventDate + "--" + eventBag.EventType);

			Directory.CreateDirectory (eventStore);

			File.WriteAllText (eventFile, serializedEventBag);
		}

		public IEnumerable<IAmAnEvent> RetrieveBy (string entityId)
		{
			var storeOfEntity = Path.Combine (_destination, entityId);

			var allEventBagsOfEntity = Directory.GetFiles (storeOfEntity);

			foreach (var eventBagFile in allEventBagsOfEntity) {

				var fileText = File.ReadAllText (eventBagFile);
				var eventBagFromFile = fileText.ToEventBag ();
				var someEvent = eventBagFromFile.ExtractEvent ();

				yield return someEvent;
			}
		}

		public IEnumerable<String> RetrieveEntities ()
		{
			var folders = Directory.GetDirectories (_destination);

			return folders.Select (folder => folder.Replace (_destination + "/", "")).ToList ();
		}
	}
}

