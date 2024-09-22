using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Queries.GetTareasPaginada
{
    public class TareaQueryModel
    {
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }
    }
}
