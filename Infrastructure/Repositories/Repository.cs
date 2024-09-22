using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Common.Factories;
using Infrastructure.Data;
using Application.UseCases.Tareas.Queries.GetTareasPaginada;
using System.Linq;

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

        public async Task<PaginatedResult<T>> GetAllAsync(int pagenumber, int pagesize, string orderByProperty, bool isAscending)
        {
            var total = await Set.CountAsync();
           

            string ordering = isAscending ? orderByProperty : $"{orderByProperty} desc";

            var query = Set.AsQueryable();



            var tareas = this.GetAllAsync().Result.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToList();
            return new PaginatedResult<T>(tareas, pagenumber, pagesize, total);



            /*_context.Tarea
            .Skip((pagenumber - 1) * pagesize)
            .Take(pagesize).ToList<T>();*/

            //.Select(u=>new  { Id = u.Id, Title = u.title, Description = u.description, Status = u.status }).ToList();


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
