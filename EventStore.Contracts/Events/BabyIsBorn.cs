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

		public String FamilyName {
			get;
			set;
		}

		public int YearOfBorn {
			get;
			set;
		}
	}
}

