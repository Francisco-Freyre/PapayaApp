using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bienvenida : ContentPage
    {
        public Bienvenida()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            lblSaludo.Text = "Hola " + Preferences.Get("nombre", "");
        }

        private async void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Calorias());
        }
    }
}