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
        readonly Authorization manager = new Authorization();
        readonly IList<Team> teams = new ObservableCollection<Team>();
        readonly IList<Match> teamMatches = new ObservableCollection<Match>();
        readonly ApiMethods apiMethods = new ApiMethods();

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

                    if (teamMatches.Count != 0)
                    {
                        foreach (Match match in team.teamMatches)
                        {
                            teamMatches.Add(match);
                        }
                    }

                }
                gameList.ItemsSource = teamMatches;
                BindingContext = teams;

                clubName.Text = currentClub.clubName;

            }
        }
}
