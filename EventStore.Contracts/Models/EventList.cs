using System;
using System.Collections.Generic;
using EventStore.Contracts.Models;

namespace EventStore.Contracts
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

