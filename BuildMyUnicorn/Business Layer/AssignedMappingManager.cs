using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildMyUnicorn.Business_Layer
{
    public class AssignedMappingManager
    {
        private readonly string SaveQuery = "INSERT into tbl_assigned_mapping ({0}) VALUES ({1})";

        private readonly string UpdateQuery = "UPDATE tbl_assigned_mapping SET {0} WHERE AssignedMappingID = @AssignedMappingID ";

        private readonly string DeleteQuery = "DELETE tbl_assigned_mapping WHERE AssignedMappingID = @AssignedMappingID ";

        private readonly string ItemQuery = @"SELECT AM.*, FirstName +' ' + LastName AS AssignedTo FROM tbl_assigned_mapping AM 
                                             LEFT JOIN tbl_client ON ClientID = AM.AssignedToID ";


        public void BulkAssign(List<AssignedMapping> mappings, Guid entityId)
        {
            if (mappings == null)
                return;

            foreach (var item in mappings)
            {
                switch (item.EntityState)
                {
                    case EntityState.New:
                        item.EntityID = entityId;
                        Save(item);
                        break;

                    case EntityState.Modified:
                        item.EntityID = entityId;
                        Update(item);
                        break;

                    case EntityState.Deleted:
                        DeleteByEntity(entityId, item.AssignedToId);
                        break;

                    default:
                        break;
                }
            }
        }
        public List<AssignedMapping> GetAssignedMapping(Guid entityId)
        {
            var query = ItemQuery + $"WHERE EntityID = '{entityId}' ";
            return SharedManager.GetList<AssignedMapping>(query).ToList();
        }

        public ResponseModel Save(AssignedMapping assignedMapping)
        {
            assignedMapping.AssignedMappingID = Guid.NewGuid();

            var responseModel = SharedManager.Save(assignedMapping, SaveQuery);
            responseModel.EntityID = assignedMapping.AssignedMappingID;
            return responseModel;
        }


        public ResponseModel Update(AssignedMapping assignedMapping)
        {
            var resposne = SharedManager.Update(assignedMapping, UpdateQuery);
            resposne.EntityID = assignedMapping.AssignedMappingID;
            return resposne;
        }

        public int Delete(Guid id)
        {
            return SharedManager.Delete(DeleteQuery, new { AssignedMappingId = id });
        }

        public int DeleteByEntity(Guid entityId)
        {
            return SharedManager.Delete($"DELETE tbl_assigned_mapping WHERE EntityID = @EntityID ", new { EntityID = entityId });
        }

        public int DeleteByEntity(Guid entityId, Guid assignedToID)
        {
            return SharedManager.Delete($"DELETE tbl_assigned_mapping WHERE EntityID = @EntityID AND AssignedToID = @AssignedToID ",
                new { EntityID = entityId, AssignedToID = assignedToID });
        }

    };
}