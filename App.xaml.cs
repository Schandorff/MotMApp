using Xamarin.Forms;
using Manofthematch.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Manofthematch.Data;
using System;

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
    }
}
