using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.CreateTarea
{
    public class CreateTareaCommandValidator : AbstractValidator<CreateTareaCommand>
    {
        public CreateTareaCommandValidator() {

            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("No tiene Id");

            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("No tiene titulo");

            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("No tiene Description");

            RuleFor(x => x.Status).NotNull().NotEmpty().WithMessage("No tiene Status");

        }
    }
}
