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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool TryConnection = false;
        public Chat.Entry.ClientRequestInfo UserSettings;

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
                    StartConnection(UserSettings);                    
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

        void StartConnection(Chat.Entry.ClientRequestInfo cri)
        {
            Chat.Client client = new Chat.Client(cri);
            client.Start();

            // send the IP address of this client to the server.


            //todo: authentication.
           
        }

    }
}
