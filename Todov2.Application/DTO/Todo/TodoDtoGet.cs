using System;
using Application.DTO.Todo.Enums;

namespace Application.DTO.Todo
{
    public class TodoDtoGet
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public  StatusDto Status{ get; set; }
    }
}
