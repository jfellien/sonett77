using System;
using EventStore.Contracts;

namespace EventStore.Contracts.Events
{
	public class SomeEvent : IAmAnEvent
	{
		public SomeEvent ()
		{
		}

		public string EntityId {
			get ;
			set ;
		}

		public String Name {
			get;
			set;
		}

		public DateTime SomeDate {
			get;
			set;
		}

		public int SomeNumber {
			get;
			set;
		}
	}
}

