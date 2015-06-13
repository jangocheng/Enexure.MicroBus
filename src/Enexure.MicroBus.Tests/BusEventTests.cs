﻿using System;
using System.Threading.Tasks;
using Enexure.MicroBus;
using Enexure.MicroBus.MessageContracts;
using Enexure.MicroBus.Tests.Common;
using FluentAssertions;
using NUnit.Framework;

namespace Enexure.MicroBus.Tests
{
	[TestFixture]
	public class EventTests
	{
		class Event : IEvent {}

		class EventHandler : IEventHandler<Event>
		{
			public Task Handle(Event command)
			{
				return Task.FromResult(0);
			}
		}

		[Test]
		public async Task TestEvent()
		{
			var pipline = new Pipeline()
				.AddHandler<PipelineHandler>();

			var bus = new BusBuilder()
				.RegisterEvent<Event>().To(x => x.Handler<EventHandler>(), pipline)
				.BuildBus();

			await bus.Publish(new Event());

		}
	}
}
