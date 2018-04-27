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
<<<<<<< HEAD
        readonly Authorization manager = new Authorization();
        readonly IList<Team> teams = new ObservableCollection<Team>();
        readonly IList<Match> teamMatches = new ObservableCollection<Match>();

=======
        readonly ApiMethods apiMethods = new ApiMethods();
        public IList<Team> teams = new ObservableCollection<Team>();
>>>>>>> b90ff636aa92857aa77afb90c8d2aa5e20756ecc

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
            requestedClub = await apiMethods.GetClub("GetCLub", currentClub.clubId);
            List<Team> ClubTeams = requestedClub.Teams;
            
            foreach (Team team in ClubTeams)
            {
                if (teams.All(b => b.teamId != team.teamId))
                    teams.Add(team);
                
<<<<<<< HEAD
               

                foreach (Match match in team.teamMatches)
                {
                    teamMatches.Add(match);

=======
                IList<Match> teamMatches = new ObservableCollection<Match>();
                if (teamMatches.Count != 0) { 
                    foreach (Match match in team.teamMatches)
                    {
                        teamMatches.Add(match);
                    }
>>>>>>> b90ff636aa92857aa77afb90c8d2aa5e20756ecc
                }

            }
            gameList.ItemsSource = teamMatches;
            BindingContext = teams;
<<<<<<< HEAD

            clubName.Text = currentClub.clubName;

=======
            clubName.Text = currentClub.clubName;            
>>>>>>> b90ff636aa92857aa77afb90c8d2aa5e20756ecc
        }
    }
}
