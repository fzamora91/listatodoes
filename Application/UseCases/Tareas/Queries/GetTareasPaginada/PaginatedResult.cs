using Application.UseCases.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.UseCases.Tareas.Queries.GetTareasPaginada
{
    public class PaginatedResult<T>
    {

     

        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }

        public PaginatedResult(List<T> items, int pageNumber, int pageSize, int totalRecords) {

            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;

        }

    }
}
