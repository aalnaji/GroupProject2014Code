using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageBus.Model
{
    public class Message
    {
        public virtual String Topic { get; set; }
    }
}
