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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamPlayersPage : ContentPage
	{
	    public FlowObservableCollection<Player> MatchPlayers = new FlowObservableCollection<Player>();
	    public List<Player> mPlayers = new List<Player>();
        readonly ApiMethods apiMethods = new ApiMethods();
        public Button currentMatchId = new Button();

        public TeamPlayersPage (Button currentMatchId)
        {
            this.currentMatchId = currentMatchId;
		    NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent ();

		    //var myCarousel = new CarouselViewControl();
		    
        }

	    protected override async void OnAppearing()
	    {
	        Carousel.ItemsSource = new ObservableCollection<String> { "hi", "my", "name", "is", "dani", "smith" }; // ADD/REMOVE PAGES FROM CAROUSEL ADDING/REMOVING ELEMENTS FROM THE COLLECTION
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
	                int matchID = int.Parse(currentMatchId.CommandParameter.ToString());
	                mPlayers = await apiMethods.GetMatchPlayers("GetMatchPlayers", matchID);
	                MatchPlayers = new FlowObservableCollection<Player>(mPlayers); //cast List<Club> to FlowObservableCollection


	                Carousel.ItemsSource = MatchPlayers;
	            }
	            finally
	            {
	                this.IsBusy = false;
	            }
	        }

	        //myCarousel.ItemTemplate = new DataTemplate(typeof(PlayerCard)); ; //new DataTemplate (typeof(MyView));
	        //myCarousel.Position = 0; //default
	        //myCarousel.InterPageSpacing = 10;
	        //myCarousel.Orientation = CarouselViewOrientation.Horizontal;

        }
    }
}