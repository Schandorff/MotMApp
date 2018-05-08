﻿using Xamarin.Forms;
using Manofthematch.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Manofthematch.Data;
using System;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Diagnostics;
using Akavache;
using DLToolkit.Forms;
using DLToolkit.Forms.Controls;
using Manofthematch.Controls;

namespace Manofthematch
{
    public partial class App : Application
    {
        public LocalStorage LocalStorage = new LocalStorage();
        //public static NavigationPage Navigation = null;
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            MainPage = new NavigationPage(new LandingPage());
            BlobCache.ApplicationName = "Manofthematch";
        }        

        protected async override void OnStart()
        {            
            CrossConnectivity.Current.ConnectivityChanged += UpdateNetworkInfo;
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
            await MainPage.Navigation.PopAsync();
        }
    }
}
