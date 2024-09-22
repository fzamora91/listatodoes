using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Common.Results
{
    public enum ResultType
    {
        Ok,
        Invalid,
        Unauthorized,
        PartialOk,
        NotFound,
        PermissionDenied,
        Unexpected,
        Created
    }
}
