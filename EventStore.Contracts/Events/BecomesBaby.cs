using System;

namespace EventStore.Contracts
{
	public class BecomesBaby :IAmAnEvent
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

