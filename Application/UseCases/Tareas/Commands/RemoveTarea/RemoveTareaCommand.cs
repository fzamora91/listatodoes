using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Application.UseCases.Tareas.Commands.CompleteTarea;
using Application.UseCases.Tareas.Commands.Kafka;
using Domain;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.RemoveTarea
{
    public class RemoveTareaCommand : RemoveTareaCommandModel, IRequest<Result<RemoveTareaCommandDto>>
    {
        public class RemoveTareaCommandHandler(
            IRepository<Tarea> tareaRepository, ILogService logService) : UseCaseHandler, IRequestHandler<RemoveTareaCommand, Result<RemoveTareaCommandDto>>
        {
            public async Task<Result<RemoveTareaCommandDto>> Handle(RemoveTareaCommand request, CancellationToken cancellationToken)
            {

                var validation = new RemoveTareaCommandValidator();
                var validationResult = await validation.ValidateAsync(request);
                if (validationResult.Errors.Count > 0) throw new Exception(validationResult.ToString());

                var tarea =
                        await tareaRepository.GetByIdAsync(request.Id)
                        ?? throw (new ArgumentException("la tarea no existe"));

                await tareaRepository.RemoveAsync(tarea);

                var resultData = new RemoveTareaCommandDto { Success = true };

                try
                {

                    ICommandSender<OrderCommand> commandSender = new KafkaCommandProducer();
                    var orderHandler = new SendOrderCommandHandler(commandSender);

                    // Enviamos un comando (orden de ejemplo)
                    var orderCommand = new OrderCommand { OrderId = Guid.NewGuid().ToString(), ProductName = "Task Removed Succesfully" };
                    await orderHandler.HandleAsync(orderCommand);


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
