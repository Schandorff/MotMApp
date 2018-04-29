﻿using System;
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
        public FlowObservableCollection<Object> clubsSorted = new FlowObservableCollection<Object>();
        public List<Club> clubCollection = new List<Club>();
        bool isInitialized = false;
        readonly ApiMethods apiMethods = new ApiMethods();

        public FlowListTryout()
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            //soccerBtn.Clicked += delegate (object sender, EventArgs e) { this.button_Click(sender, e, "Hockey"); };
            InitializeComponent();
        }        

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!isInitialized)
            {
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
                        clubsSorted = await SortClubs(allClubs, "Soccer");
                        TestClubXamlList.FlowItemsSource = clubsSorted;
                    }
                    finally
                    {
                        this.IsBusy = false;
                        isInitialized = true;
                    }
                }
            }   
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

        

        //sort the clubs into sports ordered by region
        async Task<FlowObservableCollection<Object>> SortClubs (FlowObservableCollection<Club> allClubs, string sportstype)
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

        async private void sportBtn_Clicked(object sender, EventArgs e)
        {
            Button _sender = (Button)sender;
            string message = _sender.CommandParameter.ToString();
            switch (message)
            {
                case "Soccer":
                    sportTypeLabel.Text = "Fodbold";
                    break;
                case "Handball":
                    sportTypeLabel.Text = "Håndbold";
                    break;
                case "Tennis":
                    sportTypeLabel.Text = "Tennis";
                    break;
                case "Hockey":
                    sportTypeLabel.Text = "Hockey";
                    break;
                default:
                    break;
            }
            clubsSorted = await SortClubs(allClubs, message);
            TestClubXamlList.FlowItemsSource = clubsSorted;
        }

        //async void button_Click(object sender, EventArgs e, string message)
        //{
        //    clubsSorted = await SortClubs(allClubs, message);
        //    TestClubXamlList.FlowItemsSource = clubsSorted;
            
        //}
    }
}

