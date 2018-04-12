using Manofthematch.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Manofthematch
{
    public partial class StartPage : ContentPage
    {
        readonly TeamManager manager = new TeamManager();
        TeamManager.AT Token = new TeamManager.AT();


        public StartPage()
        {
            //BindingContext = Token;
            InitializeComponent();



        }
        async void GetAuth(object sender, EventArgs e)
        {

            Token = await manager.GetToken();
            if (Token.AccesToken != null)
            {
                Label.Text = Token.AccesToken;
            }
            else{
                Label.Text = "Den var sgu tom";
            }
        }

	}
}
