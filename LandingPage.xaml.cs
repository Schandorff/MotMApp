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
using Manofthematch;

namespace Manofthematch
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : ContentPage
	{
        //Authorization authorization = new Authorization();
        public IList<Club> sealandClubs = new ObservableCollection<Club>();
        public IList<Club> jutlandClubs = new ObservableCollection<Club>();
        public IList<Club> fuenenClubs = new ObservableCollection<Club>();
        public IList<Club> bornholmClubs = new ObservableCollection<Club>();
        readonly ApiMethods apiMethods = new ApiMethods();
        

        public LandingPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent ();     
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
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
                    var clubCollection = await apiMethods.GetAllClubs("GetAllCLubs", 1083);

                    jutlandClubs = await SortClubsByRegion(clubCollection, "Jylland");
                    fuenenClubs = await SortClubsByRegion(clubCollection, "Fyn");
                    sealandClubs = await SortClubsByRegion(clubCollection, "Sjælland");
                    bornholmClubs = await SortClubsByRegion(clubCollection, "Bornholm");

                    //foreach (Club club in sealandClubs)
                    //{
                    //    if (sealandClubs.All(b => b.clubId != club.clubId))
                    //        clubs.Add(club);
                    //}
                }
                finally
                {
                    this.IsBusy = false;
                }
            }
            allJutlandClubs.ItemsSource = jutlandClubs;
            allFuenenClubs.ItemsSource = fuenenClubs;
            allSealandClubs.ItemsSource = sealandClubs;
            allBornholmClubs.ItemsSource = bornholmClubs;
            //BindingContext = sealandClubs;
        }

        async void OnClubSelect(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new SingleClub((Club)e.Item));
        }

        public async Task<List<Club>> SortClubsByRegion (List<Club> sender, string region)
        {
            List<Club> clubCollection = sender;
            List<Club> sortedClub = new List<Club>();

            foreach (Club club in clubCollection)
            {
                if (club.clubRegion == region)
                { 
                   sortedClub.Add(club);
                }
            }

            return sortedClub;
        }
    }
}

//await navigation.poptoroot