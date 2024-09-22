using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tareas.Queries.GetTarea
{
    public class GetTareaQueryDto
    {
        public string Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
