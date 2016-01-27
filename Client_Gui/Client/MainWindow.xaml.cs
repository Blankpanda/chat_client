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

        // public Chat.Client client = new Chat.Client()

        public MainWindow()
        {
            InitializeComponent();
        }
        
        // Open the Server connection dialog.
        private void FileMenuItemConnect_Click(object sender, RoutedEventArgs e)
        {

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
        
    }
}
