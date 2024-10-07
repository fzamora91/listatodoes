using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Application.UseCases.Tareas.Commands.RemoveTarea;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.UpdateTarea
{
    public class UpdateTareaCommand : UpdateTareaCommandModel, IRequest<Result<UpdateTareaCommandDto>>
    {
        public class UpdateTransactionCommandHandler(
            IRepository<Tarea> transactionRepository, ILogService logService) : UseCaseHandler, IRequestHandler<UpdateTareaCommand, Result<UpdateTareaCommandDto>>
        {
            public async Task<Result<UpdateTareaCommandDto>> Handle(UpdateTareaCommand request, CancellationToken cancellationToken)
            {

                var validation = new UpdateTareaCommandValidator();
                var validationResult = await validation.ValidateAsync(request);
                if (validationResult.Errors.Count > 0) throw new Exception(validationResult.ToString());

                var tarea =
                        await transactionRepository.GetByIdAsync(request.Id)
                        ?? throw (new ArgumentException("La Tarea no existe"));

                tarea.Id = request.Id;
                tarea.title = request.Title.ToString();
                tarea.description = request.Description.ToString();
                tarea.status = request.Status.ToString();

                await transactionRepository.UpdateAsync(tarea);

                var resultData = new UpdateTareaCommandDto { Success = true };

                try
                {

                    /*LogDto log = new LogDto();

                    log.Log.Id = Guid.NewGuid().ToString();
                    log.Log.Description = "Task Created Succesfully";
                    log.Log.Date = DateTime.UtcNow;
                    log.Log.Type = Domain.Enum.LogType.Information;*/

                    await logService.LogInformationAsync("Task Created Succesfully");

                }
                catch (Exception exp)
                {

                }

                return this.Succeded(resultData);
            }
        }
    }
}
