using System;
using Nancy;
using Nancy.TinyIoc;
using System.Collections.Generic;
using Nancy.Bootstrapper;
using EventStore;
using System.IO;

namespace SuperDuperWebClient
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		public Bootstrapper ()
		{

		}

		protected override void ApplicationStartup (TinyIoCContainer container, IPipelines pipelines)
		{
			StaticConfiguration.DisableErrorTraces = false;

			Setup (container);

			base.ApplicationStartup (container, pipelines);
		}

		void Setup (TinyIoCContainer container)
		{
			var eventsPath = Environment.CurrentDirectory;
			eventsPath += "/Sonett77-events";

			Directory.CreateDirectory (eventsPath);

			container.Register<IEventStore> (new FileStore (eventsPath));
		}
	}
}

