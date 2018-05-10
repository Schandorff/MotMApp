using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Manofthematch
{
    public partial class AdminLogin : ContentPage
    {
        public AdminLogin()
        {
			NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent();
        }
    }
}
