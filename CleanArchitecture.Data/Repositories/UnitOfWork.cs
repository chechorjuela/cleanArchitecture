using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StreamerDbContext _context;

        private IVideoRepository _videoRepository;
        private IStreamerRepository _streamerRepository;

        public IVideoRepository videoRepository => _videoRepository ??= new VideoRepository(_context);
        public IStreamerRepository streamerRepository => _streamerRepository ??= new StreamerRepository(_context);

        public UnitOfWork(Hashtable repositories, StreamerDbContext context)
        {
            _repositories = repositories;
            _context = context;
        }

        public async Task<int> complate()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null) {
                _repositories= new Hashtable();
            }
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type)) {
                var respositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(respositoryType.MakeGenericType(typeof(TEntity)),_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
