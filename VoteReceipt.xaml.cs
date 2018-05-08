using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manofthematch.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VoteReceipt : ContentPage
	{
        Player receivedPlayer = new Player();
		public VoteReceipt (Player receivedPlayer)
		{
		    this.receivedPlayer = receivedPlayer;
		    NavigationPage.SetHasNavigationBar(this, false); //remove default navigation
            InitializeComponent ();
		}

	    protected override async void OnAppearing()
	    {
	        firstName.Text = receivedPlayer.playerFirstName.ToUpper();
	        lastName.Text = receivedPlayer.playerLastName.ToUpper();
	    }

	    private async void _button_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();

        }
	}
}