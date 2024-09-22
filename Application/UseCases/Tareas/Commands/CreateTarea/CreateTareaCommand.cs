using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tareas.Commands.CreateTarea
{
    public class CreateTareaCommand : CreateTareaCommandModel, IRequest<Result<CreateTareaCommandDto>>
    {
        public class CreateTransactionCommandHandler(IRepository<Tarea> repository) : UseCaseHandler, IRequestHandler<CreateTareaCommand, Result<CreateTareaCommandDto>>
        {
            public async Task<Result<CreateTareaCommandDto>> Handle(CreateTareaCommand request, CancellationToken cancellationToken)
            {


                var validation = new CreateTareaCommandValidator();
                var validationResult = await validation.ValidateAsync(request);
                if (validationResult.Errors.Count > 0) throw new Exception(validationResult.ToString());


                var tarea = new Tarea()
                {
                    Id = request.Id,
                    title = request.Title,
                    description=request.Description,
                    status=request.Status
                };

                

                await repository.AddAsync(tarea);

                var response = new CreateTareaCommandDto()
                {
                    Success = true
                };

                return Succeded(response);
            }
        }
    }
}
