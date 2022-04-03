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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ejercicios : ContentPage
    {
        public Ejercicios()
        {
            InitializeComponent();
            obtenerPlatillos();
        }

        public class Respuesta
        {
            public int id { get; set; }
            public string nombre { get; set; }

            public string descripcion { get; set; }

            public string img { get; set; }
        }

        public async void obtenerPlatillos()
        {
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/ejercicio.php?ejercicios=0");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<List<Respuesta>>(content);

                    listEjercicio.ItemsSource = resultado;
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

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}