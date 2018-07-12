using System;

namespace LeftyBotGui
{
    public interface ISerializable
    {
        DateTime LastModified { get; set; }
    }
}
