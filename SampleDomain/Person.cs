using System;
using EventStore;
using System.Collections.Generic;
using EventStore.Contracts;
using System.Linq;
using EventStore.Contracts.Events;

namespace SampleDomain
{
	public class Person
	{
		IEnumerable<IAmAnEvent> _events;

		public static Person WithEvents (IEnumerable<IAmAnEvent> events)
		{
			return new Person (events);
		}

		protected Person (IEnumerable<IAmAnEvent> events)
		{
			_events = events;
		}

		public String FirstName {
			get {
				var firstName = _events.OfType<BabyIsBorn> ().First ().FirstName;
				return firstName;
			}
		}

		public String LastName {
			get {
				var lastName = _events.OfType<BabyIsBorn> ().First ().LastName;
				return lastName;
			}
		}
	}
}

