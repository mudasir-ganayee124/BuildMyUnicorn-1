using ALMS_DAL;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;


namespace BuildMyUnicorn.Business_Layer
{
    public class ChatManager
    {
        public void AddChatMessage(ChatMessage Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ChatID", ParamterValue = Model.ChatID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ChatSenderID", ParamterValue = Model.ChatSenderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ChatReceiverID", ParamterValue = Model.ChatReceiverID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@MessageText", ParamterValue = Model.MessageText, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@Module", ParamterValue = Model.Module, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ModuleSection", ParamterValue = Model.ModuleSection, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
            new ParametersCollection { ParamterName = "@ChatMessageID", ParamterValue = Model.ChatMessageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_chat", parameters);



        }

        public IEnumerable<ChatMessage> GetChatMessages(Chat Model)
        {
            var query = $@"SELECT * from tbl_chat INNER JOIN tbl_chat_message ON tbl_chat.ChatID = tbl_chat_message.ChatID 
                        WHERE (tbl_chat.ChatSenderID =   '{Model.ChatSenderID}' AND tbl_chat.ChatReceiverID =  '{Model.ChatReceiverID}') OR
				        (tbl_chat.ChatSenderID =  '{Model.ChatReceiverID}' AND tbl_chat.ChatReceiverID =  '{Model.ChatSenderID}')
                        order by tbl_chat_message.MessageDateTime ASC";
            return SharedManager.GetList<ChatMessage>(query);
        }

        public IEnumerable<ChatMessage> GetUnreadChat(Chat Model)
        {
            //var query = $@"SELECT  distinct Module, ModuleSection from tbl_chat INNER JOIN tbl_chat_message ON tbl_chat.ChatID = tbl_chat_message.ChatID 
            //            WHERE ((tbl_chat.ChatSenderID =   '{Model.ChatSenderID}' AND tbl_chat.ChatReceiverID =  '{Model.ChatReceiverID}') OR
            //(tbl_chat.ChatSenderID =  '{Model.ChatReceiverID}' AND tbl_chat.ChatReceiverID =  '{Model.ChatSenderID}')) AND ( Module IS NOT NULL  AND ModuleSection IS NOT NULL AND  IsRead = 1)";

            var query = $@"SELECT  distinct Module, ModuleSection from tbl_chat INNER JOIN tbl_chat_message ON tbl_chat.ChatID = tbl_chat_message.ChatID 
                        WHERE tbl_chat.ChatReceiverID =  '{Guid.Parse(HttpContext.Current.User.Identity.Name)}' AND Module IS NOT NULL  AND ModuleSection IS NOT NULL AND  IsRead = 0";
            //Guid.Parse(HttpContext.Current.User.Identity.Name)
            return SharedManager.GetList<ChatMessage>(query);
        }

        public int GetUnreadChatCount()
        {
           

            var query = $@"SELECT  count(*) from tbl_chat INNER JOIN tbl_chat_message ON tbl_chat.ChatID = tbl_chat_message.ChatID 
                        WHERE tbl_chat.ChatReceiverID =  '{Guid.Parse(HttpContext.Current.User.Identity.Name)}' AND Module IS NOT NULL  AND ModuleSection IS NOT NULL AND  IsRead = 0";
            //Guid.Parse(HttpContext.Current.User.Identity.Name)
            return SharedManager.ExecuteScalar<int>(query);
        }

        public IEnumerable<ChatMessage> GetModuleSectionChatMessages(ChatMessage Model)
        {
            var query = $@"SELECT * from tbl_chat INNER JOIN tbl_chat_message ON tbl_chat.ChatID = tbl_chat_message.ChatID 
                        WHERE ((tbl_chat.ChatSenderID =   '{Model.ChatSenderID}' AND tbl_chat.ChatReceiverID =  '{Model.ChatReceiverID}') OR
				        (tbl_chat.ChatSenderID =  '{Model.ChatReceiverID}' AND tbl_chat.ChatReceiverID =  '{Model.ChatSenderID}')) AND tbl_chat_message.Module ='{(int)Model.Module}' AND tbl_chat_message.ModuleSection = '{(int)Model.ModuleSection}' 
                        order by tbl_chat_message.MessageDateTime ASC";
            return SharedManager.GetList<ChatMessage>(query);
        }

        public IEnumerable<ChatMessage> GetRefreshChatMessages(ChatMessage Model)
        {
            var query = $@"SELECT * from tbl_chat INNER JOIN tbl_chat_message ON tbl_chat.ChatID = tbl_chat_message.ChatID 
                        WHERE 
				        tbl_chat.ChatSenderID =  '{Model.ChatReceiverID}' AND tbl_chat.ChatReceiverID =  '{Model.ChatSenderID}' AND
                        tbl_chat_message.MessageDateTime > CAST( '{Model.lastMessageDateTime}' as datetime)
                        order by tbl_chat_message.MessageDateTime ASC";
            return SharedManager.GetList<ChatMessage>(query);
        }

        public void UpdateChatMessageRead(ChatMessage Model)
        {
            var query = $@"UPDATE tbl_chat_message set IsRead = 1 WHERE ChatMessageID = '{Model.ChatMessageID}'";
            SharedManager.ExecuteScalar<int>(query);
        }

        public void UpdateModuleChatMessageRead(ChatMessage Model)
        {
            var query = $@"UPDATE tbl_chat_message set IsRead = 1 WHERE Module = '{(int)Model.Module}'  AND ModuleSection = '{(int)Model.ModuleSection}' AND  ChatID = (SELECT ChatID FROM tbl_chat WHERE tbl_chat.ChatSenderID = '{Model.ChatReceiverID}' AND tbl_chat.ChatReceiverID = '{Model.ChatSenderID}')";
            SharedManager.ExecuteScalar<int>(query);
        }
    }
}