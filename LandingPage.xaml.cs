using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Manofthematch.Data;
using Manofthematch.Models;
using System.Diagnostics;
using Plugin.Connectivity.Abstractions;
using Plugin.Connectivity;

namespace Manofthematch
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : ContentPage
	{
        Authorization authorization = new Authorization();
        readonly IList<Club> clubs = new ObservableCollection<Club>();
        readonly Authorization manager = new Authorization();

        public LandingPage ()
		{
            //foreach (Club club in)
            //{
            //    if (clubs.All(b => b.clubId != club.clubId))
            //        clubs.Add(club);
            //}
            BindingContext = clubs;
            InitializeComponent ();
          

		}
        async void OnRefresh(object sender, EventArgs e)
        {
            // Turn on network indicator
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine($"No connection");
            }
            else
            {
                Debug.WriteLine($"Connected");
                this.IsBusy = true;
                try
                {
                    var clubCollection = await manager.GetAllClubs("GetAllCLubs", 1083);

                    foreach (Club club in clubCollection)
                    {
                        if (clubs.All(b => b.clubId != club.clubId))
                            clubs.Add(club);
                    }
                }
                finally
                {
                    this.IsBusy = false;
                }
            }
        }
        async void OnClubSelect(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new SingleClub((Club)e.Item));
        }        
    }
}

//await navigation.poptoroot