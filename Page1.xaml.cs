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
using System.Diagnostics;
using Plugin.Connectivity.Abstractions;
using Plugin.Connectivity;
using Manofthematch;
using System.ComponentModel;

namespace Manofthematch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
			InitializeComponent ();
            
		}
	}
}