using Newtonsoft.Json;
using Papaya.Models;
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

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Respuesta seleccionado = e.SelectedItem as Respuesta;
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(seleccionado.id));
        }

        private void entryBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaPlatillos.ItemsSource = respuestas.Where(r => r.nombre.Contains(entryBuscar.Text));
        }
    }
}