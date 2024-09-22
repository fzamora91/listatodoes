using Application.UseCases.Tareas.Queries.GetTareasPaginada;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRepository<T>
    where T : BaseEntity
    {
        IQueryable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        Task<PaginatedResult<T>> GetAllAsync(int pagenumber, int pagesize, string orderByProperty, bool isAscending);

        Task<T?> GetByIdAsync(Guid id);

        Task<T> GetByIdAsync(string id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        //Task<IEnumerable<T>> ExecuteStoredProcedure(string query);
    }
}
