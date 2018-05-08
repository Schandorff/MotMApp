using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using DLToolkit.Forms.Controls;
using Manofthematch.Controls;
using Manofthematch.Data;
using Manofthematch.Models;
using MOTMUmbracoBackend.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamPlayersPage : ContentPage
	{
	    public FlowObservableCollection<Player> MatchPlayers = new FlowObservableCollection<Player>();
	    public int matchID;
	    public List<Player> mPlayers = new List<Player>();
        readonly ApiMethods apiMethods = new ApiMethods();
        public Button currentMatchId = new Button();
	    public LocalStorage LocalStorage = new LocalStorage();

        public TeamPlayersPage (Button currentMatchId)
        {
            this.currentMatchId = currentMatchId;
            matchID = int.Parse(currentMatchId.CommandParameter.ToString());
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent ();
        }

	    protected override async void OnAppearing()
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
	                
	                mPlayers = await apiMethods.GetMatchPlayers("GetMatchPlayers", matchID);
	                MatchPlayers = new FlowObservableCollection<Player>(mPlayers); //cast List<Player> to FlowObservableCollection
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

	    private async void PlayerVoteBtn_OnClicked(object sender, EventArgs e)
	    {
	        Button VoteBtn = (Button)sender;
	        int PlayerID = int.Parse(VoteBtn.CommandParameter.ToString());
	        Player selectedPlayer = MatchPlayers.FirstOrDefault(t => t.playerId == PlayerID);
	        Guid _DeviceId = await LocalStorage.GetCreateDeviceId();
            Vote vote = new Vote();
	        if (selectedPlayer != null)
	        {
	            vote.playerId = selectedPlayer.playerId;
	            vote.deviceId = _DeviceId.ToString();
            }

	        var answer = await DisplayAlert("Stem på Man of The Match?", $"Er du sikker på at du vil stemme på {selectedPlayer.playerFirstName} {selectedPlayer.playerLastName}", "Stem", "Annuller");
            //Player player = new Player();
	        if (!answer)
	        {
                //do nothing
	        }
	        else
	        {
	            //await apiMethods.PostVote("PostVote", matchID, vote);
	            await Navigation.PushAsync(new VoteReceipt(selectedPlayer));
            }
	        
	    }
	}
}