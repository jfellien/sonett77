using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using EventStore;
using EventStore.Contracts.Models;
using EventStore.Contracts.Events;
using EventStore.Contracts;

namespace SuperDuperWebClient
{
	public class People : NancyModule
	{
		IEventStore eventStore;

		public People (IEventStore eventStore) : base ("/people")
		{
			this.eventStore = eventStore;

			Get ["/new"] = _ => EmptyPerson ();

			Post ["/new"] = _ => SavePerson ();

			Get ["/list/{entityId}"] = _ => ListFor (_.entityId);

			Post ["/person/is-born"] = _ => Born ();
		}

		Negotiator EmptyPerson ()
		{
			return View ["new", new Person ()];
		}

		Response SavePerson ()
		{
			var person = this.Bind<Person> ();

			// Ab hier startet die Demo
			// einfügen eines SpeichernEvents

			var personSaved = new NewPersonRegistered {
				EntityId = person.FirstName + "-" + person.LastName,
				Person = person
			};

			eventStore.Store (personSaved);

			return Response.AsRedirect ("/people/list/" + personSaved.EntityId);
		}

		Negotiator ListFor (string entityId)
		{
			// Der zweite Teil, der in der Demo eingefügt wird
			// laden aller Events zu einer Entity

			var entityEvents = eventStore.RetrieveBy (entityId);
			var events = new Events {
				EventsOf = entityId,
				List = entityEvents
			};

			return View ["list", events];
		}
	}
}

