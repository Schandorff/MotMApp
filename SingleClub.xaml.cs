﻿using System;
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
        readonly IList<Sponsor> sponsors = new ObservableCollection<Sponsor>();
        public IList<Match> currentMatches = new ObservableCollection<Match>();
        public IList<Match> comingMatches = new ObservableCollection<Match>();
        public IList<Match> completedMatches = new ObservableCollection<Match>();
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
                gameList.ItemsSource = currentMatches;
                sponsorList.ItemsSource = sponsors;
                

                clubName.Text = currentClub.clubName;
                sponsorList.BackgroundColor = Color.FromHsla(255, 255, 255, 0.4);

            }
        private void currentMatchSorting(object sender, EventArgs e)
        {
            gameList.ItemsSource = currentMatches;
            current.FontSize = 22;
            current.TextColor = Color.White;
                coming.FontSize = 16;
                coming.TextColor = Color.Gray;
                completed.FontSize = 16;
                completed.TextColor = Color.Gray;
        }
        private void comingMatchSorting(object sender, EventArgs e){
            gameList.ItemsSource = comingMatches;
            coming.FontSize = 22;
            coming.TextColor = Color.White;
                current.FontSize = 16;
                current.TextColor = Color.Gray;
                completed.FontSize = 16;
                completed.TextColor = Color.Gray;

        }
        private void completedMatchSorting(object sender, EventArgs e)
        {
            gameList.ItemsSource = completedMatches;
            completed.FontSize = 22;
            completed.TextColor = Color.White;
                current.FontSize = 16;
                current.TextColor = Color.Gray;
                coming.FontSize = 16;
                coming.TextColor = Color.Gray;

        }
        }
}
