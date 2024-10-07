using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IExternalService<T>
        where T : BaseEntity
    {
        Task<bool> Create(T model);
    }
}
