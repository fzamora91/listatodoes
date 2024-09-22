using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Queries.GetTareas
{
    public class GetTareasQuery() : IRequest<Result<GetTareasQueryDto>>
    {

        public class GetTareasQueryHandler(IRepository<Tarea> repository) : UseCaseHandler, IRequestHandler<GetTareasQuery, Result<GetTareasQueryDto>>
        {
            public async Task<Result<GetTareasQueryDto>> Handle(GetTareasQuery request, CancellationToken cancellationToken)
            {
                var result = await repository.GetAllAsync();

                

                var tareas = result.Select(x => new GetTareasQueryValueDto
                {
                    Id = x.Id,
                    Title = x.title,
                    Description =x.description,
                    Status = x.status
                }).ToList();

                var response = new GetTareasQueryDto()
                {
                    Tareas = tareas
                };

                return Succeded(response);
            }
        }

    }
}
