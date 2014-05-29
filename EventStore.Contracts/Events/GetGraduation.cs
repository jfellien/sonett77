using System;

namespace EventStore.Contracts
{
	public class GetGraduation : IAmAnEvent
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

