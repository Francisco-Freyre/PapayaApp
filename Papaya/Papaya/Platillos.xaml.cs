using Newtonsoft.Json;
using Papaya.Models;
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
using XF.Material.Forms.UI.Dialogs;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Platillos : ContentPage
    {
        public List<Respuesta> respuestas; 
        public Platillos()
        {
            InitializeComponent();
            obtenerPlatillos();
        }

        public class Respuesta
        {
            public int id { get; set; }

            public string nombre { get; set; }

            public string energia { get; set; }

            public string img { get; set; }
        }

        public async void obtenerPlatillos()
        {
            try
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Obteniendo platillos"))
                {
                    var request = new HttpRequestMessage();
                    request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/platillo.php?platillos=0");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");
                    var client = new HttpClient();
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        var resultado = JsonConvert.DeserializeObject<List<Respuesta>>(content);

                        respuestas = resultado;

                        listaPlatillos.ItemsSource = resultado;
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

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Respuesta seleccionado = e.SelectedItem as Respuesta;
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(seleccionado.id));
        }

        private void entryBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaPlatillos.ItemsSource = respuestas.Where(r => r.nombre.ToLower().Contains(entryBuscar.Text.ToLower()));
        }
    }
}