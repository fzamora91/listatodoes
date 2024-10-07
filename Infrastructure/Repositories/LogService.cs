using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LogService: ILogService
    {
        private readonly IExternalService<LogDto> logRepository;

        public LogService(IExternalService<LogDto> logRepository)
        {
            this.logRepository = logRepository;
        }

        public async Task LogInformationAsync(string description)
        {
            LogDto log = new LogDto
            {
                Log = new Log
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = description,
                    Date = DateTime.UtcNow,
                    Type = Domain.Enum.LogType.Information
                }
            };
            await logRepository.Create(log);
        }

        public async Task LogErrorAsync(string description)
        {
            LogDto log = new LogDto
            {
                Log = new Log
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = description,
                    Date = DateTime.UtcNow,
                    Type = Domain.Enum.LogType.Error
                }
            };
            await logRepository.Create(log);
        }

        public async Task LogWarningAsync(string description)
        {
            LogDto log = new LogDto
            {
                Log = new Log
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = description,
                    Date = DateTime.UtcNow,
                    Type = Domain.Enum.LogType.Warning
                }
            };
            await logRepository.Create(log);
        }


    }
}
