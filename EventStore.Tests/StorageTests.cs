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
			var @event = new SomeEvent {
				EntityId = Guid.NewGuid ().ToString (),
				Name = "MyName",
				SomeDate = new DateTime (2014, 5, 1, 12, 0, 0),
				SomeNumber = 12345
			};

			_sut.Store (@event);

			var someEvent = _sut.RetrieveBy (@event.EntityId).First () as SomeEvent;

			someEvent.EntityId.Should ().BeEquivalentTo (@event.EntityId);
			someEvent.Name.Should ().BeEquivalentTo (@event.Name);
			someEvent.SomeNumber.Should ().Be (@event.SomeNumber);
			someEvent.SomeDate.Should ().Be (@event.SomeDate);
		}

		[Test]
		public void SaveAnEvent_ShouldGetTheSameEntityId ()
		{
			var @event = new SomeEvent {
				EntityId = Guid.NewGuid ().ToString (),
				Name = "MyName",
				SomeDate = new DateTime (2014, 5, 1, 12, 0, 0),
				SomeNumber = 12345
			};

			_sut.Store (@event);

			var entities = _sut.RetrieveEntities ();

			entities.First ().Should ().BeEquivalentTo (@event.EntityId);
		}

		[Test]
		public void FillInMemoryEventStore_WithAHugeListOfEvents ()
		{
			var inmemoryEventStore = new InMemoryStore ();

			for (int i = 0; i < 100000; i++) {

				inmemoryEventStore.Store (new SomeEvent {
					EntityId = "One-Entity",
					Name = "MyName",
					SomeDate = new DateTime (2014, 5, 1, 12, 0, 0),
					SomeNumber = i
				});

			}

			inmemoryEventStore.RetrieveBy ("One-Entity").Count ().ShouldBeEquivalentTo (100000);

		}
	}
}
