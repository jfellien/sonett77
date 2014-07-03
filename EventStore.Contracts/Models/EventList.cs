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

		public IEnumerable<object> List {
			get;
			set;
		}
	}
}

