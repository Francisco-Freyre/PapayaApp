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
    public partial class PesoObjetivo : ContentPage
    {
        public PesoObjetivo()
        {
            InitializeComponent();
            IMC();
        }

        public class Respuesta
        {
            public bool resultado { get; set; }

            public string peso { get; set; }

            public string altura { get; set; }
        }

        public class Meta
        {
            public int idCliente { get; set; }

            public string pesoMeta { get; set; }
        }

        public async void IMC()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php?idCliente=" + Preferences.Get("userid", ""));
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    lblPesoInicial.Text = resultado.peso + " Kg";
                    decimal altura = Convert.ToDecimal(resultado.altura);
                    decimal alturaCuadrada = altura * altura;
                    decimal rangoAlto = alturaCuadrada * Convert.ToDecimal(24.9);
                    decimal rangoBajo = alturaCuadrada * Convert.ToDecimal(18.5);
                    lblIMC.Text = "Segun la escala del IMC, tu peso debe estar entre " + Convert.ToString(rangoBajo) + "Kg - " + Convert.ToString(rangoAlto) + " Kg";
                    entryPesoObjetivo.Text = Convert.ToString(rangoAlto);
                }
                else
                {
                    await DisplayAlert("Mensaje", "Falso el valor", "OK");
                }
            }
            else
            {
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
            }
        }

        private void btnContinuar_Clicked(object sender, EventArgs e)
        {
            
        }

        private  void btnContinuar_Clicked_1(object sender, EventArgs e)
        {
            
        }

        private  void btnContinuar1_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            Meta meta = new Meta
            {
                idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                pesoMeta = entryPesoObjetivo.Text
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
                    await Navigation.PushAsync(new Bienvenida());
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
                    
        }
    }
}