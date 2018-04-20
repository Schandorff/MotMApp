using Manofthematch.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Manofthematch.Models;

namespace Manofthematch
{
    public partial class StartPage : ContentPage
    {
        readonly Authorization manager = new Authorization();
        AuthToken Token = new AuthToken();


        public StartPage()
        {
            InitializeComponent();
        }

        //object Content = manager.GetContent();
        //async void GetContent(object sender, EventArgs e)
        //{
        //    object response = await manager.GetContent("boo");
        //}

	}
}
