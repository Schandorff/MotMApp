using Xamarin.Forms;
using Manofthematch.Models;
using Manofthematch.Data;
using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Diagnostics;
using Akavache;
using DLToolkit.Forms.Controls;
using Manofthematch.Controls;

namespace Manofthematch
{
    public partial class App : Application
    {
        public LocalStorage LocalStorage = new LocalStorage();
        ApiMethods apiMethods = new ApiMethods();
        //public static NavigationPage Navigation = null;
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
			MainPage = new NavigationPage(new LandingPage());
            BlobCache.ApplicationName = "Manofthematch";
        }        

        protected override async void OnStart()
        {   
            Guid DeviceId = await LocalStorage.GetCreateDeviceId(); //Ensure that a device id is created
        }        

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        // Called by the back button in navigation bar.
        public async void OnBackButtonPressed(object sender, EventArgs e){
            
            await MainPage.Navigation.PopAsync();
        }

		async void OnLoginPressed(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                //Not Connected
            }
            else
            {
                Admin admin = await LocalStorage.GetAdminCredentials();
                if (admin.Username != null)
                {
                    Club club = await apiMethods.GetClub("GetClub", admin.ClubId);
                    await MainPage.Navigation.PushAsync(new AdminTeamsMatches(club));
                }
                else
                {
                    await MainPage.Navigation.PushAsync(new AdminLogin());
                }
            }
        }
    }
}
