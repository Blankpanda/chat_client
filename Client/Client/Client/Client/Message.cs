using System;

namespace Client.Client
{
    internal class Message
    {
        public enum MessageType
        {
            SentIP = 1,
            Message,
        }

        public string GetMessageTypeByName(MessageType message)
        {
            string Type = Enum.GetName(typeof(MessageType), message);
            return Type;
        }
    }
}