using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.CompleteTarea
{
    public class CompleteTareaCommandValidator : AbstractValidator<CompleteTareaCommand>
    {
        public CompleteTareaCommandValidator() {

            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("No tiene Id");

        }
    }
}
