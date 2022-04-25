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
using System.IO;

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

        public class Root
        {
            public bool resultado { get; set; }

            public string id { get; set; }
        }

        public class Roots
        {
            public bool resultado { get; set; }

            public string msg { get; set; }
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                await DisplayAlert("Advertencia", "El campo correo esta vacio", "OK");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    await DisplayAlert("Advertencia", "El campo contraseña esta vacio", "OK");
                }
                else
                {
                    using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Iniciando sesiòn"))
                    {
                        try
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
                                    Loadad(resultado.token, resultado.nombre, resultado.userid);
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
                        catch (IOException ex)
                        {
                            Console.WriteLine(ex.Source);
                            await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
                        }
                    }
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

        void btnVerPass_Clicked(System.Object sender, System.EventArgs e)
        {
            txtPassword.IsPassword = !txtPassword.IsPassword;
            if (txtPassword.IsPassword)
            {
                btnVerPass.Source = "eye.png";
            }
            else
            {
                btnVerPass.Source = "noteye.png";
            }
        }

        public async void Loadad(string token, string nombre, string userid)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://www.bithives.com/PapayaApp/api/diag.php?kcal=0&idCliente=" + userid);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Root>(content);

                if (resultado.resultado)
                {
                    if (Convert.ToInt32(resultado.id) > 0 || resultado.id != null)
                    {
                        Preferences.Set("token", token);
                        Preferences.Set("nombre", nombre);
                        Preferences.Set("userid", userid);

                        var request2 = new HttpRequestMessage();
                        request2.RequestUri = new Uri("https://bithives.com/PapayaApp/api/helpers.php?continuar=1&cliente_id=" + userid);
                        request2.Method = HttpMethod.Get;
                        request2.Headers.Add("Accept", "application/json");
                        var client2 = new HttpClient();
                        HttpResponseMessage response2 = await client2.SendAsync(request2);
                        if (response2.StatusCode == HttpStatusCode.OK)
                        {
                            string content2 = await response2.Content.ReadAsStringAsync();

                            var resultado2 = JsonConvert.DeserializeObject<Roots>(content2);

                            if (resultado2.resultado)
                            {
                                await Navigation.PushAsync(new Home());
                                Application.Current.MainPage = new Home();
                            }
                            else
                            {
                                await Navigation.PushAsync(new IniDiag());
                            }
                        }                        
                    }
                    else
                    {
                        Preferences.Set("token", token);
                        Preferences.Set("nombre", nombre);
                        Preferences.Set("userid", userid);
                        await Navigation.PushAsync(new IniDiag());
                    }
                }
                else
                {
                    Preferences.Set("token", token);
                    Preferences.Set("nombre", nombre);
                    Preferences.Set("userid", userid);
                    await Navigation.PushAsync(new IniDiag());
                }
            }
        }
    }
}
