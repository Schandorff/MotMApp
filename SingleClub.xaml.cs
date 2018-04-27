using System;
using Manofthematch.Data;
using Manofthematch.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;

namespace Manofthematch
{
    public partial class SingleClub : ContentPage
    {
        readonly Club currentClub;
        readonly ApiMethods apiMethods = new ApiMethods();
        public IList<Team> teams = new ObservableCollection<Team>();

        Club requestedClub = new Club();
        //readonly IList<Club> clubs;
        //readonly Authorization manager;

        public SingleClub(Club currentClub)
        {
            this.currentClub = currentClub;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            requestedClub = await apiMethods.GetClub("GetCLub", currentClub.clubId);
            List<Team> ClubTeams = requestedClub.Teams;
            
            foreach (Team team in ClubTeams)
            {
                if (teams.All(b => b.teamId != team.teamId))
                    teams.Add(team);
                
                IList<Match> teamMatches = new ObservableCollection<Match>();
                if (teamMatches.Count != 0) { 
                    foreach (Match match in team.teamMatches)
                    {
                        teamMatches.Add(match);
                    }
                }
            }
            BindingContext = teams;
            clubName.Text = currentClub.clubName;            
        }
    }
}
