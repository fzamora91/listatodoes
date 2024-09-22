using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Application.UseCases.Tareas.Queries.GetTareas;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Queries.GetTareasPaginada
{
    public class GetTareaPaginationQuery() : TareaQueryModel, IRequest<PaginatedResult<Tarea>>
    {

        public class GetTareasQueryHandler(IRepository<Tarea> repository) : UseCaseHandler, IRequestHandler<GetTareaPaginationQuery, PaginatedResult<Tarea>>
        {
            public async Task<PaginatedResult<Tarea>> Handle(GetTareaPaginationQuery request, CancellationToken cancellationToken)
            {
                var result = await repository.GetAllAsync(request.PageNumber, request.PageSize);

                return new PaginatedResult<Tarea>(result.Items, request.PageNumber, request.PageSize, result.Items.Count);

              
            }
        }

    }
}
