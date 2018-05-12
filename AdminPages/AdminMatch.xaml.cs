using System.Collections.Generic;
using Manofthematch.Data;
using Manofthematch.Models;
using Manofthematch.Controls;
using System.Diagnostics;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

//namespace Manofthematch.AdminPages
namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminMatch : ContentPage
	{
	    private Match currentMatch;
        ApiMethods apiMethods = new ApiMethods();

        public AdminMatch (Match currentMatch)
		{
		    NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
		    this.currentMatch = currentMatch;
		    InitializeComponent ();
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        StatusPicker.ItemsSource = MatchStatusList();
	        HomeTeamName.Text = currentMatch.matchHomeTeam;
	        HomeTeamScore.Text = currentMatch.homeGoal.ToString();
	        OpponentTeamName.Text = currentMatch.opponent;
	        OpponentTeamScore.Text = currentMatch.opponentGoal.ToString();
	        CurrentStatus.Text = MatchStatusTranslator(currentMatch.status);
	    }

	    private string MatchStatusTranslator(string status)
	    {
	        switch (status)
	        {
	            case "Coming":
	                return "Kommende";
	            case "Current":
	                return "Nuværende";
	            case "Finished":
	                return "Afsluttet";
	            case "Kommende":
	                return "Coming";
	            case "Nuværende":
	                return "Current";
	            case "Afsluttet":
	                return "Finished";
                default:
	                return "";
	        }
        }

	    private List<string> MatchStatusList()
	    {
            List<string> statusList = new List<string>();
            statusList.Add("Kommende");
	        statusList.Add("Nuværende");
	        statusList.Add("Afsluttet");
	        return statusList;
	    }

	    private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	    {
	        var picker = (Picker)sender;
	        int selectedIndex = picker.SelectedIndex;

	        if (selectedIndex != -1)
	        {
	            //CurrentStatus.Text = MatchStatusTranslator((string)picker.ItemsSource[selectedIndex]);
                currentMatch.status = MatchStatusTranslator((string)picker.ItemsSource[selectedIndex]);
            }

	        currentMatch = await apiMethods.UpdateMatchById("UpdateMatchById", currentMatch.matchId, currentMatch);
	        OnAppearing();
        }

	    private async void HomeGoalBtn_OnClicked(object sender, EventArgs e)
	    {
	        Button _sender = (Button)sender;
	        int HomeScore = currentMatch.homeGoal;

	        if (!CrossConnectivity.Current.IsConnected)
	        {
	            Debug.WriteLine($"No connection");
	        }
	        else
	        {
	            if (_sender.Text == "+")
	            {
	                HomeScore += 1;
	                currentMatch.homeGoal = HomeScore;
	            }
	            else
	            {
	                HomeScore -= 1;
	                currentMatch.homeGoal = HomeScore;
	                if (HomeScore < 0)
	                {
	                    HomeScore = 0;}
	                }
	            
	        }

	        currentMatch = await apiMethods.UpdateMatchById("UpdateMatchById", currentMatch.matchId, currentMatch);
            OnAppearing();
	    }
        
	    private async void OpponentGoalBtn_OnClicked(object sender, EventArgs e)
	    {
	        Button _sender = (Button)sender;
	        int OpponentScore = currentMatch.opponentGoal;
	        if (!CrossConnectivity.Current.IsConnected)
	        {
	            Debug.WriteLine($"No connection");
	        }
	        else
	        {
	            if (_sender.Text == "+")
	            {
	                OpponentScore += 1;
	                currentMatch.opponentGoal = OpponentScore;
	            }
	            else
	            {
	                OpponentScore -= 1;
	                currentMatch.opponentGoal = OpponentScore;
	                if (OpponentScore < 0)
	                {
	                    OpponentScore = 0;
	                }
	            }
            }

	        currentMatch = await apiMethods.UpdateMatchById("UpdateMatchById", currentMatch.matchId, currentMatch);
	        OnAppearing();
        }
	}
}