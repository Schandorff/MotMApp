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
    public partial class FinishedMatch : ContentPage
    {
		public Button currentMatchId = new Button();
		public int matchID;
		readonly IList<Sponsor> sponsors = new ObservableCollection<Sponsor>();
		readonly ApiMethods apiMethods = new ApiMethods();
		bool isInitialized = false;
		Match requestedMatch = new Match();
        

		public FinishedMatch(Button currentMatchId)
        {
			this.currentMatchId = currentMatchId;
			matchID = int.Parse(currentMatchId.CommandParameter.ToString());
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent();
        }
		protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!isInitialized)
			{
				requestedMatch = await apiMethods.GetMatch("GetMatchByID", matchID);
				List<Sponsor> MatchSponsors = requestedMatch.matchSponsors;
				if (MatchSponsors.Count != 0)
                {
					foreach (Sponsor sponsor in MatchSponsors)
                    {
                        if (sponsors.All(b => b.sponsorId != sponsor.sponsorId))
                            sponsors.Add(sponsor);

                    }
                }
				BindingContext = requestedMatch;
				sponsorList.BackgroundColor = Color.FromHsla(255, 255, 255, 0.6);
				sponsorList.ItemsSource = sponsors;
				Title = requestedMatch.matchHomeTeam + " - " + requestedMatch.opponent;
			}

            isInitialized = true;
        }
    }
}
