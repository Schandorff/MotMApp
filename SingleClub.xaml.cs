using System;
using Manofthematch.Data;
using Manofthematch.Models;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Manofthematch
{
    public partial class SingleClub : ContentPage
    {
        readonly Club currentClub;
        readonly Authorization manager = new Authorization();
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

            requestedClub = await manager.GetClub("GetCLub", currentClub.clubId);
            BindingContext = requestedClub;
            
        }
    }
}
