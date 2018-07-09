using System;
using System.Collections.Generic;

namespace LeftyBotGui
{
    public class PronounList
    {
        public Dictionary<String, String> pronounsList { get; set; }
        public Dictionary<String, String> pronounsByName { get; set; }
        public List<List<string>> pronounTypes { get; set; }
    }
}
