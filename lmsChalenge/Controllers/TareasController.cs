using Application.UseCases.Tareas.Commands.CompleteTarea;
using Application.UseCases.Tareas.Commands.CreateTarea;
using Application.UseCases.Tareas.Commands.RemoveTarea;
using Application.UseCases.Tareas.Commands.UpdateTarea;
using Application.UseCases.Tareas.Queries.GetTarea;
using Application.UseCases.Tareas.Queries.GetTareas;
using Application.UseCases.Tareas.Queries.GetTareasPaginada;
using Microsoft.AspNetCore.Mvc;



namespace lmsChalenge.Controllers
{
    public class TareasController : BaseController
    {
        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetTareasQuery();
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }


        [HttpPost]
        [Route("Paginate")]
        public async Task<IActionResult> Paginate([FromBody] TareaQueryModel model)
        {
            

            var query = this.Mapper.Map<GetTareaPaginationQuery>(model);
            var result = await this.Mediator.Send(query);
            return Ok(result);
        }



        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetById([FromBody] GetTareaQueryModel model)
        {
            var query = this.Mapper.Map<GetTareaQuery>(model);
            var result = await this.Mediator.Send(query);
            return this.FromResult(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTarea([FromBody]CreateTareaCommandModel model)
        {
            var command = this.Mapper.Map<CreateTareaCommand>(model);
            var result = await this.Mediator.Send(command);
            return this.FromResult(result);
        }

        [HttpPost]
        [Route("Completar")]
        public async Task<IActionResult> MarcarTareaCompletada([FromBody] CompleteTareaCommandModel model)
        {
            var command = this.Mapper.Map<CompleteTareaCommand>(model);
            var result = await this.Mediator.Send(command);
            return this.FromResult(result);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateTarea([FromBody] UpdateTareaCommandModel model)
        {
            var command = this.Mapper.Map<UpdateTareaCommand>(model);
            var result = await this.Mediator.Send(command);
            return this.FromResult(result);
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteTarea([FromBody] RemoveTareaCommandModel model)
        {
            var command = this.Mapper.Map<RemoveTareaCommand>(model);
            var result = await this.Mediator.Send(command);
            return this.FromResult(result);
        }



    }
}
