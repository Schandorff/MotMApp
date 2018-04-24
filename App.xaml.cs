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
            //MainPage = new StartPage();
            MainPage = new NavigationPage(new LandingPage());
        }

        protected override async void OnStart()
        {
           await GetContent();
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
        async Task GetContent()
        {
            Authorization authorization = new Authorization();
            AllClubs = await authorization.GetAllClubs("GetAllCLubs", 1083);
        }
        public List<Club> AllClubs
        {
            get;
            set;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    //ConnectionDetails.Text = "The Network Connection Status: " + CrossConnectivity.Current.IsConnected.ToString();


        //    CrossConnectivity.Current.ConnectivityChanged += UpdateNetworkInfo;
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();

        //    if (CrossConnectivity.Current != null)
        //        CrossConnectivity.Current.ConnectivityChanged -= UpdateNetworkInfo;
        //}

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
            //if (CrossConnectivity.Current != null && CrossConnectivity.Current.ConnectionTypes != null)
            //{
            //    var connectionType = e.IsConnected;
            //    ConnectionDetails.Text = "The Network Connection Status: " + connectionType.ToString();
            //}
        }

    }
}
