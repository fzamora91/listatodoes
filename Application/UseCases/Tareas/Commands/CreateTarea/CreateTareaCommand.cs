using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Application.UseCases.Tareas.Commands.Kafka;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Tareas.Commands.CreateTarea
{
    public class CreateTareaCommand : CreateTareaCommandModel, IRequest<Result<CreateTareaCommandDto>>
    {
        public class CreateTransactionCommandHandler(IRepository<Tarea> repository, ILogService logService) : UseCaseHandler, IRequestHandler<CreateTareaCommand, Result<CreateTareaCommandDto>>
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



                var response = new CreateTareaCommandDto()
                {
                    Success = true
                };


                await repository.AddAsync(tarea);

                try
                {


                    ICommandSender<OrderCommand> commandSender = new KafkaCommandProducer();
                    var orderHandler = new SendOrderCommandHandler(commandSender);

                    // Enviamos un comando (orden de ejemplo)
                    var orderCommand = new OrderCommand { OrderId = Guid.NewGuid().ToString(), ProductName = "Task Created Succesfully" };
                    await orderHandler.HandleAsync(orderCommand);


                    await logService.LogInformationAsync("Task Created Succesfully");

                }
                catch(Exception exp)
                {

                }

                
                return Succeded(response);
            }
        }
    }
}
