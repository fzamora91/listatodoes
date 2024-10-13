using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Application.UseCases.Tareas.Commands.CreateTarea;
using Application.UseCases.Tareas.Commands.Kafka;
using Domain;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.CompleteTarea
{
    public class CompleteTareaCommand : CompleteTareaCommandModel, IRequest<Result<CompleteTareaCommandDto>>
    {
        public class UpdateTransactionCommandHandler(
            IRepository<Tarea> transactionRepository, ILogService logService) : UseCaseHandler, IRequestHandler<CompleteTareaCommand, Result<CompleteTareaCommandDto>>
        {
            public async Task<Result<CompleteTareaCommandDto>> Handle(CompleteTareaCommand request, CancellationToken cancellationToken)
            {

                var validation = new CompleteTareaCommandValidator();
                var validationResult = await validation.ValidateAsync(request);
                if (validationResult.Errors.Count > 0) throw new Exception(validationResult.ToString());

                var tarea =
                        await transactionRepository.GetByIdAsync(request.Id)
                        ?? throw (new ArgumentException("La Tarea no existe"));

                tarea.Id = request.Id;
                /*tarea.title = request.Title.ToString();
                tarea.description = request.Description.ToString();*/
                tarea.status = "Done";

                await transactionRepository.UpdateAsync(tarea);

                var resultData = new CompleteTareaCommandDto { Success = true };


                

                try
                {
                    // Configuramos el producer y el handler de comandos
                    ICommandSender<OrderCommand> commandSender = new KafkaCommandProducer();
                    var orderHandler = new SendOrderCommandHandler(commandSender);

                    // Enviamos un comando (orden de ejemplo)
                    var orderCommand = new OrderCommand { OrderId = Guid.NewGuid().ToString(), ProductName = "Task Completed Succesfully" };
                    await orderHandler.HandleAsync(orderCommand);

                    await logService.LogInformationAsync("Task Completed Succesfully");
                }
                catch (Exception exp)
                {

                }

                return this.Succeded(resultData);
            }
        }
    }
}
