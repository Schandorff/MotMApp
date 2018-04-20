using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Manofthematch.Data;
using Manofthematch.Models;

namespace Manofthematch
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : ContentPage
	{
        Authorization authorization = new Authorization();
        readonly IList<Club> clubs = new ObservableCollection<Club>();
        readonly Authorization manager = new Authorization();
        

        public LandingPage ()
		{
            BindingContext = clubs;
            InitializeComponent ();   
		}

        async void OnRefresh(object sender, EventArgs e)
        {
            // Turn on network indicator
            this.IsBusy = true;

            try
            {
                var clubCollection = await manager.GetAllClubs("GetAllCLubs", 1083);

                foreach (Club club in clubCollection)
                {
                    if (clubs.All(b => b.clubId != club.clubId))
                        clubs.Add(club);
                }
            }
            finally
            {
                this.IsBusy = false;
            }
        }



    }



}