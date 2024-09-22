using Application.UseCases.Tareas.Commands.CompleteTarea;
using Application.UseCases.Tareas.Commands.CreateTarea;
using Application.UseCases.Tareas.Commands.RemoveTarea;
using Application.UseCases.Tareas.Commands.UpdateTarea;
using Application.UseCases.Tareas.Queries.GetTarea;
using Application.UseCases.Tareas.Queries.GetTareasPaginada;
using AutoMapper;


namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            this.CreateMap<GetTareaQueryModel, GetTareaQuery>();
            this.CreateMap<TareaQueryModel, GetTareaPaginationQuery>();
            this.CreateMap<CreateTareaCommandModel, CreateTareaCommand>();
            this.CreateMap<UpdateTareaCommandModel, UpdateTareaCommand>();
            this.CreateMap<RemoveTareaCommandModel, RemoveTareaCommand>();
            this.CreateMap<CompleteTareaCommandModel, CompleteTareaCommand>();
        }

    }
}
