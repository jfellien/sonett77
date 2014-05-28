using System;
using EventStore.Contracts.Models;

namespace EventStore.Contracts.Events
{
	public class NewPersonRegistered : IAmAnEvent
	{
		public string EntityId {
			get ;
			set ;
		}

		public Person Person {
			get;
			set;
		}
	}
}

