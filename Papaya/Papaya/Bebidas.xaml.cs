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
    public partial class Bebidas : ContentPage
    {
        public bool cerveza, tequila, wisky, ron, vino, mezcal, sidra, vodka, pulque, ginebra, brandy, champa, preparados;
        public string frec;
        public Bebidas()
        {
            InitializeComponent();
        }

        public class Alcohol
        {
            public int idCliente { get; set; }

            public string alcohol { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }
        }

        private async void btnContinuar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Alcohol alcohol = new Alcohol
                {
                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    alcohol = frec
                };
                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(alcohol);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado)
                    {
                        await Navigation.PushAsync(new Alimentos());
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
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
            }
            
        }

        private void btnSi_Clicked(object sender, EventArgs e)
        {
            GridFrecuencia.IsVisible = true;
        }

        private void PickerFrecuencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            frec = (string)PickerFrecuencia.ItemsSource[PickerFrecuencia.SelectedIndex];
            gridBebidas.IsVisible = true;
            gridBebidas.IsVisible = true;
        }

        private async void btnNo_Clicked(object sender, EventArgs e)
        {
            try
            {
                Alcohol alcohol = new Alcohol
                {
                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    alcohol = "No"
                };
                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(alcohol);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado)
                    {
                        await Navigation.PushAsync(new Alimentos());
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
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
            }

        }

        private void btnCerveza_Clicked(object sender, EventArgs e)
        {
            if (cerveza)
            {
                btnCerveza.BorderColor = Color.White;
                cerveza = false;
            }
            else
            {
                btnCerveza.BorderColor = Color.Gold;
                cerveza = true;
            }
        }

        private void btnWiski_Clicked(object sender, EventArgs e)
        {
            if (wisky)
            {
                btnWiski.BorderColor = Color.White;
                wisky = false;
            }
            else
            {
                btnWiski.BorderColor = Color.Gold;
                wisky = true;
            }
        }

        private void btnTequila_Clicked(object sender, EventArgs e)
        {
            if (tequila)
            {
                btnTequila.BorderColor = Color.White;
                tequila = false;
            }
            else
            {
                btnTequila.BorderColor = Color.Gold;
                tequila = true;
            }
        }

        private void btnRon_Clicked(object sender, EventArgs e)
        {
            if (ron)
            {
                btnRon.BorderColor = Color.White;
                ron = false;
            }
            else
            {
                btnRon.BorderColor = Color.Gold;
                ron = true;
            }
        }

        private void btnVino_Clicked(object sender, EventArgs e)
        {
            if (vino)
            {
                btnVino.BorderColor = Color.White;
                vino = false;
            }
            else
            {
                btnVino.BorderColor = Color.Gold;
                vino = true;
            }
        }

        private void btnMezcal_Clicked(object sender, EventArgs e)
        {
            if (mezcal)
            {
                btnMezcal.BorderColor = Color.White;
                mezcal = false;
            }
            else
            {
                btnMezcal.BorderColor = Color.Gold;
                mezcal = true;
            }
        }

        private void btnSidra_Clicked(object sender, EventArgs e)
        {
            if (sidra)
            {
                btnSidra.BorderColor = Color.White;
                sidra = false;
            }
            else
            {
                btnSidra.BorderColor = Color.Gold;
                sidra = true;
            }
        }

        private void btnVodka_Clicked(object sender, EventArgs e)
        {
            if (vodka)
            {
                btnVodka.BorderColor = Color.White;
                vodka = false;
            }
            else
            {
                btnVodka.BorderColor = Color.Gold;
                vodka = true;
            }
        }

        private void btnPulque_Clicked(object sender, EventArgs e)
        {
            if (pulque)
            {
                btnPulque.BorderColor = Color.White;
                pulque = false;
            }
            else
            {
                btnPulque.BorderColor = Color.Gold;
                pulque = true;
            }
        }

        private void btnGinebra_Clicked(object sender, EventArgs e)
        {
            if (ginebra)
            {
                btnGinebra.BorderColor = Color.White;
                ginebra = false;
            }
            else
            {
                btnGinebra.BorderColor = Color.Gold;
                ginebra = true;
            }
        }

        private void btnBrandy_Clicked(object sender, EventArgs e)
        {
            if (brandy)
            {
                btnBrandy.BorderColor = Color.White;
                brandy = false;
            }
            else
            {
                btnBrandy.BorderColor = Color.Gold;
                brandy = true;
            }
        }

        private void btnChamp_Clicked(object sender, EventArgs e)
        {
            if (champa)
            {
                btnChamp.BorderColor = Color.White;
                champa = false;
            }
            else
            {
                btnChamp.BorderColor = Color.Gold;
                champa = true;
            }
        }

        private void btnPreparadas_Clicked(object sender, EventArgs e)
        {
            if (preparados)
            {
                btnPreparadas.BorderColor = Color.White;
                preparados = false;
            }
            else
            {
                btnPreparadas.BorderColor = Color.Gold;
                preparados = true;
            }
        }
    }
}