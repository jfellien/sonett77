using System;
using System.IO;

namespace EventStore.Contracts
{
	public interface IAmAnEvent
	{
		string EntityId { get; set; }
	}
}
