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

        /// <summary>
        /// Load the settings into the Server
        /// </summary>
        /// <param name="ServerSettings"></param>
        public FileServer(ServerInit.ServerSettings ServerSettings)
        {
            _ServerSettings = ServerSettings;
        }

        /// <summary>
        /// Invokes a python script.
        /// </summary>
        public void Start()
        {
        //    var engine = Python.CreateRuntime(); // used to invoke python            
        //    List<string> argv = new List<string>(); // script arguments

        //    //string[] args = SettingsToArray(_ServerSettings);
        //    //args.ToList().ForEach(a => argv.Add(a));
            
        //    //engine.GetSysModule().SetVariable("argv", argv);            
        //    //engine.ExecuteFile(@"server.py");

        //    ProcessStartInfo start = new ProcessStartInfo();
        //    start.FileName = @"server.exe";
        //    start.Arguments = string.Format("{0} {1} {2} {3}",
        //        _ServerSettings.server_ip_address,
        //        _ServerSettings.server_name,
        //        _ServerSettings.server_password,
        //        _ServerSettings.port_number.ToString(),
        //        _ServerSettings.backlog.ToString());
        //    start.UseShellExecute = false;
        //    start.RedirectStandardOutput = true;
        //    using (Process process = Process.Start(start))
        //    {
        //        using(StreamReader reader = process.StandardOutput)
        //        {
        //            string result = reader.ReadToEnd();
        //            Console.Write(result);
        //        }
        //    }

            var ipy = Python.CreateRuntime();
            dynamic test = ipy.UseFile("test.py");
            test.meme();
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
