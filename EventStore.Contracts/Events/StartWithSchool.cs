using System;

namespace EventStore.Contracts
{
	public class StartWithSchool : IAmAnEvent
	{
		public string EntityId {
			get ;
			set ;
		}

		public int At {
			get;
			set;
		}
	}
}

