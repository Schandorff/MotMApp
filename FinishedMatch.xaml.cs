using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using DLToolkit.Forms.Controls;
using Manofthematch.Data;
using Manofthematch.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

namespace Manofthematch
{
    public partial class FinishedMatch : ContentPage
    {
		public Button currentMatchId = new Button();
		readonly ApiMethods apiMethods = new ApiMethods();

		public FinishedMatch(Button currentMatchId)
        {
			this.currentMatchId = currentMatchId;
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent();
        }
    }
}
