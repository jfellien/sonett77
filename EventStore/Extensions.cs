using System;
using System.Collections.Generic;
using EventStore.Contracts;
using fastJSON;

namespace EventStore
{
	static class Extensions
	{
		static JSONParameters _serializationParameters = null;

		public static EventBag ToEventBag (this IAmAnEvent source)
		{
			var serializedSource = JSON.ToJSON (source, DefaultSerializationParameters);

			var eventBag = new EventBag {
				EventId = Guid.NewGuid (),
				EntityId = source.EntityId,
				EventDate = DateTime.Now.ToUniversalTime ().ToString ("O"),
				EventType = source.GetType ().Name,
				EventData = serializedSource
			};

			return eventBag;
		}

		public static EventBag ToEventBag (this String source)
		{
			var eventBag = JSON.ToObject<EventBag> (source, DefaultSerializationParameters);

			return eventBag;
		}

		public static IAmAnEvent ExtractEvent (this EventBag source)
		{
			var eventType = Type.GetType (source.EventType);
			var @event = JSON.ToObject (source.EventData, eventType) as IAmAnEvent;

			return @event;
		}

		public static string Serialize (this EventBag source)
		{
			var serializedEventBag = JSON.ToJSON (source, DefaultSerializationParameters);

			return serializedEventBag;
		}

		static JSONParameters DefaultSerializationParameters {
			get {
				if (_serializationParameters == null) {
					_serializationParameters = new JSONParameters ();
					_serializationParameters.UseUTCDateTime = true;
					_serializationParameters.KVStyleStringDictionary = true;
					_serializationParameters.EnableAnonymousTypes = false;
				}
				return _serializationParameters;
			}
		}
	}
}
