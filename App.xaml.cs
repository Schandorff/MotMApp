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
            MainPage = new LandingPage();
        }

        protected override void OnStart()
        {
                        
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
