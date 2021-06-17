using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Papaya.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        public class Respuesta
        {
            public string resultado { get; set; }

            public string msg { get; set; }
        } 

        private async void btnCrear_Clicked(object sender, EventArgs e)
        {
            Regi registro = new Regi
            {
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                edad = Convert.ToInt16(txtEdad.Text),
                sexo = txtSexo.Text,
                email = txtCorreo.Text,
                password = txtPassword.Text
            };

            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/registro.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(registro);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado == "exito")
                {
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtEdad.Text = "";
                    txtCorreo.Text = "";
                    txtSexo.Text = "";
                    txtPassword.Text = "";
                    await DisplayAlert("Mensaje", resultado.msg, "OK");
                }
                else
                {
                    await DisplayAlert("Mensaje", resultado.msg, "OK");
                }
            }
            else
            {
                await DisplayAlert("Mensaje", "La conexion con el servidor fallo, intenta de nuevo", "OK");
            }

        }
    }
}