using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Papaya.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Papaya
{
    public partial class Opciones : ContentPage
    {
        Dieta diet = new Dieta();
        DateTime fecha = DateTime.Today;
        DateTime dias = DateTime.Today;
        string dat;
        public Opciones()
        {
            InitializeComponent();
            listaDietas();
        }

        public class Dietas
        {
            public string id { get; set; }

            public string kcal { get; set; }
        }

        public class Respuesta1
        {
            public bool resultado { get; set; }

            public List<Dietas> dietas { get; set; }
        }

        public async void listaDietas()
        {
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Buscando posibles dietas"))
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php?dietas=" + Preferences.Get("kcal", ""));
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta1>(content);

                    if (resultado.resultado)
                    {
                        listaDeDietas.ItemsSource = resultado.dietas;
                    }
                }
            }
        }

        async void listaDeDietas_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Dietas seleccionado = e.SelectedItem as Dietas;
            //await DisplayAlert("Mensaje", seleccionado.kcal + ' ' + seleccionado.id, "OK");
            // Open a PopupPage
            await Navigation.PushPopupAsync(new Popup(Convert.ToInt32(seleccionado.id)));
        }
    }
}