using Domain.Enum;

namespace Domain.Entities
{
    public class Log
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public LogType Type { get; set; }
    }
}
