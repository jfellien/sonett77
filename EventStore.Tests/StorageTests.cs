using System;
using NUnit.Framework;
using System.IO;
using FluentAssertions;
using System.Diagnostics;
using System.Linq;
using EventStore;
using EventStore.Contracts.Events;

namespace EventStore.Tests
{
	[TestFixture]
	public class StorageTests
	{
		bool deleteRecursive = true;
		string eventsPath;
		FileStore _sut;

		[SetUp]
		public void Setup ()
		{
			eventsPath = Environment.CurrentDirectory;
			eventsPath += "/Sonett77-events";

			Directory.CreateDirectory (eventsPath);

			_sut = new FileStore (eventsPath);
		}

		[TearDown]
		public void ResetAll ()
		{
			Directory.Delete (eventsPath, deleteRecursive);
		}

		[Test]
		public void SaveAndLoadEventToFileStorage_ShouldTheSameEvent ()
		{
			var entityId = Guid.NewGuid ().ToString ();
			var @event = new SomeEvent {
				Name = "MyName",
				SomeDate = new DateTime (2014, 5, 1, 12, 0, 0),
				SomeNumber = 12345
			};

			_sut.Store (entityId, @event);

			var someEvent = _sut.RetrieveBy (entityId).First () as SomeEvent;

			someEvent.Name.Should ().BeEquivalentTo (@event.Name);
			someEvent.SomeNumber.Should ().Be (@event.SomeNumber);
			someEvent.SomeDate.Should ().Be (@event.SomeDate);
		}

		[Test]
		public void FillInMemoryEventStore_WithAHugeListOfEvents ()
		{
			var inmemoryEventStore = new InMemoryStore ();

			for (int i = 0; i < 100000; i++) {

				inmemoryEventStore.Store ("One-Entity", new SomeEvent {
					Name = "MyName",
					SomeDate = new DateTime (2014, 5, 1, 12, 0, 0),
					SomeNumber = i
				});

			}

			inmemoryEventStore.RetrieveBy ("One-Entity").Count ().ShouldBeEquivalentTo (100000);

		}
	}
}
