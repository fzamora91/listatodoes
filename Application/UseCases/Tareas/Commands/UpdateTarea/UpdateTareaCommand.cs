using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Application.UseCases.Tareas.Commands.Kafka;
using Application.UseCases.Tareas.Commands.RemoveTarea;
using Domain;
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

                    ICommandSender<OrderCommand> commandSender = new KafkaCommandProducer();
                    var orderHandler = new SendOrderCommandHandler(commandSender);

                    // Enviamos un comando (orden de ejemplo)
                    var orderCommand = new OrderCommand { OrderId = Guid.NewGuid().ToString(), ProductName = "Task Updated Succesfully" };
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
