using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Papaya.Models;
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

        public class Checks
        {
            public string fecha { get; set; }

            public string check1 { get; set; }

            public string check2 { get; set; }

            public string check3 { get; set; }

            public string check4 { get; set; }

            public string check5 { get; set; }

            public bool fechamayor { get; set; }
        }

        public class Root
        {
            public bool resultado { get; set; }

            public Checks checks { get; set; }
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

        public async void GetChecks()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php?checks=1&cliente_id=" + Preferences.Get("userid", ""));
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Root>(content);

                if (resultado.resultado)
                {

                    if (resultado.checks.fechamayor)
                    {
                        btnProgreso.IsVisible = resultado.checks.fechamayor;
                    }
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
                }
            }
        }

        async void btnAvances_Clicked(System.Object sender, System.EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Avances());
        }
    }
}