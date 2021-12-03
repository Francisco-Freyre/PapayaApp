using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Papaya
{
    public partial class Apetito : ContentPage
    {
        double suave = 0.05, rapido = 0.10;
        public Apetito()
        {
            InitializeComponent();
        }

        public class porApetito
        {
            public int id { get; set; }

            public double apetito { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }
        }

        void btnBastante_Clicked(System.Object sender, System.EventArgs e)
        {
            lblPerdida.IsVisible = true;
            btnSuave.IsVisible = true;
            btnRapido.IsVisible = true;
            btnBastante.BackgroundColor = Color.DarkGray;
            btnPoca.BackgroundColor = Color.White;
            suave = 0.05;
            rapido = 0.10;
        }

        void btnPoca_Clicked(System.Object sender, System.EventArgs e)
        {
            lblPerdida.IsVisible = true;
            btnSuave.IsVisible = true;
            btnRapido.IsVisible = true;
            btnPoca.BackgroundColor = Color.DarkGray;
            btnBastante.BackgroundColor = Color.White;
            suave = 0.10;
            rapido = 0.15;
        }

        async void btnSuave_Clicked(System.Object sender, System.EventArgs e)
        {
            porApetito apetito = new porApetito
            {
                id = Convert.ToInt32(Preferences.Get("userid", "")),
                apetito = suave
            };

            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(apetito);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    await Navigation.PushAsync(new ActividadFisica());
                }
            }
        }

        async void btnRapido_Clicked(System.Object sender, System.EventArgs e)
        {
            porApetito apetito = new porApetito
            {
                id = Convert.ToInt32(Preferences.Get("userid", "")),
                apetito = rapido
            };

            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(apetito);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    await Navigation.PushAsync(new ActividadFisica());
                }
            }
        }
    }
}
