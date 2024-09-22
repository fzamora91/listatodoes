using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Common.Factories;
using Infrastructure.Data;
using Application.UseCases.Tareas.Queries.GetTareasPaginada;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {


        private readonly ApplicationDbContext _context;


        public Repository(IDbContextFactory factory)
        {
            _context = (ApplicationDbContext)factory.Create();
        }

        protected DbSet<T> Set => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Set.AsQueryable<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }


        // Método para aplicar el orden dinámico
        private IQueryable<T> ApplyOrdering(IQueryable<T> query, string orderByProperty, bool isAscending)
        {
            // Crear una expresión lambda para la propiedad
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, orderByProperty);
            var lambda = Expression.Lambda(property, parameter);

            // Construir la consulta dinamicamente con OrderBy u OrderByDescending
            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            // Obtener el método de LINQ dinámicamente
            var orderByMethod = typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                                    .Single()
                                    .MakeGenericMethod(typeof(T), property.Type);

            // Aplicar el método OrderBy o OrderByDescending
            return (IQueryable<T>)orderByMethod.Invoke(null, new object[] { query, lambda });
        }


        public async Task<PaginatedResult<T>> GetAllAsync(int pagenumber, int pagesize, string orderByProperty, bool isAscending)
        {
            var total = await Set.CountAsync();


            // Obtener la queryable base
            var query = Set.AsQueryable();

            // Aplicar el orden dinámico
            query = ApplyOrdering(query, orderByProperty, isAscending);

            // Paginación
            var tareas = await query
                                .Skip((pagenumber - 1) * pagesize)
                                .Take(pagesize)
                                .ToListAsync();

            return new PaginatedResult<T>(tareas, pagenumber, pagesize, total);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await Set.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task AddAsync(T entity)
        {
            await Set.AddAsync(entity);
            await SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Set.Update(entity);
            await SaveAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            Set.Remove(entity);
            await SaveAsync();
        }

       /* public async Task<IEnumerable<T>> ExecuteStoredProcedure(string query)
        {
            return await Set.FromSqlRaw(query).ToListAsync();
        }*/

        ///////////////////////////// Private Methods ///////////////////////////////
        private async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }




    }
}
