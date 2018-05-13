using Manofthematch.Models;
using System;
using Manofthematch.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
	        Guid DeviceId = await LocalStorage.GetCreateDeviceId();
	        DeviceIdLbl.Text = DeviceId.ToString();
        }

	    private void DeleteGuid_OnClicked(object sender, EventArgs e)
	    {
	        LocalStorage.DeleteDeviceId();
	        DeviceIdLbl2.Text = "Device id deleted from local storage";
        }

	    private async void GetAdmin_OnClicked(object sender, EventArgs e)
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

	    private void DeleteAdmin_OnClicked(object sender, EventArgs e)
	    {
	        LocalStorage.DeleteAdminCredentials();
	        AdminLbl2.Text = "Admin credentials deleted from local storage";
	    }
	}
}