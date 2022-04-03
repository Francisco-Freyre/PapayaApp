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
    public partial class Master : ContentPage
    {
        public Master()
        {
            InitializeComponent();
            LblNombre.Text = "Bienvenido " + Preferences.Get("nombre", "");
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnPerfil_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Perfil());
        }

        private async void btnPlatillos_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillos());
        }

        private async void btnDieta_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Midieta());
        }

        private async void btnEjer_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Ejercicios());
        }

        private async void btnCerrar_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("token");
            Preferences.Remove("nombre");
            Preferences.Remove("userid");
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new MainPage());
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        async void btnConfig_Clicked(System.Object sender, System.EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Config());
        }

        async void btnProgreso_Clicked(System.Object sender, System.EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new IniPro());
        }
    }
}