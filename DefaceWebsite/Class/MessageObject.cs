using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaceWebsite.Class
{
    public class MessageResult
    {
       public List<MessageObject> ListMessage { get; set; }
    }
    public class MessageObject
    {
        public string message_id { get; set; }
    }
}
