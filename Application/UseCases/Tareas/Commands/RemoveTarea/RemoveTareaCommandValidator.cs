using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.RemoveTarea
{
    public class RemoveTareaCommandValidator : AbstractValidator<RemoveTareaCommand>
    {
        public RemoveTareaCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("No tiene Id");
        }
    }
}
