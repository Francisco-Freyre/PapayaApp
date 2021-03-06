using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Objetivo : ContentPage
    {
        public Objetivo()
        {
            InitializeComponent();
        }

        public class Meta
        {
            public int idCliente { get; set; }

            public string meta { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }
        }

        private async void btnMantener_Clicked(object sender, EventArgs e)
        {
            try
            {
                Meta meta = new Meta
                {
                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    meta = "Mantener peso"
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
                        await Navigation.PushAsync(new Datos());
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Source);
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
            }
        }

        private async void btnPerder_Clicked(object sender, EventArgs e)
        {
            try
            {
                Meta meta = new Meta
                {
                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    meta = "Perder grasa"
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
                        await Navigation.PushAsync(new Datos());
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Source);
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
            }
        }
    }
}