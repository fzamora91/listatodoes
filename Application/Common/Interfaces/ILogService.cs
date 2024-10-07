using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ILogService
    {
        Task LogInformationAsync(string description);
        Task LogErrorAsync(string description);
    }
}
