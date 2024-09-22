using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Tarea> Transaction { get; set; }
    }
}
