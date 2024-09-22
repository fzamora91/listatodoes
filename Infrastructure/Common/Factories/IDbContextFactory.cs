using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Factories
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}
