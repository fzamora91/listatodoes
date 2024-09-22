using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tarea : BaseEntity
    {
        public required string title { get; set; }
        public required string description { get; set; }
        public required string status { get; set; }
    }
}
