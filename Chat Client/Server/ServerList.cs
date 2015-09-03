using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Chat_Client
{
	/// <summary>
	/// Used to maintain and add to a list of directories 
	/// </summary>
	class ServerList
	{
		private string ServerDirectory = "Servers";

		public string MainServerDirectory
		{
			get { return ServerDirectory;  }
			set { MainServerDirectory = ServerDirectory; }
		}

		/* Initalizes a directory in the constructor to hold all fo the servers config files */        
		public ServerList()
		{
			Directory.CreateDirectory(ServerDirectory);
		}


		/* Creates a directory to store the configuration file and writes the configuration file */
		internal void Add (Server.ServerSettings settings)
		{
			
			string NewDirectory = ServerDirectory + @"\" + settings.server_name; // EX Servers\myserver\
			Directory.CreateDirectory(NewDirectory);
																				
				
			
			
			List<string> settings_content = new List<string>();
			// build a list that makes the configuration file

			// Example config file:
			//      server #
			//      name 
			//      backlog
			//      IP
			//      Port

			int ServerNumber = GetServerListCount(); // gives the server a number


			settings_content.Add(ServerNumber.ToString());  // Server #
			settings_content.Add(settings.server_name);     // Name
			settings_content.Add(settings.backlog.ToString());  // Backlog
			settings_content.Add(settings.server_ip_address);   // IP
			settings_content.Add(settings.port_number.ToString());  //  Port

			string TextFile = settings.server_name + "_Config" + ".txt";
			string ConfigPath = ServerDirectory + @"\" + settings.server_name + @"\";

			ConfigPath = ConfigPath + TextFile;
			WriteToTextFile(settings_content, ConfigPath);

			
		}

		/* delets a server from the server list */
		internal void Delete(string path)
		{
			Directory.Delete(path);
		}

		/* Writes to a Text File for the servers configuration. */

		private void WriteToTextFile (List<string> content, string path)
		{           
			using ( StreamWriter writer = new StreamWriter(path) )
			{
				for (int i = 0; i < content.Count; i++)
				{
					writer.WriteLine(content[i]);
				}
			}
		}


		/* Reads a text file  and returns the output in a list or a string.*/

		//  for List
		public List<string> ReadTextFileList (string FileName)
		{
			List<string> elems = new List<string>();
			using( StreamReader reader = new StreamReader(FileName))
			{
				if (File.Exists(FileName))
				{
				  while (reader.Peek() >= 0)
				  {

					  elems.Add(reader.ReadLine());

				  }  
				}
				
			}

			return elems;
		}

		//  for string (string is appended with new line constant).
		public string ReadTextFileString (string FileName)
		{
			StringBuilder sb = new StringBuilder();
			
			using (StreamReader reader = new StreamReader(FileName))
			{
				if (File.Exists(FileName))
				{
					while (reader.Peek() >= 0)
					{

						sb.Append(reader.ReadLine() + "\n");

					}


				}
			}

			string readFile = sb.ToString();

			return readFile;
		}

		// returns the server list with their config files
		public string[] GetServerList()
		{
			string[] directories = Directory.GetDirectories(ServerDirectory);

			return directories;
		}

		// displays the server list using standard output
		public void DisplayServerList()
		{
			string[] directories = Directory.GetDirectories(ServerDirectory);

			if (directories.Length == 0)
			{
				Console.WriteLine("No servers");
			}
			else
			{

				for (int i = 0; i < directories.Length; i++)
				{
					Console.WriteLine(directories[i]);
				}
			}

		}

		/* Gets the total number of directories in the servers directory */        
		public int GetServerListCount()
		{
			int ServerCount = 0;
			ServerCount = Directory.GetDirectories(ServerDirectory, "*").Length;
			return ServerCount;
		}
	}
}
