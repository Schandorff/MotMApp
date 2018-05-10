using System;
using Manofthematch.Data;
using Manofthematch.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DLToolkit.Forms.Controls;
using DLToolkit.Forms;
using Plugin.Connectivity;
using System.Diagnostics;
using Xamarin.Forms;

namespace Manofthematch
{
    public partial class SingleTeam : ContentPage
    {
        readonly Team currentTeam;
        readonly Authorization manager = new Authorization();
		readonly IList<Sponsor> sponsors = new ObservableCollection<Sponsor>();
        public IList<Match> currentMatches = new ObservableCollection<Match>();
        public IList<Match> comingMatches = new ObservableCollection<Match>();
        public IList<Match> completedMatches = new ObservableCollection<Match>();
		readonly FlowObservableCollection<Player> players = new FlowObservableCollection<Player>();
        readonly ApiMethods apiMethods = new ApiMethods();
        bool isInitialized = false;
        Team requestedTeam = new Team();

        public SingleTeam(Team currentTeam)
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            this.currentTeam = currentTeam;
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!isInitialized)
                switch (currentTeam.teamSport)
                {
                    case "Soccer":
                        BackgroundImage = "FodboldBG.png";
                        break;
                    case "Handball":
                        BackgroundImage = "HandballBG.png";
                        break;
                    case "Tennis":
                        BackgroundImage = "TennisBG.png";
                        break;
                    case "Hockey":
                        BackgroundImage = "HockeyBG.png";
                        break;
                default:
                        break;
                }
            {
                requestedTeam = await apiMethods.GetTeam("GetTeam", currentTeam.teamId);
                List<Sponsor> TeamSponsors = requestedTeam.teamSponsors;
                List<Match> TeamMatches = requestedTeam.teamMatches;
                List<Player> TeamPlayers = requestedTeam.teamPlayers;

                if (TeamSponsors.Count != 0)
                {
                    foreach (Sponsor sponsor in TeamSponsors)
                    {
                        if (sponsors.All(b => b.sponsorId != sponsor.sponsorId))
                            sponsors.Add(sponsor);

                    }
                }

                if (TeamMatches.Count != 0)
                {
                    foreach (Match match in TeamMatches)
                    {

						if (match.status == "Current"){
                                    match.buttonColor = "#F8144E";
                                    match.buttonText = "STEM";
                                    currentMatches.Add(match);
                                }
                                else if (match.status == "Coming")
                                {
                                    match.buttonColor = "#F8144E";
                                    match.buttonText = "STEM";
                                    comingMatches.Add(match);
                                }
                                else if (match.status == "Finished")
                                {
                                    match.buttonColor = "#FF7F00";
                                    match.buttonText = "SE";
                                    completedMatches.Add(match);
                                }   

                    }
                }
                
                    foreach (Player player in TeamPlayers)
                    {
                        if (players.All(b => b.playerId != player.playerId))
                            players.Add(player);
                    }
                



                gameList.ItemsSource = currentMatches;
                sponsorList.ItemsSource = sponsors;
				playerList.ItemsSource = players;
               


                Title = currentTeam.TeamName;
                coming.TextColor = Color.FromHsla(255, 255, 255, 0.6);
                completed.TextColor = Color.FromHsla(255, 255, 255, 0.6);
                sponsorList.BackgroundColor = Color.FromHsla(255, 255, 255, 0.6);

            }

            isInitialized = true;
        }
        private void currentMatchSorting(object sender, EventArgs e)
        {
            gameList.ItemsSource = currentMatches;
            current.FontSize = 18;
            current.TextColor = Color.White;
            coming.FontSize = 14;
            coming.TextColor = Color.FromHsla(255, 255, 255, 0.6);
            completed.FontSize = 14;
            completed.TextColor = Color.FromHsla(255, 255, 255, 0.6);
        }
        private void comingMatchSorting(object sender, EventArgs e)
        {
            gameList.ItemsSource = comingMatches;
            coming.FontSize = 18;
            coming.TextColor = Color.White;
            current.FontSize = 14;
            current.TextColor = Color.FromHsla(255, 255, 255, 0.6);
            completed.FontSize = 14;
            completed.TextColor = Color.FromHsla(255, 255, 255, 0.6);

        }
        private void completedMatchSorting(object sender, EventArgs e)
        {
            gameList.ItemsSource = completedMatches;
            completed.FontSize = 18;
            completed.TextColor = Color.White;
            current.FontSize = 14;
            current.TextColor = Color.FromHsla(255, 255, 255, 0.6);
            coming.FontSize = 14;
            coming.TextColor = Color.FromHsla(255, 255, 255, 0.6);

        }

		async void MatchVoteBtn_OnClicked(object sender, EventArgs e)
        {
            Button _sender = (Button)sender;
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine($"No connection");
            }
            else
            {
                if (_sender.Text == "SE")
                {
                    await Navigation.PushAsync(new FinishedMatch(_sender));
                }
                else
                {
                    await Navigation.PushAsync(new TeamPlayersPage(_sender));
                }
            }
        }
    }
}
