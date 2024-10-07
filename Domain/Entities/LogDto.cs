namespace Domain.Entities
{
    public class LogDto : BaseEntity
    {
        public LogDto() { }
        public Log Log { get; set; } = new Log();
    }
}
