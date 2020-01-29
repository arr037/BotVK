using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ApiRequest
    {
        public string Type { get; set; }
        public int GroupId { get; set; }
        public Object Object { get; set; }
    }

    public class Object
    {
        public Message Message { get; set; }
        public ClientInfo ClientInfo { get; set; }
    }
    public class Message
    {
        public int Date { get; set; }
        public long FromId { get; set; }
        //public int Id { get; set; }
        //public int Out { get; set; }
        public long PeerId { get; set; }
        public string Text { get; set; }
        //public int ConversationMessageId { get; set; }
        //public IList<object> FwdMessages { get; set; }
        //public bool Important { get; set; }
        public int RandomId { get; set; }
        //public IList<object> Attachments { get; set; }
        //public bool IsHidden { get; set; }
    }

    public class ClientInfo
    {
        public IList<string> ButtonActions { get; set; }
        public bool Keyboard { get; set; }
        public bool InlineKeyboard { get; set; }
        public int LangId { get; set; }
    }


}
