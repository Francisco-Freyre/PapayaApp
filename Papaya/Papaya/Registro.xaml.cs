using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Papaya.Models;
using Xamarin.Essentials;
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

            public string token { get; set; }

            public string nombre { get; set; }

            public string userid { get; set; }
        } 

        private async void btnCrear_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                await DisplayAlert("Advertencia", "El campo nombre esta vacio", "OK");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    await DisplayAlert("Advertencia", "El campo apellido esta vacio", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(Convert.ToString(PickerEstado.SelectedItem)))
                    {
                        await DisplayAlert("Advertencia", "No se ha seleccionado ningun estado", "OK");
                    }
                    else
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
                                if (txtPassword.Text == txtPasswordConfirm.Text)
                                {
                                    Regi registro = new Regi
                                    {
                                        nombre = txtNombre.Text,
                                        apellido = txtApellido.Text,
                                        estado = Convert.ToString(PickerEstado.SelectedItem),
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
                                            txtCorreo.Text = "";
                                            txtPassword.Text = "";
                                            txtPasswordConfirm.Text = "";
                                            Preferences.Set("token", resultado.token);
                                            Preferences.Set("nombre", resultado.nombre);
                                            Preferences.Set("userid", resultado.userid);
                                            await Navigation.PushAsync(new IniDiag());
                                        }
                                        else
                                        {
                                            await DisplayAlert("Mensaje", "Fallo el registro, intente de nuevo", "OK");
                                        }
                                    }
                                    else
                                    {
                                        await DisplayAlert("Mensaje", "La conexion con el servidor fallo, intenta de nuevo", "OK");
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Mensaje", "La contraseña no coincide en la confirmacion", "OK");
                                }
                            }
                        }
                    }
                }
            }
        }

        void txtNombre_Completed(System.Object sender, System.EventArgs e)
        {
            txtApellido.Focus();
        }

        void txtApellido_Completed(System.Object sender, System.EventArgs e)
        {
            PickerEstado.Focus();
        }

        void PickerEstado_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            txtCorreo.Focus();
        }

        void txtCorreo_Completed(System.Object sender, System.EventArgs e)
        {
            bool isEmail = Regex.IsMatch(txtCorreo.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                DisplayAlert("Advertencia", "El formato del correo electrónico es incorrecto, revíselo e intente de nuevo.", "OK");
            }
            else
            {
                txtPassword.Focus();
            }
        }

        void txtPassword_Completed(System.Object sender, System.EventArgs e)
        {
            txtPasswordConfirm.Focus();
        }

        void txtPasswordConfirm_Completed(System.Object sender, System.EventArgs e)
        {

        }

        void txtPasswordConfirm_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                lbladvertencia.IsVisible = true;
                btnCrear.IsEnabled = false;
            }
            else
            {
                lbladvertencia.IsVisible = false;
                btnCrear.IsEnabled = true;
            }
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

        void btnPassConfirm_Clicked(System.Object sender, System.EventArgs e)
        {
            txtPasswordConfirm.IsPassword = !txtPasswordConfirm.IsPassword;
            if (txtPasswordConfirm.IsPassword)
            {
                btnPassConfirm.Source = "eye.png";
            }
            else
            {
                btnPassConfirm.Source = "noteye.png";
            }
        }
    }
}