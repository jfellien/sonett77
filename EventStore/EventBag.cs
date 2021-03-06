using System;
using System.IO;

namespace EventStore
{
	public class EventBag
	{
		public Guid EventId {
			get;
			set;
		}

		public String EntityId {
			get;
			set;
		}

		public String EventData {
			get;
			set;
		}

		public String EventDate {
			get;
			set;
		}

		public String EventType {
			get;
			set;
		}
	}
}
