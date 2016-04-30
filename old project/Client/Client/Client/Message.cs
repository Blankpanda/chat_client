using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Client
{
	class Message
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
