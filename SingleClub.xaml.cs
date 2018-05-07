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
    public partial class SingleClub : ContentPage
    {
        readonly Club currentClub;
        readonly Authorization manager = new Authorization();
        readonly FlowObservableCollection<Team> teams = new FlowObservableCollection<Team>();
        readonly IList<Sponsor> sponsors = new ObservableCollection<Sponsor>();
        public IList<Match> currentMatches = new ObservableCollection<Match>();
        public IList<Match> comingMatches = new ObservableCollection<Match>();
        public IList<Match> completedMatches = new ObservableCollection<Match>();
        readonly ApiMethods apiMethods = new ApiMethods();
        bool isInitialized = false;
        Club requestedClub = new Club();

        public SingleClub(Club currentClub)
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            this.currentClub = currentClub;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!isInitialized) { 
            requestedClub = await apiMethods.GetClub("GetCLub", currentClub.clubId);
            List<Team> ClubTeams = requestedClub.Teams;
            List<Sponsor> ClubSponsors = requestedClub.Sponsors;

            //add to teams list
            foreach (Team team in ClubTeams)
            {
                if (teams.All(b => b.teamId != team.teamId))
                    teams.Add(team);

                    //if (teamMatches.Count != 0)
                    //{
                        foreach (Match match in team.teamMatches)
                        {
                    
                                if (match.status == "Current"){
                                    currentMatches.Add(match);
                                }
                                else if (match.status == "Coming")
                                {
                                    comingMatches.Add(match);
                                }
                                else if (match.status == "Finished")
                                {
                                    completedMatches.Add(match);
                                }   

                        }
                    //}

                }

            // add to sponsor list
            foreach (Sponsor sponsor in ClubSponsors){
                if (sponsors.All(b => b.sponsorId != sponsor.sponsorId))
                    sponsors.Add(sponsor);
            }

                BindingContext = teams;
                clubTeamList.FlowItemsSource = teams;
                gameList.ItemsSource = currentMatches;
                sponsorList.ItemsSource = sponsors;



            Title = currentClub.clubName;
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
        private void comingMatchSorting(object sender, EventArgs e){
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
                await Navigation.PushAsync(new TeamPlayersPage(_sender));
            }
        }
    }
}
