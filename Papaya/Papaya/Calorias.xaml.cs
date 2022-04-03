using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calorias : ContentPage
    {
        int kcalorias = 0;
        
        public Calorias()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            kcal();
        }

        public class Dieta
        {
            public int idCliente { get; set; }

            public int kcal { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }

            public string peso { get; set; }

            public string altura { get; set; }

            public string actividad { get; set; }

            public string alcohol { get; set; }

            public string meta { get; set; }

            public string pesoMeta { get; set; }

            public string edad { get; set; }

            public string sexo { get; set; }

            public string apetito { get; set; }
        }

        public async void kcal()
        {
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php?calorias=0&idCliente=" + Preferences.Get("userid", ""));
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
                        decimal altura = Convert.ToDecimal(resultado.altura) * 100;
                        if (resultado.sexo == "Hombre")
                        {
                            decimal tmb = Convert.ToDecimal(66.5) + (Convert.ToDecimal(13.75) * Convert.ToDecimal(resultado.peso)) + (Convert.ToDecimal(5) * Convert.ToDecimal(altura)) - (Convert.ToDecimal(6.78) * Convert.ToDecimal(resultado.edad));

                            if (resultado.actividad == "Sedentario")
                            {
                                tmb = tmb * Convert.ToDecimal(1.1);
                            }
                            else if (resultado.actividad == "Ligero")
                            {
                                tmb = tmb * Convert.ToDecimal(1.2);
                            }
                            else if (resultado.actividad == "Moderado")
                            {
                                tmb = tmb * Convert.ToDecimal(1.3);
                            }
                            else if (resultado.actividad == "Alto")
                            {
                                tmb = tmb * Convert.ToDecimal(1.4);
                            }

                            lblCalorias.Text = "Tus calorias a consumir son " + Convert.ToString(Decimal.Round(tmb - (tmb * Convert.ToDecimal(resultado.apetito)))) + " kcal";
                            kcalorias = Convert.ToInt32(Decimal.Round(tmb - (tmb * Convert.ToDecimal(resultado.apetito))));
                            Preferences.Set("kcal", Convert.ToString(kcalorias));
                        }
                        else
                        {
                            decimal tmb = Convert.ToDecimal(655) + (Convert.ToDecimal(9.56) * Convert.ToDecimal(resultado.peso)) + (Convert.ToDecimal(1.85) * Convert.ToDecimal(altura)) - (Convert.ToDecimal(4.68) * Convert.ToDecimal(resultado.edad));

                            if (resultado.actividad == "Sedentario")
                            {
                                tmb = tmb * Convert.ToDecimal(1.1);
                            }
                            else if (resultado.actividad == "Ligero")
                            {
                                tmb = tmb * Convert.ToDecimal(1.2);
                            }
                            else if (resultado.actividad == "Moderado")
                            {
                                tmb = tmb * Convert.ToDecimal(1.3);
                            }
                            else if (resultado.actividad == "Alto")
                            {
                                tmb = tmb * Convert.ToDecimal(1.4);
                            }

                            lblCalorias.Text = "Tus calorias a consumir son " + Convert.ToString(Decimal.Round(tmb - (tmb * Convert.ToDecimal(resultado.apetito)))) + " kcal";
                            kcalorias = Convert.ToInt32(Decimal.Round(tmb - (tmb * Convert.ToDecimal(resultado.apetito))));
                            Preferences.Set("kcal", Convert.ToString(kcalorias));
                        }
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
            catch (IOException ex)
            {
                Console.WriteLine(ex.Source);
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
            }
        }

        private async void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            try
            {
                Dieta meta = new Dieta
                {
                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    kcal = kcalorias
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
                        await Navigation.PushAsync(new Opciones());
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