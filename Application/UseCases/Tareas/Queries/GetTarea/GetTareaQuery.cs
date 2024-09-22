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

namespace Application.UseCases.Tareas.Queries.GetTarea
{
    public class GetTareaQuery : GetTareaQueryModel, IRequest<Result<GetTareaQueryDto>>
    {
        public class GetTareaQueryHandler(IRepository<Tarea> repository) : UseCaseHandler, IRequestHandler<GetTareaQuery, Result<GetTareaQueryDto>>
        {
            

            public async Task<Result<GetTareaQueryDto>> Handle(GetTareaQuery request, CancellationToken cancellationToken)
            {
                var tarea = await repository.GetByIdAsync(request.Id);

                var resultData = new GetTareaQueryDto()
                {

                    Id = tarea.Id,
                    title = tarea.title.ToString(),
                    description = tarea.description.ToString(),
                    status = tarea.status.ToString()
                };


                return this.Succeded(resultData);


            }
        }
    }
}
