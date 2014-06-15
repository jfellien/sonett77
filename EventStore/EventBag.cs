using System;
using System.IO;
using ProtoBuf;

namespace EventStore
{
	[ProtoContract]
	public class EventBag
	{
		[ProtoMember (1)]
		public Guid EventId {
			get;
			set;
		}

		[ProtoMember (2)]
		public String EntityId {
			get;
			set;
		}

		[ProtoMember (3)]
		public String EventData {
			get;
			set;
		}

		[ProtoMember (4)]
		public String EventDate {
			get;
			set;
		}

		[ProtoMember (5)]
		public String EventType {
			get;
			set;
		}
	}
}
