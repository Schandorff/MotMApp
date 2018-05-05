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
	            Guid DeviceId = await LocalStorage.CreateDeviceId();
	            DeviceIdLbl.Text = DeviceId.ToString();
	            //await Navigation.PushAsync(new SingleClub((Club)e.Item));
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
	            
	            //await Navigation.PushAsync(new SingleClub((Club)e.Item));
	        }
        }
	}
}