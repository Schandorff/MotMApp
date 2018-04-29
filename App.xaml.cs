using Xamarin.Forms;
using Manofthematch.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Manofthematch.Data;
using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Diagnostics;
using DLToolkit.Forms;
using DLToolkit.Forms.Controls;

namespace Manofthematch
{
    public partial class App : Application
    {
        //public static NavigationPage Navigation = null;
        public App()
        {
            
            InitializeComponent();
            FlowListView.Init();
            MainPage = new NavigationPage(new FlowListTryout());
            //Application.Current.MainPage = Navigation;
            //Current.MainPage = Navigation;
            
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

        // Called by the back button in our header/navigation bar.
        public async void OnBackButtonPressed(object sender, EventArgs e)
        {
            await ((NavigationPage)Application.Current.MainPage).PopAsync();
        }
    }
}
