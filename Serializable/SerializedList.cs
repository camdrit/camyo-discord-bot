using System;
using System.Collections.Generic;

namespace LeftyBotGui
{
    public class SerializedList : ISerializable
    {
        public List<string> responses { get; set; }
        public DateTime LastModified { get; set; }
    }
}
