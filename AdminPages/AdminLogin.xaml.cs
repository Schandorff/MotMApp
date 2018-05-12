using System;
using System.Collections.Generic;
using System.Diagnostics;
using Manofthematch.Data;
using Manofthematch.Models;
using Manofthematch.Controls;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Manofthematch
{
    public partial class AdminLogin : ContentPage
    {
        ApiMethods apiMethods = new ApiMethods();
        LocalStorage localStorage = new LocalStorage();
        
        public AdminLogin()
        {
            NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        async private void LoginBtn_OnClicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine($"No connection");
            }
            else
            {
                Admin clubAdmin = new Admin();
                clubAdmin.Username = username.Text;
                clubAdmin.Password = password.Text;
                eMessage.Text = "";
                if (clubAdmin.Username != "" && clubAdmin.Password != null)
                {
                    clubAdmin = await apiMethods.ClubAdminLogin("FindAdminClub", clubAdmin);
                    if (clubAdmin.ClubId != 0)
                    {
                        var temp = await localStorage.GetCreateAdminCredentials(clubAdmin);
                        var club = await apiMethods.GetClub("GetClub", clubAdmin.ClubId);
                        if (club.Teams.Count != 0)
                        {
                            await Navigation.PushAsync(new AdminTeamsMatches(club));
                        }
                    }
                    else
                    {
                        eMessage.Text = "Brugernavn eller adgangskode er forkert";
                    }
                }
                else
                {
                    eMessage.Text = "Udfyld venligst brugernavn og adgangskode";
                }
            }
        }
    }
}


