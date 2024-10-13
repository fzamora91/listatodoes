using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.Kafka
{
    public class SendOrderCommandHandler
    {
        private readonly ICommandSender<OrderCommand> _commandSender;

        public SendOrderCommandHandler(ICommandSender<OrderCommand> commandSender)
        {
            _commandSender = commandSender;
        }

        public async Task HandleAsync(OrderCommand command)
        {
            // Lógica de negocio antes de enviar
            await _commandSender.SendCommandAsync(command);
        }
    }

    public class OrderCommand
    {
        public string OrderId { get; set; }
        public string ProductName { get; set; }
    }
}
