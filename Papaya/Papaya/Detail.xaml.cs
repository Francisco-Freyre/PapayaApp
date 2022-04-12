using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Papaya.Models;
using XF.Material.Forms.UI.Dialogs;
using System.IO;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Detail : ContentPage
    {
        Dieta diet = new Dieta();
        DateTime fecha = DateTime.Today;
        DateTime dias = DateTime.Today;
        int numDia, idDesayuno = 0, idColacion = 0, idColacion2 = 0, idComida = 0, idCena = 0, dia;
        public Detail()
        {
            InitializeComponent();
            GetChecks();
            lblFecha.Text = fecha.ToString("dddd").ToUpper();
            numDia = (int)fecha.DayOfWeek;
            dia = numDia;
            dieta();
        }

        public class Datos
        {
            public string desayuno { get; set; }

            public string colacion { get; set; }

            public string colacion2 { get; set; }

            public string comida { get; set; }

            public string cena { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }

            public int id_des { get; set; }

            public string img_des { get; set; }

            public string kcal_des { get; set; }

            public int id_col { get; set; }

            public string img_col { get; set; }

            public string kcal_col { get; set; }

            public int id_col2 { get; set; }

            public string img_col2 { get; set; }

            public string kcal_col2 { get; set; }

            public int id_com { get; set; }

            public string img_com { get; set; }

            public string kcal_com { get; set; }

            public int id_cen { get; set; }

            public string img_cen { get; set; }

            public string kcal_cen { get; set; }
        };

        public class Checks
        {
            public string fecha { get; set; }

            public string check1 { get; set; }

            public string check2 { get; set; }

            public string check3 { get; set; }

            public string check4 { get; set; }

            public string check5 { get; set; }

            public bool fechamayor { get; set; }
        }

        public class Root
        {
            public bool resultado { get; set; }

            public Checks checks { get; set; }
        }

        public class Enviar
        {
            public bool check { get; set; }

            public int id_cliente { get; set; }

            public string numCheck { get; set; }

            public string valor { get; set; }
        }

        public class Verify
        {
            public bool resultado { get; set; }

            public string id { get; set; }
        }

        public async void GetChecks()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php?checks=1&cliente_id=" + Preferences.Get("userid", ""));
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
                    if (resultado.checks.check1 == "0")
                    {
                        checkDes.IsChecked = false;
                    }
                    else if (resultado.checks.check1 == "1")
                    {
                        checkDes.IsChecked = true;
                    }
                    if (resultado.checks.check2 == "0")
                    {
                        checkCol1.IsChecked = false;
                    }
                    else if (resultado.checks.check2 == "1")
                    {
                        checkCol1.IsChecked = true;
                    }
                    if (resultado.checks.check3 == "0")
                    {
                        checkCom.IsChecked = false;
                    }
                    else if (resultado.checks.check3 == "1")
                    {
                        checkCom.IsChecked = true;
                    }
                    if (resultado.checks.check4 == "0")
                    {
                        checkCol2.IsChecked = false;
                    }
                    else if (resultado.checks.check4 == "1")
                    {
                        checkCol2.IsChecked = true;
                    }
                    if (resultado.checks.check5 == "0")
                    {
                        checkCen.IsChecked = false;
                    }
                    else if (resultado.checks.check5 == "1")
                    {
                        checkCen.IsChecked = true;
                    }

                    if (resultado.checks.fechamayor)
                    {
                        var answer = await DisplayAlert("¡Felicidades!", "Has completado dos semanas con tu dieta, veamos los resultados", "¡Vamos!", "Luego");
                        if (answer)
                        {
                            App.MasterDet.IsPresented = false;
                            await App.MasterDet.Detail.Navigation.PushAsync(new IniPro());
                        }
                    }
                }
                else
                {
                    App.MasterDet.IsPresented = false;
                    await App.MasterDet.Detail.Navigation.PushAsync(new IniDiag());
                    Application.Current.MainPage = new NavigationPage(new IniDiag());
                }
            }
        }

        public async void dieta()
        {
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Obteniendo datos"))
            {
                try
                {
                    var request = new HttpRequestMessage();
                    request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php?kcal=0&idCliente=" + Preferences.Get("userid", ""));
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");
                    var client = new HttpClient();
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        var resultado = JsonConvert.DeserializeObject<Dieta>(content);

                        if (resultado.resultado)
                        {
                            this.diet = resultado;
                            switch (numDia)
                            {
                                case 0:
                                    cardDes.IsVisible = false;
                                    cardCol.IsVisible = false;
                                    cardCom.IsVisible = false;
                                    cardCol2.IsVisible = false;
                                    cardCen.IsVisible = false;
                                    cardDomingo.IsVisible = true;
                                    break;

                                case 1:
                                    var coms = comidas(resultado.d_lunes, resultado.c_lunes, resultado.c2_lunes, resultado.co_lunes, resultado.ce_lunes);
                                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                                    platillo(cos0[0], cos0[1], cos0[2], cos0[3], cos0[4], cos0[5], cos0[6], cos0[7], cos0[8], cos0[9]);
                                    lblTitleDes.Text = cos0[10];
                                    lblColcacion.Text = cos0[11];
                                    lblColacion2.Text = cos0[12];
                                    lblComida.Text = cos0[13];
                                    lblCena.Text = cos0[14];
                                    break;

                                case 2:
                                    var coms2 = comidas(resultado.d_martes, resultado.c_martes, resultado.c2_martes, resultado.co_martes, resultado.ce_martes);
                                    var cos = individual(coms2[0], coms2[1], coms2[2], coms2[3], coms2[4]);
                                    platillo(cos[0], cos[1], cos[2], cos[3], cos[4], cos[5], cos[6], cos[7], cos[8], cos[9]);
                                    lblTitleDes.Text = cos[10];
                                    lblColcacion.Text = cos[11];
                                    lblColacion2.Text = cos[12];
                                    lblComida.Text = cos[13];
                                    lblCena.Text = cos[14];
                                    break;

                                case 3:
                                    var coms3 = comidas(resultado.d_miercoles, resultado.c_miercoles, resultado.c2_miercoles, resultado.co_miercoles, resultado.ce_miercoles);
                                    var cos2 = individual(coms3[0], coms3[1], coms3[2], coms3[3], coms3[4]);
                                    platillo(cos2[0], cos2[1], cos2[2], cos2[3], cos2[4], cos2[5], cos2[6], cos2[7], cos2[8], cos2[9]);
                                    lblTitleDes.Text = cos2[10];
                                    lblColcacion.Text = cos2[11];
                                    lblColacion2.Text = cos2[12];
                                    lblComida.Text = cos2[13];
                                    lblCena.Text = cos2[14];
                                    break;

                                case 4:
                                    var coms4 = comidas(resultado.d_jueves, resultado.c_jueves, resultado.c2_jueves, resultado.co_jueves, resultado.ce_jueves);
                                    var cos3 = individual(coms4[0], coms4[1], coms4[2], coms4[3], coms4[4]);
                                    platillo(cos3[0], cos3[1], cos3[2], cos3[3], cos3[4], cos3[5], cos3[6], cos3[7], cos3[8], cos3[9]);
                                    lblTitleDes.Text = cos3[10];
                                    lblColcacion.Text = cos3[11];
                                    lblColacion2.Text = cos3[12];
                                    lblComida.Text = cos3[13];
                                    lblCena.Text = cos3[14];
                                    break;

                                case 5:
                                    var coms5 = comidas(resultado.d_viernes, resultado.c_viernes, resultado.c2_viernes, resultado.co_viernes, resultado.ce_viernes);
                                    var cos4 = individual(coms5[0], coms5[1], coms5[2], coms5[3], coms5[4]);
                                    platillo(cos4[0], cos4[1], cos4[2], cos4[3], cos4[4], cos4[5], cos4[6], cos4[7], cos4[8], cos4[9]);
                                    lblTitleDes.Text = cos4[10];
                                    lblColcacion.Text = cos4[11];
                                    lblColacion2.Text = cos4[12];
                                    lblComida.Text = cos4[13];
                                    lblCena.Text = cos4[14];
                                    break;

                                case 6:
                                    var coms6 = comidas(resultado.d_sabado, resultado.c_sabado, resultado.c2_sabado, resultado.co_sabado, resultado.ce_sabado);
                                    var cos5 = individual(coms6[0], coms6[1], coms6[2], coms6[3], coms6[4]);
                                    platillo(cos5[0], cos5[1], cos5[2], cos5[3], cos5[4], cos5[5], cos5[6], cos5[7], cos5[8], cos5[9]);
                                    lblTitleDes.Text = cos5[10];
                                    lblColcacion.Text = cos5[11];
                                    lblColacion2.Text = cos5[12];
                                    lblComida.Text = cos5[13];
                                    lblCena.Text = cos5[14];
                                    break;
                            }
                            //await DisplayAlert("Mensaje", resultado.kcal + resultado.categoria + resultado.descripcion, "OK");
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

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private async void btnDesayuno_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idDesayuno));
        }

        private async void cardDes_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idDesayuno));
        }

        private async void cardCol_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idColacion));
        }

        private async void cardCom_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idComida));
        }

        private async void cardCol2_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idColacion2));
        }

        private async void cardCen_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idCena));
        }

        private void btnDiaAnterior_Clicked(object sender, EventArgs e)
        {
            dias = dias.AddDays(-1);
            int numerodias = (int)dias.DayOfWeek;
            if (numerodias == dia)
            {
                checkDes.IsVisible = true;
                checkCol1.IsVisible = true;
                checkCom.IsVisible = true;
                checkCol2.IsVisible = true;
                checkCen.IsVisible = true;
            }
            else
            {
                checkDes.IsVisible = false;
                checkCol1.IsVisible = false;
                checkCom.IsVisible = false;
                checkCol2.IsVisible = false;
                checkCen.IsVisible = false;
            }
            switch (numerodias)
            {
                case 0:
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    cardDes.IsVisible = false;
                    cardCol.IsVisible = false;
                    cardCom.IsVisible = false;
                    cardCol2.IsVisible = false;
                    cardCen.IsVisible = false;
                    cardDomingo.IsVisible = true;
                    break;

                case 1:
                    var coms = comidas(diet.d_lunes, diet.c_lunes, diet.c2_lunes, diet.co_lunes, diet.ce_lunes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                    platillo(cos0[0], cos0[1], cos0[2], cos0[3], cos0[4], cos0[5], cos0[6], cos0[7], cos0[8], cos0[9]);
                    lblTitleDes.Text = cos0[10];
                    lblColcacion.Text = cos0[11];
                    lblColacion2.Text = cos0[12];
                    lblComida.Text = cos0[13];
                    lblCena.Text = cos0[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCen.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;

                case 2:
                    var coms2 = comidas(diet.d_martes, diet.c_martes, diet.c2_martes, diet.co_martes, diet.ce_martes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos = individual(coms2[0], coms2[1], coms2[2], coms2[3], coms2[4]);
                    platillo(cos[0], cos[1], cos[2], cos[3], cos[4], cos[5], cos[6], cos[7], cos[8], cos[9]);
                    lblTitleDes.Text = cos[10];
                    lblColcacion.Text = cos[11];
                    lblColacion2.Text = cos[12];
                    lblComida.Text = cos[13];
                    lblCena.Text = cos[14];
                    break;

                case 3:
                    var coms3 = comidas(diet.d_miercoles, diet.c_miercoles, diet.c2_miercoles, diet.co_miercoles, diet.ce_miercoles);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos2 = individual(coms3[0], coms3[1], coms3[2], coms3[3], coms3[4]);
                    platillo(cos2[0], cos2[1], cos2[2], cos2[3], cos2[4], cos2[5], cos2[6], cos2[7], cos2[8], cos2[9]);
                    lblTitleDes.Text = cos2[10];
                    lblColcacion.Text = cos2[11];
                    lblColacion2.Text = cos2[12];
                    lblComida.Text = cos2[13];
                    lblCena.Text = cos2[14];
                    break;

                case 4:
                    var coms4 = comidas(diet.d_jueves, diet.c_jueves, diet.c2_jueves, diet.co_jueves, diet.ce_jueves);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos3 = individual(coms4[0], coms4[1], coms4[2], coms4[3], coms4[4]);
                    platillo(cos3[0], cos3[1], cos3[2], cos3[3], cos3[4], cos3[5], cos3[6], cos3[7], cos3[8], cos3[9]);
                    lblTitleDes.Text = cos3[10];
                    lblColcacion.Text = cos3[11];
                    lblColacion2.Text = cos3[12];
                    lblComida.Text = cos3[13];
                    lblCena.Text = cos3[14];
                    break;

                case 5:
                    var coms5 = comidas(diet.d_viernes, diet.c_viernes, diet.c2_viernes, diet.co_viernes, diet.ce_viernes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos4 = individual(coms5[0], coms5[1], coms5[2], coms5[3], coms5[4]);
                    platillo(cos4[0], cos4[1], cos4[2], cos4[3], cos4[4], cos4[5], cos4[6], cos4[7], cos4[8], cos4[9]);
                    lblTitleDes.Text = cos4[10];
                    lblColcacion.Text = cos4[11];
                    lblColacion2.Text = cos4[12];
                    lblComida.Text = cos4[13];
                    lblCena.Text = cos4[14];
                    break;

                case 6:
                    var coms6 = comidas(diet.d_sabado, diet.c_sabado, diet.c2_sabado, diet.co_sabado, diet.ce_sabado);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos5 = individual(coms6[0], coms6[1], coms6[2], coms6[3], coms6[4]);
                    platillo(cos5[0], cos5[1], cos5[2], cos5[3], cos5[4], cos5[5], cos5[6], cos5[7], cos5[8], cos5[9]);
                    lblTitleDes.Text = cos5[10];
                    lblColcacion.Text = cos5[11];
                    lblColacion2.Text = cos5[12];
                    lblComida.Text = cos5[13];
                    lblCena.Text = cos5[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCen.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;
            }
        }

        private void btnDiaSiguiente_Clicked(object sender, EventArgs e)
        {
            dias = dias.AddDays(1);
            int numerodias = (int)dias.DayOfWeek;
            if (numerodias == dia)
            {
                checkDes.IsVisible = true;
                checkCol1.IsVisible = true;
                checkCom.IsVisible = true;
                checkCol2.IsVisible = true;
                checkCen.IsVisible = true;
            }
            else
            {
                checkDes.IsVisible = false;
                checkCol1.IsVisible = false;
                checkCom.IsVisible = false;
                checkCol2.IsVisible = false;
                checkCen.IsVisible = false;
            }
            switch (numerodias)
            {
                case 0:
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    cardDes.IsVisible = false;
                    cardCol.IsVisible = false;
                    cardCom.IsVisible = false;
                    cardCol2.IsVisible = false;
                    cardCen.IsVisible = false;
                    cardDomingo.IsVisible = true;
                    break;

                case 1:
                    var coms = comidas(diet.d_lunes, diet.c_lunes, diet.c2_lunes, diet.co_lunes, diet.ce_lunes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                    platillo(cos0[0], cos0[1], cos0[2], cos0[3], cos0[4], cos0[5], cos0[6], cos0[7], cos0[8], cos0[9]);
                    lblTitleDes.Text = cos0[10];
                    lblColcacion.Text = cos0[11];
                    lblColacion2.Text = cos0[12];
                    lblComida.Text = cos0[13];
                    lblCena.Text = cos0[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCen.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;

                case 2:
                    var coms2 = comidas(diet.d_martes, diet.c_martes, diet.c2_martes, diet.co_martes, diet.ce_martes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos = individual(coms2[0], coms2[1], coms2[2], coms2[3], coms2[4]);
                    platillo(cos[0], cos[1], cos[2], cos[3], cos[4], cos[5], cos[6], cos[7], cos[8], cos[9]);
                    lblTitleDes.Text = cos[10];
                    lblColcacion.Text = cos[11];
                    lblColacion2.Text = cos[12];
                    lblComida.Text = cos[13];
                    lblCena.Text = cos[14];
                    break;

                case 3:
                    var coms3 = comidas(diet.d_miercoles, diet.c_miercoles, diet.c2_miercoles, diet.co_miercoles, diet.ce_miercoles);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos2 = individual(coms3[0], coms3[1], coms3[2], coms3[3], coms3[4]);
                    platillo(cos2[0], cos2[1], cos2[2], cos2[3], cos2[4], cos2[5], cos2[6], cos2[7], cos2[8], cos2[9]);
                    lblTitleDes.Text = cos2[10];
                    lblColcacion.Text = cos2[11];
                    lblColacion2.Text = cos2[12];
                    lblComida.Text = cos2[13];
                    lblCena.Text = cos2[14];
                    break;

                case 4:
                    var coms4 = comidas(diet.d_jueves, diet.c_jueves, diet.c2_jueves, diet.co_jueves, diet.ce_jueves);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos3 = individual(coms4[0], coms4[1], coms4[2], coms4[3], coms4[4]);
                    platillo(cos3[0], cos3[1], cos3[2], cos3[3], cos3[4], cos3[5], cos3[6], cos3[7], cos3[8], cos3[9]);
                    lblTitleDes.Text = cos3[10];
                    lblColcacion.Text = cos3[11];
                    lblColacion2.Text = cos3[12];
                    lblComida.Text = cos3[13];
                    lblCena.Text = cos3[14];
                    break;

                case 5:
                    var coms5 = comidas(diet.d_viernes, diet.c_viernes, diet.c2_viernes, diet.co_viernes, diet.ce_viernes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos4 = individual(coms5[0], coms5[1], coms5[2], coms5[3], coms5[4]);
                    platillo(cos4[0], cos4[1], cos4[2], cos4[3], cos4[4], cos4[5], cos4[6], cos4[7], cos4[8], cos4[9]);
                    lblTitleDes.Text = cos4[10];
                    lblColcacion.Text = cos4[11];
                    lblColacion2.Text = cos4[12];
                    lblComida.Text = cos4[13];
                    lblCena.Text = cos4[14];
                    break;

                case 6:
                    var coms6 = comidas(diet.d_sabado, diet.c_sabado, diet.c2_sabado, diet.co_sabado, diet.ce_sabado);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos5 = individual(coms6[0], coms6[1], coms6[2], coms6[3], coms6[4]);
                    platillo(cos5[0], cos5[1], cos5[2], cos5[3], cos5[4], cos5[5], cos5[6], cos5[7], cos5[8], cos5[9]);
                    lblTitleDes.Text = cos5[10];
                    lblColcacion.Text = cos5[11];
                    lblColacion2.Text = cos5[12];
                    lblComida.Text = cos5[13];
                    lblCena.Text = cos5[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCen.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;
            }
        }

        public string[] comidas(string desayuno, string colacion, string colacion2, string comid, string cena)
        {
            char del = '(';
            string des = desayuno;
            string[] desa = des.Split(del);
            string c = colacion;
            string[] col = c.Split(del);
            string c2 = colacion2;
            string[] col2 = c2.Split(del);
            string co = comid;
            string[] com = co.Split(del);
            string ce = cena;
            string[] cen = ce.Split(del);
            string[] comida = { desa[0], col[0], col2[0], com[0], cen[0] };
            return comida;
        }

        public string[] individual(string desayuno, string colacion, string colacion2, string comid, string cena)
        {
            char del = '-';
            string des = desayuno;
            string[] desa = des.Split(del);
            string c = colacion;
            string[] col = c.Split(del);
            string c2 = colacion2;
            string[] col2 = c2.Split(del);
            string co = comid;
            string[] com = co.Split(del);
            string ce = cena;
            string[] cen = ce.Split(del);
            string[] comida = { desayuno, desa[1], colacion, col[1], colacion2, col2[1], comid, com[1], cena, cen[1], desa[0], col[0], col2[0], com[0], cen[0] };
            return comida;
        }

        public async void platillo(string desayuno, string deskcal, string colacion, string colkcal, string colacion2, string col2kcal, string comida, string comkcal, string cena, string cenkcal)
        {
            try
            {
                Datos dato = new Datos
                {
                    desayuno = desayuno.Trim(),
                    colacion = colacion.Trim(),
                    colacion2 = colacion2.Trim(),
                    comida = comida.Trim(),
                    cena = cena.Trim(),
                };

                Uri RequestUri = new Uri("https://www.bithives.com/PapayaApp/api/platillo.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(dato);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado)
                    {
                        idDesayuno = resultado.id_des;
                        imgDesayuno.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_des;
                        kcalDesayuno.Text = resultado.kcal_des + " kcal";
                        idColacion = resultado.id_col;
                        imgColacion.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_col;
                        kcalColacion.Text = resultado.kcal_col + " kcal";
                        idColacion2 = resultado.id_col2;
                        imgColacion2.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_col2;
                        kcalColacion2.Text = resultado.kcal_col2 + " kcal";
                        idComida = resultado.id_com;
                        imgComida.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_com;
                        kcalComida.Text = resultado.kcal_com + " kcal";
                        idCena = resultado.id_cen;
                        imgCena.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_cen;
                        kcalCena.Text = resultado.kcal_cen + " kcal";
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

        async void checkDes_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (checkDes.IsChecked)
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check1",
                    valor = "1"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Has completado tu desayuno", "OK");
                    }
                }
            }
            else
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check1",
                    valor = "0"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
        }

        async void checkCol1_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (checkCol1.IsChecked)
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check2",
                    valor = "1"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Has completado tu colacion", "OK");
                    }
                }
            }
            else
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check2",
                    valor = "0"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
        }

        async void checkCom_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (checkCom.IsChecked)
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check3",
                    valor = "1"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Has completado tu comida", "OK");
                    }
                }
            }
            else
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check3",
                    valor = "0"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
        }

        async void checkCol2_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (checkCol2.IsChecked)
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check4",
                    valor = "1"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Has completado tu colacion", "OK");
                    }
                }
            }
            else
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check4",
                    valor = "0"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
        }

        async void checkCen_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (checkCen.IsChecked)
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check5",
                    valor = "1"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Bien hecho", "Has completado tu dia", "OK");
                    }
                }
            }
            else
            {
                Enviar enviar = new Enviar
                {
                    check = true,
                    id_cliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    numCheck = "check5",
                    valor = "0"
                };

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(enviar);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado == false)
                    {
                        await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                    }
                }
            }
        }
    }
}