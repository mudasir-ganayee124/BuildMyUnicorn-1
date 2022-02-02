using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNet.SignalR.Hubs;
using System.Configuration;
using BuildMyUnicorn.Business_Layer;
using Business_Model.Model;
using System.Web.Script.Serialization;

namespace BuildMyUnicorn
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }

    
    public class ChatHubClient : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public void SendChatMessage(string who, string message, string sender,string sendername , string module, string section)
        {
            string name = Context.User.Identity.Name;
            ChatMessage Model = new ChatMessage();
            Model.ChatID = Guid.NewGuid();
            Model.ChatMessageID = Guid.NewGuid();
            Model.ChatSenderID =Guid.Parse(sender);
            Model.ChatReceiverID = Guid.Parse(who);
            Model.SenderName = sendername;
            Model.MessageText = message;
            int _module;
            if (int.TryParse(module, out _module))
            {
                Model.Module = (Module)_module;
            }
            int _section;
            if (int.TryParse(section, out _section))
            {
                Model.ModuleSection = (ModuleSection)_section;
            }

            new ChatManager().AddChatMessage(Model);

            foreach (var connectionId in _connections.GetConnections(who.ToLower()))
            {
             
                Clients.Client(connectionId).addChatMessage(new JavaScriptSerializer().Serialize(Model));
               // new ChatManager().UpdateChatMessageRead(Model);
            }


        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            var accparam = this.Context.QueryString["accparam"];
            if (!string.IsNullOrEmpty(accparam))
                name = accparam.ToLower();
            _connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}

