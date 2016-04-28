using Client.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool TryConnection = false;
        public Chat.Entry.ClientRequestInfo UserSettings;

        TcpClient ClientSocket = new TcpClient();
        NetworkStream ServerStream = default(NetworkStream);
        string ReadData = null;

        public MainWindow()
        {
            InitializeComponent();

            FileMenuItemDisconnect.IsEnabled = false;
        }
        
        // Open the Server connection dialog.
        // TODO: we need to make it so that it calls client.Start() when its ready
        private void FileMenuItemConnect_Click(object sender, RoutedEventArgs e)
        {
            // Open the Connection form and initialize an event to pass the data.
            Connect.ConnectDialog ConnectDialogForm = new Connect.ConnectDialog();
            ConnectDialogForm.RaiseConnectionFormEvent += 
                new EventHandler<ConnectionFormEventArgs>(ConnectDiaglogForm_RaiseCustomEvent);
            ConnectDialogForm.Show();           

            // try to establish a connection using the user settings.             
            if (TryConnection)
            {                
                try
                {
                    FileMenuItemDisconnect.IsEnabled = true;
                 //   StartConnection(UserSettings);
            
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
             

        }

        // Disconnect to the current server.
        private void FileMenuItemDisconnect_Click(object sender, RoutedEventArgs e)
        {
          
        }

        // Close out of the application.
        private void FileMenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // handles the passage of data between forms
        void ConnectDiaglogForm_RaiseCustomEvent(object sender, ConnectionFormEventArgs e)
        {
            UserSettings = e.ServerRequestSettings;
            TryConnection = true;
        }

        //void StartConnection(Chat.Entry.ClientRequestInfo cri)
        //{
        //    Chat.Client client = new Chat.Client(cri, this);
        //    client.Start2();     
        //    //todo: authentication.
           
        //}

        // Hack: need to add this here sucks.
        // todo: rename and or (preferably or, perhaps and + or but who know) remove.
        public void Start2()
        {
            ReadData = "Connecting to Chat Server...";
            UpdateMainWindow();
            ClientSocket.Connect(UserSettings.ip_address, UserSettings.port_number);
            ServerStream = ClientSocket.GetStream();

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(MessageTextBox.Text + "$"); //txt box
            ServerStream.Write(outStream, 0, outStream.Length);
            ServerStream.Flush();

            Thread ctThread = new Thread(getMessage);
        }

        private void getMessage()
        {
            while (true)
            {
                ServerStream = ClientSocket.GetStream();
                int buffsize = 0;
                byte[] inStream = new byte[10025];
                buffsize = ClientSocket.ReceiveBufferSize;
                ServerStream.Read(inStream, 0, buffsize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                ReadData = "" + returndata;
                UpdateMainWindow();

            }
        }
        private void UpdateMainWindow()
        {
            MessageTextBox.Text = MessageTextBox.Text + Environment.NewLine + " >> " + ReadData;
        }

        private void SendButton_click(object sender, RoutedEventArgs e)
        {            
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(MessageTextBox.Text + "$");
            ServerStream.Write(outStream, 0, outStream.Length);
            ServerStream.Flush();
        }

        private void test_button_click(object sender, RoutedEventArgs e)
        {
            Start2();
        }

    }
}
