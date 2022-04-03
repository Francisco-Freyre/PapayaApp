using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Papaya
{
    public partial class Bienvenida2 : ContentPage
    {
        public Bienvenida2()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            lblSaludo.Text = "Hola " + Preferences.Get("nombre", "");
        }

        async void btnSiguiente_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Calorias());
        }
    }
}
