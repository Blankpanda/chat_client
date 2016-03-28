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
using System.Windows.Shapes;

namespace Client.Connect
{
	/// <summary>
	/// Interaction logic for ConnectDialog.xaml
	/// </summary>
	public partial class ConnectDialog : Window
	{

		public event EventHandler<CustomEventArgs> RaiseCustomEvent;

		Chat.Entry.ClientRequestInfo UserInfo = new Chat.Entry.ClientRequestInfo();

		public ConnectDialog()
		{      
			InitializeComponent();          
		}



		// Close out of the form.
		private void ExitBtn_Click(object sender, RoutedEventArgs e)
		{

			this.Close();
		}

		private void ConnectBtn_Click(object sender, RoutedEventArgs e)
		{            
			// Validate the user information.
			int err = 0;
			if (!(Chat.Net.IsPrivateAddress(ServerNameInputBox.Text)))
			{
				err += 1;

				ServerNameInputBox.Text = "Invalid IP Adress.";
				ServerNameInputBox.Select(0, ServerPortInputBox.Text.Length);

			}


			int PortNumber;           
			try 
			{	        
				PortNumber = int.Parse(ServerPortInputBox.Text);
				if (!(Chat.Net.IsValidPortNumber(PortNumber)))
				{
					err += 1;
					ServerPortInputBox.Text = "Invalid Port Number.";
					ServerNameInputBox.Select(0, ServerPortInputBox.Text.Length);
				}
			}
			catch (Exception)
			{		
				err += 1;
				ServerPortInputBox.Text = "Invalid Port Number.";
				ServerNameInputBox.Select(0, ServerPortInputBox.Text.Length);
			}
			
			// The user properly entered in the IP and the port number and thus we can pass the information to the main form
			if (err == 0)
			{
				UserInfo.ip_address = ServerNameInputBox.Text;   // IP address
				UserInfo.port_number = int.Parse(ServerPortInputBox.Text);  // Port Number
				UserInfo.password = ServerPasswordInputBox.Text; // the password to the server    

				Chat.Client client = new Chat.Client(UserInfo);

				 // CustomEventArgs xxxxxx = new CustomEventArgs(<STRUCT>);
				 // RaiseCustomEvent(this, (ServerPortInputBox.Text));
				this.Close();
			}
			

		}

		private void ServerPasswordInputBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			CanStartConnect();
		}

		private void ServerPortInputBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			CanStartConnect();
		}

		private void ServerNameInputBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			CanStartConnect();
		}

		private void CanStartConnect()
		{
			// enable the connect button if all of the other text box's are filled.
			if (ServerNameInputBox.Text != ""
				&& ServerPasswordInputBox.Text != ""
				&& ServerPortInputBox.Text != "")
			{
				ConnectBtn.IsEnabled = true;
			}
		}

	}

	public class CustomEventArgs : EventArgs
	{
		public CustomEventArgs(string s)
		{
			msg = s;
		}
		private string msg;
		public string Message
		{
			get { return msg; }
		}
	}
}
