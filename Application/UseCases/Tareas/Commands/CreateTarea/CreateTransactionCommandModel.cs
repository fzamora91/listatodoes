﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Commands.CreateTarea
{
    public class CreateTareaCommandModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
