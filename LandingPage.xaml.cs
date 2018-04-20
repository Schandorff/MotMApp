using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Manofthematch.Data;
using Manofthematch.Models;
using Manofthematch;

namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : ContentPage
	{
        Authorization authorization = new Authorization();
        //Task<AuthToken> token = authorization.GetToken();
        //string method = "GetAllClubs";
        

    public LandingPage ()
		{
            //Task.Run(async () => { await GetContent(); }).Wait();
            InitializeComponent ();   
		}

        

        //protected override async void OnStart()
        //{
        //    //await GetContent();
        //    //base.OnStart();
        //    await GetContent(sender, OnAppearing);
        //}

        //async void GetContent(object sender, EventArgs e)
        //{
        //    object response = await authorization.GetAllClubs("GetAllCLubs", 1083);
        //}

    }


}