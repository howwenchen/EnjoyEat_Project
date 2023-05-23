using EnjoyEat.Models;
using MessagePack;

namespace EnjoyEat.Controllers.DTO
{   
    public class CustomerServiceDTO
    {
        
        public int QuestionId { get; set; }
        public DateTime QuestionDatetime { get; set; }
        public string QuestionKeynote { get; set; } = null!;
        public string QuestionContent { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
