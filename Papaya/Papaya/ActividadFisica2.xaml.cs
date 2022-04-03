using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Papaya
{
    public partial class ActividadFisica2 : ContentPage
    {
        public ActividadFisica2()
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

        async void btnSedentario_Clicked(System.Object sender, System.EventArgs e)
        {
            try
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
                        await Navigation.PushAsync(new Bienvenida2());
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

        void btnSedentario2_Clicked(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Sedentario", "Nada de ejercicio", "OK");
        }

        async void btnLigero_Clicked(System.Object sender, System.EventArgs e)
        {
            try
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
                        await Navigation.PushAsync(new Bienvenida2());
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

        void btnLigero2_Clicked(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Ligero", "Ejercicio 2-3 dias por semana", "OK");
        }

        async void btnModerado_Clicked(System.Object sender, System.EventArgs e)
        {
            try
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
                        await Navigation.PushAsync(new Bienvenida2());
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

        void btnModerado2_Clicked(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Moderado", "Ejercicio 4-5 dias por semana", "OK");
        }


        async void btnAlto_Clicked(System.Object sender, System.EventArgs e)
        {
            try
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
                        await Navigation.PushAsync(new Bienvenida2());
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

        void btnAlto2_Clicked(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Alto", "Ejercicio 6-7 dias por semana", "OK");
        }
    }
}
