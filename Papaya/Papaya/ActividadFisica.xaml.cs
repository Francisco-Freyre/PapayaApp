using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActividadFisica : ContentPage
    {
        public ActividadFisica()
        {
            InitializeComponent();
        }

        public class Meta
        {
            public int idCliente { get; set; }

            public string actividad { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }
        }

        private async void btnSedentario_Clicked(object sender, EventArgs e)
        {
            Meta meta = new Meta
            {
                idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                actividad = "Sedentario"
            };

            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(meta);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    await Navigation.PushAsync(new Bebidas());
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
            
        }

        private async void btnLigero_Clicked(object sender, EventArgs e)
        {
            Meta meta = new Meta
            {
                idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                actividad = "Ligero"
            };

            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(meta);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    await Navigation.PushAsync(new Bebidas());
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
        }

        private async void btnModerado_Clicked(object sender, EventArgs e)
        {
            Meta meta = new Meta
            {
                idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                actividad = "Moderado"
            };

            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(meta);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    await Navigation.PushAsync(new Bebidas());
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
        }

        private async void btnAlto_Clicked(object sender, EventArgs e)
        {
            Meta meta = new Meta
            {
                idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                actividad = "Alto"
            };

            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(meta);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    await Navigation.PushAsync(new Bebidas());
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
        }

        private void btnSedentario2_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Sedentario", "Nada de ejercicio", "OK");
        }

        private void btnLigero2_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Ligero", "Ejercicio 2-3 dias por semana", "OK");
        }

        private void btnModerado2_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Moderado", "Ejercicio 4-5 dias por semana", "OK");
        }

        private void btnAlto_Clicked_1(object sender, EventArgs e)
        {

        }

        private void btnAlto2_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Alto", "Ejercicio 6-7 dias por semana", "OK");
        }
    }
}