using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistance;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class StreamerRepository : BaseRepository<Streamer>, IStreamerRepository
    {
        public StreamerRepository(StreamerDbContext context) : base(context)
        {
        }
    }
}
