using Xamarin.Forms;
using Manofthematch.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Manofthematch.Data;
using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Diagnostics;

namespace Manofthematch
{
    public partial class App : Application
    {

        public List<Club> toomanyclubs;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LandingPage());
        }


        

        protected async override void OnStart()
        {
            toomanyclubs = await GetContent();
            
            CrossConnectivity.Current.ConnectivityChanged += UpdateNetworkInfo;
        }

        

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        async Task<List<Club>> GetContent()
        {
            Authorization authorization = new Authorization();
            AllClubs = await authorization.GetAllClubs("GetAllCLubs", 1083);
            return AllClubs;
        }
        public List<Club> AllClubs
        {
            get;
            set;
        }

        private void UpdateNetworkInfo(object sender, ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {
                Debug.WriteLine($"No connection");
            }
            else if (e.IsConnected)
            {
                Debug.WriteLine($"Connected");
            }

        }

    }
}
