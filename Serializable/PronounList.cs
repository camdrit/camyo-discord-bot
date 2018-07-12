using System;
using System.Collections.Generic;

namespace LeftyBotGui
{
    public class PronounList : ISerializable
    {
        public Dictionary<String, String> pronounsList { get; set; }
        public Dictionary<String, String> pronounsByName { get; set; }
        public List<List<string>> pronounTypes { get; set; }
        public DateTime LastModified { get; set; }
    }
}
