using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
	/// <summary>
	/// starts a supplied server from name.
	/// </summary>
	class Start
	{

		private string _Name = "Start";
		private string _Desc = "Starts a server by name";
		public string Name
		{
			get { return _Name; }
			set { Name = _Name; }
		}

		public string Description
		{
			get { return _Desc; }
			set { Description = _Desc; }
		}


		public static void Execute()
		{
			// Set up loggers.

			Console.WriteLine("Enter the server by name that you would like to start.");
			string ServerName =
				Console.ReadLine();

			Server.Logger EventLogger = new Server.Logger(Server.LogType.Type.EVENT, ServerName);


			Server.ServerList InitalizeSettings = new Server.ServerList();
			

			if (InitalizeSettings.ServerExists(ServerName))
			{
				Server.ServerInit.ServerSettings Settings = InitalizeSettings.GetServerByName(ServerName); //   load in user settings.               
				Server.Server srv = new Server.Server(Settings);                                       //    create a new object with those server settings

				EventLogger.Write(ServerName + " settings are loaded.");

				Console.WriteLine(Settings.server_name + " starting.");
				EventLogger.Write(Settings.server_name + " started.");
				srv.Start();     // start the server
			}
			else
			{
				Console.WriteLine("Invalid server name supplied.");
				EventLogger.Write(ServerName + "is an invalid name.  Server was not found.");
			}   
			
		}
	}
}
