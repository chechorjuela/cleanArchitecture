using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Test.Mocks
{
    public class MockStreamerRepository
    {
        public static void AddDataStreamerRepository(StreamerDbContext streamerDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var streamer = fixture.CreateMany<Streamer>().ToList();
            streamer.Add(fixture.Build<Streamer>()
                .With(tr => tr.CreatedBy, "saorjuela")
                .Without(tr => tr.Videos)
                .Create());

            streamerDbContextFake.Streamers!.AddRange(streamer);
            streamerDbContextFake.SaveChanges();
        }
    }
}
