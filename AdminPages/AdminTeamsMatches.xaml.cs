using System;
using Manofthematch.Data;
using Manofthematch.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DLToolkit.Forms.Controls;
using Plugin.Connectivity;
using System.Diagnostics;
using Manofthematch.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminTeamsMatches : ContentPage
	{
	    readonly Club currentClub;
	    readonly Authorization manager = new Authorization();
	    readonly FlowObservableCollection<Team> teams = new FlowObservableCollection<Team>();
	    readonly IList<Sponsor> sponsors = new ObservableCollection<Sponsor>();
	    public IList<Match> currentMatches = new ObservableCollection<Match>();
	    public IList<Match> comingMatches = new ObservableCollection<Match>();
	    public IList<Match> completedMatches = new ObservableCollection<Match>();
	    public List<Match> ClubMatches = new List<Match>();
        readonly ApiMethods apiMethods = new ApiMethods();
	    Club requestedClub = new Club();
        LocalStorage localStorage = new LocalStorage();
        

        public AdminTeamsMatches (Club currentClub)
		{
		    NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
		    this.currentClub = currentClub;
		    InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            InitializeComponent();
            Admin admin = await localStorage.GetAdminCredentials();
            switch (currentClub.clubSports[0])
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

            ClubMatches.Clear();
            currentMatches.Clear();
            comingMatches.Clear();
            completedMatches.Clear();

            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine($"No connection");
            }
            else
            {
                requestedClub = await apiMethods.GetClub("GetCLub", currentClub.clubId);
            }

            List<Team> ClubTeams = requestedClub.Teams;
            List<Sponsor> ClubSponsors = requestedClub.Sponsors;

            //add to teams list
            foreach (Team team in ClubTeams)
            {
                if (teams.All(b => b.teamId != team.teamId))
                    teams.Add(team);

                foreach (Match match in team.teamMatches)
                {
                    ClubMatches.Add(match);

                    if (match.status == "Current")
                    {
                        match.buttonColor = "#F8144E";
                        match.buttonText = "VÆLG";
                        currentMatches.Add(match);
                    }
                    else if (match.status == "Coming")
                    {
                        match.buttonColor = "#F8144E";
                        match.buttonText = "VÆLG";
                        comingMatches.Add(match);
                    }
                    else if (match.status == "Finished")
                    {
                        match.buttonColor = "#FF7F00";
                        match.buttonText = "VÆLG";
                        completedMatches.Add(match);
                    }
                }
            }

            // add to sponsor list
            foreach (Sponsor sponsor in ClubSponsors)
            {
                if (sponsors.All(b => b.sponsorId != sponsor.sponsorId))
                    sponsors.Add(sponsor);
            }

            BindingContext = teams;
            //clubTeamList.FlowItemsSource = teams;
            gameList.ItemsSource = currentMatches;
            sponsorList.ItemsSource = sponsors;

            Title = admin.UserFriendlyName;
            coming.TextColor = Color.FromHsla(255, 255, 255, 0.6);
            completed.TextColor = Color.FromHsla(255, 255, 255, 0.6);
            sponsorList.BackgroundColor = Color.FromHsla(255, 255, 255, 0.6);
            
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
        async void OnTeamSelect(object sender, ItemTappedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine($"No connection");
            }
            else
            {
                await Navigation.PushAsync(new SingleTeam((Team)e.Item));
            }
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
                Match match = new Match();
                foreach (var item in ClubMatches)
                {
                    if (item.matchId == int.Parse(_sender.CommandParameter.ToString()))
                    {
                        match = item;
                    }
                }
                await Navigation.PushAsync(new AdminMatch(match));
            }
        }
    }
}