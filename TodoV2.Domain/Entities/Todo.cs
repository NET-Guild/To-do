using System;
using TodoV2.Domain.Enums;

namespace TodoV2.Domain.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public  Status Status{ get; set; }
    }
}