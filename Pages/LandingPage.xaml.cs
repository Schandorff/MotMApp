using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manofthematch.Models;
using Manofthematch.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using DLToolkit.Forms.Controls;

namespace Manofthematch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public FlowObservableCollection<Club> allClubs = new FlowObservableCollection<Club>();
        public FlowObservableCollection<Object> clubsSorted = new FlowObservableCollection<Object>();
        public List<Club> clubCollection = new List<Club>();
        bool isInitialized = false;
        readonly ApiMethods apiMethods = new ApiMethods();
        public string Message = "Soccer";

        public LandingPage()
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent();
        }

        protected async void ListItems_Refreshing(object sender, EventArgs e)
        {
            await DoRefresh();
            ClubList.EndRefresh(); //important step. It will refresh forever without triggering it
        }

        public async Task DoRefresh()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                //Not Connected
            }
            else
            {
                clubCollection = await apiMethods.GetAllClubs("GetAllCLubs", 1083);
                allClubs = new FlowObservableCollection<Club>(
                clubCollection); //cast List<Club> to FlowObservableCollection
                clubsSorted = await SortClubs(allClubs, Message);
                ClubList.FlowItemsSource = clubsSorted;
            }
        }

        protected override async void OnAppearing()
        {
            CrossConnectivity.Current.ConnectivityChanged += UpdateNetworkInfo;
            base.OnAppearing();
            if (!CrossConnectivity.Current.IsConnected)
            {
                //Not Connected
            }
            else
            {
                clubsSorted = await SortClubs(allClubs, Message);
                ClubList.FlowItemsSource = clubsSorted;
                if (!isInitialized)
                {
                    IsBusy = true;
                    try
                    {
                        clubCollection = await apiMethods.GetAllClubs("GetAllCLubs", 1083);
                        allClubs = new FlowObservableCollection<Club>(
                            clubCollection); //cast List<Club> to FlowObservableCollection
                        clubsSorted = await SortClubs(allClubs, Message);
                        ClubList.FlowItemsSource = clubsSorted;
                    }
                    finally
                    {
                        IsBusy = false;
                        isInitialized = true;
                    }
                }
            }
        }

        //sort the clubs into sports ordered by region
        async Task<FlowObservableCollection<Object>> SortClubs(FlowObservableCollection<Club> allClubs, string sportstype)
        {
            List<Club> clubs = new List<Club>(allClubs);
            var sortedClubs = clubs.Where(a => a.clubSports.Contains(sportstype))
                                    .OrderBy(a => a.clubRegion)
                                    .GroupBy(a => a.clubRegion)
                                    .Select(itemGroup => new Grouping<string, Club>(itemGroup.Key, itemGroup))
                                    .ToList();
            var Items = new FlowObservableCollection<Object>(sortedClubs);
            return Items;
        }

        private async void SportBtn_Clicked(object sender, EventArgs e)
        {
            Button _sender = (Button)sender;
            Message = _sender.CommandParameter.ToString();
            clubsSorted = await SortClubs(allClubs, Message);
            ClubList.FlowItemsSource = clubsSorted;
            switch (Message)
            {
                case "Soccer":
                    sportTypeLabel.Text = "Fodbold";
                    BackgroundImage = "FodboldBG.png";
                    break;
                case "Handball":
                    sportTypeLabel.Text = "Håndbold";
                    BackgroundImage = "HandballBG.png";
                    break;
                case "Tennis":
                    sportTypeLabel.Text = "Tennis";
                    BackgroundImage = "TennisBG.png";
                    break;
                case "Hockey":
                    sportTypeLabel.Text = "Hockey";
                    BackgroundImage = "HockeyBG.png";
                    break;
                default:
                    break;
            }
        }

        async void OnClubSelect(object sender, ItemTappedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                //Not Connected
            }
            else
            {
                await Navigation.PushAsync(new SingleClub((Club) e.Item, Message));
            }
        }

        private async void FavBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouritePage(allClubs));
        }

        //SortClubs Grouping Helper
        public class Grouping<K, T> : FlowObservableCollection<T>
        {
            public K Key { get; private set; }
            public int ColumnCount { get; private set; }

            public Grouping(K key)
            {
                Key = key;
            }

            public Grouping(K key, IEnumerable<T> items)
                : this(key)
            {
                AddRange(items);
            }

            public Grouping(K key, IEnumerable<T> items, int columnCount)
                : this(key, items)
            {
                ColumnCount = columnCount;
            }
        }

        private async void UpdateNetworkInfo(object sender, ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {
                IsBusy = true;
                await DisplayAlert("Ingen Forbindelse", "Du har ikke forbindelse til internettet og funktionaliteten er begrænset", "OK");
            }
            else if (e.IsConnected)
            {
                IsBusy = false;
            }
        }
    }
}

