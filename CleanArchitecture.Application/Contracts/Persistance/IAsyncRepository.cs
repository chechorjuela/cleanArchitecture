using CleanArchitecture.Domain.Common;


namespace CleanArchitecture.Application.Contracts.Persistance
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
    }
}
