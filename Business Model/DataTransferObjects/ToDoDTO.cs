using Business_Model.Helper;
using System;

namespace Business_Model
{
    public sealed class ToDoDTO
    {
        public Guid ToDoTaskID { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public ToDoStatus Status { get; set; }

        public DateTime? AssignedOn { get; set; }

        public DateTime? CompletedOn { get; set; }
    }
}
