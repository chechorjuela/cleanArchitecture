using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistance
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<Video> GetVideoByNombre(string nameVideo);
        Task<IEnumerable<Video>> GetVideoByUsername(string userName);
    }
}
