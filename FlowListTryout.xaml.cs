using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manofthematch.Models;
using Manofthematch.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using System.Diagnostics;
using DLToolkit.Forms.Controls;
using DLToolkit.Forms;
using System.Collections.ObjectModel;

namespace Manofthematch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlowListTryout : ContentPage
    {
        public FlowObservableCollection<Club> allClubs = new FlowObservableCollection<Club>();
        public FlowObservableCollection<Object> clubsSortedBySport = new FlowObservableCollection<Object>();
        public List<Club> clubCollection = new List<Club>();

        readonly ApiMethods apiMethods = new ApiMethods();

        public FlowListTryout()
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent();
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
                    clubCollection = await apiMethods.GetAllClubs("GetAllCLubs", 1083);
                    allClubs = new FlowObservableCollection<Club>(clubCollection); //cast List<Club> to FlowObservableCollection
                    
                }
                finally
                {
                    this.IsBusy = false;
                }
            }

            

            clubsSortedBySport = await SortBySports(allClubs, "Soccer");
            TestClubXamlList.FlowItemsSource = clubsSortedBySport;
        }

        async void OnClubSelect(object sender, ItemTappedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine($"No connection");
            }
            else
            {
                await Navigation.PushAsync(new SingleClub((Club)e.Item));
            }
        }

        async Task<FlowObservableCollection<Object>> SortBySports (FlowObservableCollection<Club> allClubs, string sportstype)
        {
            List<Club> clubs = new List<Club>(allClubs);
            //IEnumerable<Club> _sortedClubsSport = new List<Club>();
            FlowObservableCollection<Club> sortedClubsSport = new FlowObservableCollection<Club>();
            //clubs = allClubs;

            var __sortedClubsSport = clubs.Where(a => a.clubSports.Contains(sportstype))
                                    .OrderBy(a => a.clubRegion)
                                    .GroupBy(a => a.clubRegion)                                    
                                    .Select(itemGroup => new Grouping<string, Club>(itemGroup.Key, itemGroup))
                                    .ToList(); //  .Where(c => c.clubSports.Contains(sportstype));

            //IOrderedEnumerable<Club> _sortedClubsSport = new ObservableCollection<Club>(__sortedClubsSport);
            var Items = new FlowObservableCollection<Object>(__sortedClubsSport);
            return Items;
        }

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
    }
}

