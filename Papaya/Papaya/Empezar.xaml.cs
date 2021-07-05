using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empezar : ContentPage
    {
        public Empezar()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void btnPremiun_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
            Application.Current.MainPage = new Home();
        }
    }
}