using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;
using System.IO;

namespace Chat_Client.Server
{
	class FileServer
	{
		private static ServerInit.ServerSettings _ServerSettings;

		private string _ResourceFolderPath;

		public string ResourceFolderPath
		{
			get { return _ResourceFolderPath;}
			set { _ResourceFolderPath = value;}
		}
	
		/// <summary>
		/// Load the settings into the Server
		/// </summary>
		/// <param name="ServerSettings"></param>
		public FileServer(ServerInit.ServerSettings ServerSettings)
		{
			_ServerSettings = ServerSettings;

			// create resource directory if it hasn't been created
			CreateResourceFolder(@"res"); // todo user defined.
			_ResourceFolderPath = @"res";
		}
		
		private void CreateResourceFolder(string DirectoryPath)
		{
			if (!(Directory.Exists(DirectoryPath)))
			{
				Directory.CreateDirectory(DirectoryPath);
			}
		}

		/// <summary>
		/// Invokes a python script.
		/// </summary>
		public void Start()
		{
			CommandStructure.Commands.Clear.Execute(); // clear the terminal

			var IronPythonRunTime = Python.CreateRuntime();

			dynamic server = IronPythonRunTime.UseFile(@"python/server.py");
			server.Start(
				_ServerSettings.server_ip_address,
				_ServerSettings.server_name,
				_ServerSettings.server_password,
				_ServerSettings.port_number.ToString(),
				_ServerSettings.backlog.ToString(),
				_ResourceFolderPath);
		}

		/// <summary>
		/// this is the biggest hack job I have ever done but I dont care
		/// a[0] = server_ip_address
		/// a[1] = server_name
		/// a[2] = server_password
		/// a[3] = server_port_number
		/// a[4] = server_backlog
		/// </summary>
		/// <param name="settings"></param>
		/// <returns></returns>=
		private static string[] SettingsToArray(ServerInit.ServerSettings settings)
		{
			string[] a_settings = new string[5];
			a_settings[0] = settings.server_ip_address;
			a_settings[1] = settings.server_name;
			a_settings[2] = settings.server_password;
			a_settings[3] = settings.port_number.ToString();
			a_settings[4] = settings.backlog.ToString();

			return a_settings;
		}
	}
}
