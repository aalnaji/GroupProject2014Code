using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageBus.Model
{
    public abstract class Command : Message
    {
        public override  String Topic { get { return typeof(Command).AssemblyQualifiedName; } }
        public abstract void Execute();
    }
}
