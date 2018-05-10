using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Manofthematch
{
	public partial class MasterDetailPage : MasterDetailPage
    {
        public MasterDetailPage()
        {
            InitializeComponent();

			Detail = new NavigationPage(new LandingPage());
        }
    }
}
