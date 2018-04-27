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
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LandingPage());
        }        

        protected override void OnStart()
        {            
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
