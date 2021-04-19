using Business_Model.Helper;
using System;

namespace Business_Model.Model
{
    public sealed class AssignedMapping
    {
        public Guid AssignedMappingID { get; set; }

        public Guid AssignedToId { get; set; }

        public Guid EntityID { get; set; }

        public EntityType EntityType { get; set; }

        public ToDoStatus Status { get; set; }

        public DateTime? CompletedOn { get; set; }

        [IgnoreInsert]
        public EntityState EntityState { get; set; }

        [IgnoreInsert]
        public string AssignedTo { get; set; }
    }
}
