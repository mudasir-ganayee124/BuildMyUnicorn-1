using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{

   

    public class Chat
    {
        public Guid ChatID { get; set; }
        public Guid ChatSenderID { get; set; }
        public Guid ChatReceiverID { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }




    }

    public class ChatMessage : Chat
    {
        public Guid ChatMessageID { get; set; }
      
        public string MessageText { get; set; }
        public Module? Module { get; set; }
        public ModuleSection? ModuleSection { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string lastMessageDateTime { get; set; }

        public bool IsRead { get; set; }

    }


}
