using System;
using System.Collections.Generic;

namespace EventStore.Contracts.Models
{
	public class Events
	{
		public String EventsOf {
			get;
			set;
		}

		public IEnumerable<IAmAnEvent> List {
			get;
			set;
		}
	}
}

