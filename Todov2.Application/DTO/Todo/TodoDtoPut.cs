using Application.DTO.Todo.Enums;

namespace Application.DTO.Todo
{
    public class TodoDtoPut
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public  StatusDto? Status{ get; set; }
    }
}
