using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lmsChalenge.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => this._mediator;

       /* public IActionResult Index()
        {
            return View();
        }*/
    }
}
