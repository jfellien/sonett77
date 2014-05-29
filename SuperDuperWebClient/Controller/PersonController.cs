using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using EventStore;
using EventStore.Contracts;
using EventStore.Contracts.Events;
using EventStore.Contracts.Models;
using SampleDomain;

namespace SuperDuperWebClient
{
	public class PersonController : NancyModule
	{
		IEventStore eventStore;

		public PersonController (IEventStore eventStore) : base ("/person")
		{
		
			this.eventStore = eventStore;

			Get ["/born"] = _ => NewBaby ();

			Post ["/born"] = _ => RegisterThisBaby ();

			Get ["/happened/{eventName}/{entityId}"] = _ => WhenItsHappened (_.entityId, _.eventName);

			Post ["/happened/{eventName}/{entityId}"] = _ => RegisterEvent ();

			Get ["/state/{entityId}"] = _ => StateOfPerson (_.entityId);

			Get ["/list-people"] = _ => ListOfPeople ();

			Get ["/list/{entityId}"] = _ => ListFor (_.entityId);

		}

		Negotiator NewBaby ()
		{
			return View ["new", new Baby ()];
		}

		Negotiator RegisterThisBaby ()
		{
			var baby = this.Bind<Baby> ();

			var entityId = baby.FirstName + "-" + baby.LastName;
			var babyIsBorn = new BabyIsBorn {
				EntityId = entityId,
				FirstName = baby.FirstName,
				LastName = baby.LastName,
				YearOfBorn = baby.Year
			};

			eventStore.Store (babyIsBorn);
			var person = RestoreFrom (entityId);

			return View ["state-of-person", person];
		}

		Negotiator StateOfPerson (string entityId)
		{
			var person = RestoreFrom (entityId);

			return View ["state-of-person", person];
		}

		Negotiator WhenItsHappened (string entityId, string eventName)
		{
			var person = RestoreFrom (entityId);

			return View ["happened", new Happening {
				EntityId = entityId,
				EventName = eventName,
				Year = CurrentYear,
				FirstName = person.FirstName,
				LastName = person.LastName
			}];
		}

		Negotiator RegisterEvent ()
		{ 
			var happening = this.Bind<Happening> ();

			var @event = happening.ToEvent ();
			eventStore.Store (@event);

			var person = RestoreFrom (@event.EntityId);

			return View ["state-of-person", person];
		}

		Negotiator ListOfPeople ()
		{
			var entities = eventStore.RetrieveEntities ();

			return View ["list-people", entities];
		}

		Negotiator ListFor (string entityId)
		{
			var entityEvents = eventStore.RetrieveBy (entityId);
			var events = new Events {
				EventsOf = entityId,
				List = entityEvents
			};

			return View ["list-events", events];
		}

		int CurrentYear {
			get{ return DateTime.UtcNow.Year; }
		}

		Person RestoreFrom (string entityId)
		{
			var entityEvents = eventStore.RetrieveBy (entityId);
			return Person.WithEvents (entityEvents);
		}
	}
}

