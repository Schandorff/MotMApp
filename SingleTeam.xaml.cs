using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Manofthematch.Data;
using Manofthematch.Models;

using Xamarin.Forms;

namespace Manofthematch
{
    public partial class SingleTeam : ContentPage
    {
        readonly Team currentTeam;
        readonly IList<Sponsor> sponsors = new ObservableCollection<Sponsor>();
        public IList<Match> currentMatches = new ObservableCollection<Match>();
        public IList<Match> comingMatches = new ObservableCollection<Match>();
        public IList<Match> completedMatches = new ObservableCollection<Match>();
        public SingleTeam(Team currentTeam)
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            this.currentTeam = currentTeam;
            InitializeComponent();
            BindingContext = currentTeam;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        //    Title = currentTeam.TeamName;
        //    List<Sponsor> ClubSponsors = currentTeam.Sponsors;

        //    //add to teams list
        //    foreach (Team team in ClubTeams)
        //    {
        //        if (teams.All(b => b.teamId != team.teamId))
        //            teams.Add(team);

        //        //if (teamMatches.Count != 0)
        //        //{
        //        foreach (Match match in team.teamMatches)
        //        {

        //            if (match.status == "Current")
        //            {
        //                currentMatches.Add(match);
        //            }
        //            else if (match.status == "Coming")
        //            {
        //                comingMatches.Add(match);
        //            }
        //            else if (match.status == "Finished")
        //            {
        //                completedMatches.Add(match);
        //            }

        //        }
        //        //}

        //    }

        //    // add to sponsor list
        //    foreach (Sponsor sponsor in ClubSponsors)
        //    {
        //        if (sponsors.All(b => b.sponsorId != sponsor.sponsorId))
        //            sponsors.Add(sponsor);
        //    }

        //    BindingContext = teams;
        //    clubTeamList.FlowItemsSource = teams;
        //    gameList.ItemsSource = currentMatches;
        //    sponsorList.ItemsSource = sponsors;



        //    Title = currentClub.clubName;
        //    coming.TextColor = Color.FromHsla(255, 255, 255, 0.6);
        //    completed.TextColor = Color.FromHsla(255, 255, 255, 0.6);
        //    sponsorList.BackgroundColor = Color.FromHsla(255, 255, 255, 0.6);

        }
    }
}
