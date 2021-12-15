using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Papaya.Models;
using System.Net.Http;
using System.Net;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace Papaya
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        public class Respuesta
        {
            public string resultado { get; set; }

            public string token { get; set; }

            public string nombre { get; set; }

            public string userid { get; set; }

            public string msg { get; set; }
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Iniciando sesiòn"))
            {
                Login log = new Login
                {
                    correo = txtCorreo.Text,
                    password = txtPassword.Text
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/login.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(log);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == "exito")
                    {
                        Preferences.Set("token", resultado.token);
                        Preferences.Set("nombre", resultado.nombre);
                        Preferences.Set("userid", resultado.userid);
                        await Navigation.PushAsync(new IniDiag());
                    }
                    else if (resultado.resultado == "fallo")
                    {
                        await DisplayAlert("Mensaje", resultado.msg, "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Mensaje", "Datos invalidos", "OK");
                }
            }
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }

        void txtCorreo_Completed(System.Object sender, System.EventArgs e)
        {
            txtPassword.Focus();
        }
    }
}
