using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Platillo : ContentPage
    {
        public Platillo(int idPlatillo)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            obtenerPlatillo(idPlatillo);
        }
        public class Respuesta
        {
            public bool resultado { get; set; }
            public string nombre { get; set; }
            public string procedimiento { get; set; }
            public string tiempo { get; set; }
            public string energia { get; set; }
            public string proteina { get; set; }
            public string carbohidratos { get; set; }
            public string grasas { get; set; }
            public string img { get; set; }
            public List<string> ingredientes { get; set; }
        }

        public async void obtenerPlatillo(int id)
        {
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando datos del platillo"))
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/platillo.php?idPlatillo=" + id);
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
                        Etiquetaimg.Source = "https://bithives.com/PapayaApp/" + resultado.img;
                        char del = '-';
                        string[] desa = resultado.nombre.Split(del);
                        lblNombre.Text = desa[0];
                        lblProcedimiento.Text = resultado.procedimiento;
                        lblTiempo.Text = "Tiempo de elaboracion: " + resultado.tiempo;
                        lblEnergia.Text = "Energia: " + resultado.energia;
                        lblProteinas.Text = "Proteinas: " + resultado.proteina;
                        lblCarbo.Text = "Carbohidratos: " + resultado.carbohidratos;
                        lblGrasas.Text = "Grasas: " + resultado.grasas;
                        foreach (string ingrediente in resultado.ingredientes)
                        {
                            Label lblprueba = new Label
                            {
                                Text = ingrediente,
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                HorizontalOptions = LayoutOptions.Center,
                                TextColor = Color.Black
                            };
                            stackIngredientes.Children.Add(lblprueba);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Algo fallo, recargue la pagina", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
        }
    }
}