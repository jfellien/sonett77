using System;

namespace EventStore.Contracts.Events
{
	public class BabyIsBorn
	{
		public String EntityId {
			get;
			set;
		}

		public String FirstName {
			get;
			set;
		}

		public String LastName {
			get;
			set;
		}

		public int YearOfBorn {
			get;
			set;
		}
	}
}

