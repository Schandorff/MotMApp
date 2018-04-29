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
using System.ComponentModel;

namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : ContentPage
	{
        //Authorization authorization = new Authorization();
        public IList<Club> sealandClubs = new ObservableCollection<Club>();
        public IList<Club> jutlandClubs = new ObservableCollection<Club>();
        public IList<Club> fuenenClubs = new ObservableCollection<Club>();
        public IList<Club> bornholmClubs = new ObservableCollection<Club>();

        public IList<Club> allTestClubs = new List<Club>();

        readonly ApiMethods apiMethods = new ApiMethods();
        

        public LandingPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent ();
            BindingContext = this;
            
            
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
                    allTestClubs = clubCollection;
                    jutlandClubs = await SortClubsByRegion(clubCollection, "Jylland");
                    fuenenClubs = await SortClubsByRegion(clubCollection, "Fyn");
                    sealandClubs = await SortClubsByRegion(clubCollection, "Sjælland");
                    bornholmClubs = await SortClubsByRegion(clubCollection, "Bornholm");
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
            
            allJutlandClubs.HeightRequest = (90 * jutlandClubs.Count) + (10 * jutlandClubs.Count);
            allFuenenClubs.HeightRequest = (90 * fuenenClubs.Count) + (10 * fuenenClubs.Count);
            allSealandClubs.HeightRequest = (90 * sealandClubs.Count) + (10 * sealandClubs.Count);
            allBornholmClubs.HeightRequest = (90 * bornholmClubs.Count) + (10 * bornholmClubs.Count);

            int count = 0;
            for (int k = 0; k <= allTestClubs.Count; k++)
            {
                for (int row = 0; row < clubsGrid.RowDefinitions.Count; row++)
                {
                    for (int col = 0; col < clubsGrid.ColumnDefinitions.Count; col++)
                    {
                        if (count >= allTestClubs.Count)
                        {
                            break;
                        }
                        Club tempClub = allTestClubs[count];
                        clubsGrid.Children.Add(new Image { Source = tempClub.clubLogo, HeightRequest = 40, WidthRequest = 40,   }, col, row);
                        count++;
                    }
                }
            }

            var MyCommand = new Command<String>(s => Debug.WriteLine("command executed: {0}", s));
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

        

        //public Command TapCommand => new Command<Club>((ClubObject) => Tapped(ClubObject));

        //private void Tapped(Club ClubObject)
        //{
        //    DisplayAlert("Tapped!", ClubObject.clubName, "Gotcha");
        //}

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //OnClubSelect(sender, args);
            int tapCount = 0;
            tapCount++;
        }
    }
}

//await navigation.poptoroot