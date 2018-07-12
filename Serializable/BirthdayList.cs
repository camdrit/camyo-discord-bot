using System;
using System.Collections.Generic;

namespace LeftyBotGui
{
    public class BirthdayList : ISerializable
    {
        public Dictionary<String, String> birthdaysList { get; set; }
        public DateTime LastModified { get; set; }
    }
}
