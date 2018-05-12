using Manofthematch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manofthematch.Controls;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using DLToolkit.Forms.Controls;

namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavouritePage : ContentPage
	{
        public LocalStorage LocalStorage = new LocalStorage();
	    
        public FavouritePage (object clubsSorted)
        {
		    NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent ();
		}
        
	    private async void GetGuid_OnClicked(object sender, EventArgs eventArgs)
	    {
	        if (!CrossConnectivity.Current.IsConnected)
	        {
	            Debug.WriteLine($"No connection");
	        }
	        else
	        {
	            Guid DeviceId = await LocalStorage.GetCreateDeviceId();
	            DeviceIdLbl.Text = DeviceId.ToString();
	        }
        }

	    private void DeleteGuid_OnClicked(object sender, EventArgs e)
	    {
	        if (!CrossConnectivity.Current.IsConnected)
	        {
	            Debug.WriteLine($"No connection");
	        }
	        else
	        {
	            LocalStorage.DeleteDeviceId();
	            DeviceIdLbl2.Text = "Device id deleted from local storage";
	        }
        }

	    private async void GetAdmin_OnClicked(object sender, EventArgs e)
	    {
	        if (!CrossConnectivity.Current.IsConnected)
	        {
	            Debug.WriteLine($"No connection");
	        }
	        else
	        {
                Admin clubAdmin = new Admin();
	            Admin admin = await LocalStorage.GetCreateAdminCredentials(clubAdmin);
	            if (admin.Username != null)
	            {
	                AdminLbl.Text = admin.UserFriendlyName;
	            }
	            else
	            {
	                AdminLbl.Text = "No admin in local storage";
	            }
	        }
        }

	    private void DeleteAdmin_OnClicked(object sender, EventArgs e)
	    {
	        if (!CrossConnectivity.Current.IsConnected)
	        {
	            Debug.WriteLine($"No connection");
	        }
	        else
	        {
	            LocalStorage.DeleteAdminCredentials();
	            AdminLbl2.Text = "Admin credentials deleted from local storage";
	        }
        }
	}
}