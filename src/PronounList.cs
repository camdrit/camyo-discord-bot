using System;
using System.Collections.Generic;
using System.Text;

namespace Camyo
{
    public class PronounList
    {
        public Dictionary<String, String> pronounsList { get; set; }
        public Dictionary<String, String> pronounsByName { get; set; }
        public List<List<object>> pronounTypes { get; set; }
    }
}
