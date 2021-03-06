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
    public partial class Datos : ContentPage
    {
        public Datos()
        {
            InitializeComponent();
        }

        public class Meta
        {
            public int idCliente { get; set; }

            public decimal peso { get; set; }

            public decimal estatura { get; set; }

            public int edad { get; set; }

            public string sexo { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }
        }

        private async void btnContinuar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Convert.ToString(PickerEstatura.SelectedItem)))
            {
                await DisplayAlert("Advertencia", "No se ha seleccionado ninguna estatura", "OK");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(Convert.ToString(PickerPeso.SelectedItem)))
                {
                    await DisplayAlert("Advertencia", "No se ha seleccionado ningun peso", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(Convert.ToString(PickerSexo.SelectedItem)))
                    {
                        await DisplayAlert("Advertencia", "No se ha seleccionado ningun sexo", "OK");
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(Convert.ToString(fechaNacimiento.Date)))
                        {
                            await DisplayAlert("Advertencia", "No se ha seleccionado ninguna fecha", "OK");
                        }
                        else
                        {
                            try
                            {
                                Meta meta = new Meta
                                {
                                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                                    peso = Convert.ToDecimal(Convert.ToString(PickerPeso.SelectedItem).Replace(" Kg", "")),
                                    estatura = Convert.ToDecimal(Convert.ToString(PickerEstatura.SelectedItem).Replace(" Mts", "")),
                                    edad = DateTime.Today.AddTicks(-fechaNacimiento.Date.Ticks).Year - 1,
                                    sexo = Convert.ToString(PickerSexo.SelectedItem)
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
                                        await Navigation.PushAsync(new PesoObjetivo());
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
            }
        }

        private void PickerSexo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}