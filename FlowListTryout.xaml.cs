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

namespace Manofthematch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlowListTryout : ContentPage
    {
        public FlowObservableCollection<Club> allTestClubs = new FlowObservableCollection<Club>();
        public List<Club> clubCollection = new List<Club>();

        readonly ApiMethods apiMethods = new ApiMethods();

        public FlowListTryout()
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent();
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

        //public Command ItemSelectedCommand = new Command<SelectedItemChangedEventArgs>((arg) =>
        //{
        //    var item = arg?.SelectedItem as Club;
        //    if (item != null)
        //    {
        //        //SelectedItem = null;
        //        //item.Command?.Execute(null);
        //    }
        //});

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
                    allTestClubs = new FlowObservableCollection<Club>(clubCollection);
                    
                }
                finally
                {
                    this.IsBusy = false;
                }
            }

            TestClubXamlList.FlowItemsSource = clubCollection;
        }
    }
}

