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
            InitializeComponent();
        }

        //object Content = manager.GetContent();
        async void GetContent(object sender, EventArgs e)
        {
            object response = await manager.GetContent();
        }

	}
}
