using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Queries.GetTareas
{
    public class GetTareasQueryDto
    {
        public List<GetTareasQueryValueDto> Tareas { get; set; }
    }
}
