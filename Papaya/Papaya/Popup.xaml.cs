using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Papaya.Models;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Papaya
{
    public partial class Popup : Rg.Plugins.Popup.Pages.PopupPage
    {
        Dieta diet = new Dieta();
        DateTime fecha = DateTime.Today;
        DateTime dias = DateTime.Today;
        int numDia, idDesayuno = 0, idColacion = 0, idColacion2 = 0, idComida = 0, idCena = 0, idPlatillo;

        public Popup(int idplatillo)
        {
            InitializeComponent();
            lblFecha.Text = fecha.ToString("dddd").ToUpper();
            numDia = (int)fecha.DayOfWeek;
            this.idPlatillo = idplatillo;
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

        public class Enviar
        {
            public int idCliente { get; set; }

            public int dieta { get; set; }
        }

        public class Res
        {
            public bool resultado { get; set; }

            public Dieta dieta { get; set; }
        }

        private class Check
        {
            public bool newCheck { get; set; }

            public int cliente_id { get; set; }
        }

        public async void dieta()
        {
            try
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Obteniendo datos"))
                {
                    var request = new HttpRequestMessage();
                    request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php?diet=" + idPlatillo);
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");
                    var client = new HttpClient();
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        var resultado = JsonConvert.DeserializeObject<Res>(content);

                        if (resultado.resultado)
                        {
                            this.diet = resultado.dieta;
                            switch (numDia)
                            {
                                case 0:
                                    cardDes.IsVisible = false;
                                    cardCol.IsVisible = false;
                                    cardCom.IsVisible = false;
                                    cardCol2.IsVisible = false;
                                    cardCena.IsVisible = false;
                                    cardDomingo.IsVisible = true;
                                    break;

                                case 1:
                                    var coms = comidas(resultado.dieta.d_lunes, resultado.dieta.c_lunes, resultado.dieta.c2_lunes, resultado.dieta.co_lunes, resultado.dieta.ce_lunes);
                                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                                    platillo(cos0[0], cos0[2], cos0[4], cos0[6], cos0[8]);
                                    lblTitleDes.Text = cos0[10];
                                    lblColcacion.Text = cos0[11];
                                    lblColacion2.Text = cos0[12];
                                    lblComida.Text = cos0[13];
                                    lblCena.Text = cos0[14];
                                    break;

                                case 2:
                                    var coms2 = comidas(resultado.dieta.d_martes, resultado.dieta.c_martes, resultado.dieta.c2_martes, resultado.dieta.co_martes, resultado.dieta.ce_martes);
                                    var cos = individual(coms2[0], coms2[1], coms2[2], coms2[3], coms2[4]);
                                    platillo(cos[0], cos[2], cos[4], cos[6], cos[8]);
                                    lblTitleDes.Text = cos[10];
                                    lblColcacion.Text = cos[11];
                                    lblColacion2.Text = cos[12];
                                    lblComida.Text = cos[13];
                                    lblCena.Text = cos[14];
                                    break;

                                case 3:
                                    var coms3 = comidas(resultado.dieta.d_miercoles, resultado.dieta.c_miercoles, resultado.dieta.c2_miercoles, resultado.dieta.co_miercoles, resultado.dieta.ce_miercoles);
                                    var cos2 = individual(coms3[0], coms3[1], coms3[2], coms3[3], coms3[4]);
                                    platillo(cos2[0], cos2[2], cos2[4], cos2[6], cos2[8]);
                                    lblTitleDes.Text = cos2[10];
                                    lblColcacion.Text = cos2[11];
                                    lblColacion2.Text = cos2[12];
                                    lblComida.Text = cos2[13];
                                    lblCena.Text = cos2[14];
                                    break;

                                case 4:
                                    var coms4 = comidas(resultado.dieta.d_jueves, resultado.dieta.c_jueves, resultado.dieta.c2_jueves, resultado.dieta.co_jueves, resultado.dieta.ce_jueves);
                                    var cos3 = individual(coms4[0], coms4[1], coms4[2], coms4[3], coms4[4]);
                                    platillo(cos3[0], cos3[2], cos3[4], cos3[6], cos3[8]);
                                    lblTitleDes.Text = cos3[10];
                                    lblColcacion.Text = cos3[11];
                                    lblColacion2.Text = cos3[12];
                                    lblComida.Text = cos3[13];
                                    lblCena.Text = cos3[14];
                                    break;

                                case 5:
                                    var coms5 = comidas(resultado.dieta.d_viernes, resultado.dieta.c_viernes, resultado.dieta.c2_viernes, resultado.dieta.co_viernes, resultado.dieta.ce_viernes);
                                    var cos4 = individual(coms5[0], coms5[1], coms5[2], coms5[3], coms5[4]);
                                    platillo(cos4[0], cos4[2], cos4[4], cos4[6], cos4[8]);
                                    lblTitleDes.Text = cos4[10];
                                    lblColcacion.Text = cos4[11];
                                    lblColacion2.Text = cos4[12];
                                    lblComida.Text = cos4[13];
                                    lblCena.Text = cos4[14];
                                    break;

                                case 6:
                                    var coms6 = comidas(resultado.dieta.d_sabado, resultado.dieta.c_sabado, resultado.dieta.c2_sabado, resultado.dieta.co_sabado, resultado.dieta.ce_sabado);
                                    var cos5 = individual(coms6[0], coms6[1], coms6[2], coms6[3], coms6[4]);
                                    platillo(cos5[0], cos5[2], cos5[4], cos5[6], cos5[8]);
                                    lblTitleDes.Text = cos5[10];
                                    lblColcacion.Text = cos5[11];
                                    lblColacion2.Text = cos5[12];
                                    lblComida.Text = cos5[13];
                                    lblCena.Text = cos5[14];
                                    break;
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Source);
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
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

        public async void platillo(string desayuno, string colacion, string colacion2, string comida, string cena)
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
                        kcalDesayuno.Text = resultado.kcal_des + "kcal";
                        idColacion = resultado.id_col;
                        imgColacion.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_col;
                        kcalColacion.Text = resultado.kcal_col + "kcal";
                        idColacion2 = resultado.id_col2;
                        imgColacion2.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_col2;
                        kcalColacion2.Text = resultado.kcal_col2 + "kcal";
                        idComida = resultado.id_com;
                        imgComida.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_com;
                        kcalComida.Text = resultado.kcal_com + "kcal";
                        idCena = resultado.id_cen;
                        imgCena.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_cen;
                        kcalCena.Text = resultado.kcal_cen + "kcal";
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

        void btnDiaAnterior_Clicked(System.Object sender, System.EventArgs e)
        {
            dias = dias.AddDays(-1);
            int numerodias = (int)dias.DayOfWeek;
            switch (numerodias)
            {
                case 0:
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    cardDes.IsVisible = false;
                    cardCol.IsVisible = false;
                    cardCom.IsVisible = false;
                    cardCol2.IsVisible = false;
                    cardCena.IsVisible = false;
                    cardDomingo.IsVisible = true;
                    break;

                case 1:
                    var coms = comidas(diet.d_lunes, diet.c_lunes, diet.c2_lunes, diet.co_lunes, diet.ce_lunes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                    platillo(cos0[0], cos0[2], cos0[4], cos0[6], cos0[8]);
                    lblTitleDes.Text = cos0[10];
                    lblColcacion.Text = cos0[11];
                    lblColacion2.Text = cos0[12];
                    lblComida.Text = cos0[13];
                    lblCena.Text = cos0[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCena.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;

                case 2:
                    var coms2 = comidas(diet.d_martes, diet.c_martes, diet.c2_martes, diet.co_martes, diet.ce_martes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos = individual(coms2[0], coms2[1], coms2[2], coms2[3], coms2[4]);
                    platillo(cos[0], cos[2], cos[4], cos[6], cos[8]);
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
                    platillo(cos2[0], cos2[2], cos2[4], cos2[6], cos2[8]);
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
                    platillo(cos3[0], cos3[2], cos3[4], cos3[6], cos3[8]);
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
                    platillo(cos4[0], cos4[2], cos4[4], cos4[6], cos4[8]);
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
                    platillo(cos5[0], cos5[2], cos5[4], cos5[6], cos5[8]);
                    lblTitleDes.Text = cos5[10];
                    lblColcacion.Text = cos5[11];
                    lblColacion2.Text = cos5[12];
                    lblComida.Text = cos5[13];
                    lblCena.Text = cos5[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCena.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;
            }
        }

        void btnDiaSiguiente_Clicked(System.Object sender, System.EventArgs e)
        {
            dias = dias.AddDays(1);
            int numerodias = (int)dias.DayOfWeek;
            switch (numerodias)
            {
                case 0:
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    cardDes.IsVisible = false;
                    cardCol.IsVisible = false;
                    cardCom.IsVisible = false;
                    cardCol2.IsVisible = false;
                    cardCena.IsVisible = false;
                    cardDomingo.IsVisible = true;
                    break;

                case 1:
                    var coms = comidas(diet.d_lunes, diet.c_lunes, diet.c2_lunes, diet.co_lunes, diet.ce_lunes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                    platillo(cos0[0], cos0[2], cos0[4], cos0[6], cos0[8]);
                    lblTitleDes.Text = cos0[10];
                    lblColcacion.Text = cos0[11];
                    lblColacion2.Text = cos0[12];
                    lblComida.Text = cos0[13];
                    lblCena.Text = cos0[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCena.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;

                case 2:
                    var coms2 = comidas(diet.d_martes, diet.c_martes, diet.c2_martes, diet.co_martes, diet.ce_martes);
                    lblFecha.Text = dias.ToString("dddd").ToUpper();
                    var cos = individual(coms2[0], coms2[1], coms2[2], coms2[3], coms2[4]);
                    platillo(cos[0], cos[2], cos[4], cos[6], cos[8]);
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
                    platillo(cos2[0], cos2[2], cos2[4], cos2[6], cos2[8]);
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
                    platillo(cos3[0], cos3[2], cos3[4], cos3[6], cos3[8]);
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
                    platillo(cos4[0], cos4[2], cos4[4], cos4[6], cos4[8]);
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
                    platillo(cos5[0], cos5[2], cos5[4], cos5[6], cos5[8]);
                    lblTitleDes.Text = cos5[10];
                    lblColcacion.Text = cos5[11];
                    lblColacion2.Text = cos5[12];
                    lblComida.Text = cos5[13];
                    lblCena.Text = cos5[14];
                    cardDes.IsVisible = true;
                    cardCol.IsVisible = true;
                    cardCom.IsVisible = true;
                    cardCol2.IsVisible = true;
                    cardCena.IsVisible = true;
                    cardDomingo.IsVisible = false;
                    break;
            }
        }

        async void seleccionarDieta_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                Enviar dato = new Enviar
                {
                    idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                    dieta = idPlatillo
                };

                Uri RequestUri = new Uri("https://www.bithives.com/PapayaApp/api/diag.php");

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
                        Check check = new Check
                        {
                            newCheck = true,
                            cliente_id = Convert.ToInt32(Preferences.Get("userid", ""))
                        };

                        Uri RequestUri1 = new Uri("https://bithives.com/PapayaApp/api/check.php");

                        var client1 = new HttpClient();

                        var json1 = JsonConvert.SerializeObject(check);

                        var contentJson1 = new StringContent(json1, Encoding.UTF8, "application/json");

                        var response1 = await client1.PostAsync(RequestUri1, contentJson1);

                        if (response1.StatusCode == HttpStatusCode.OK)
                        {
                            string content1 = await response1.Content.ReadAsStringAsync();

                            var resultado1 = JsonConvert.DeserializeObject<Respuesta>(content1);

                            if (resultado1.resultado)
                            {
                                // Close the last PopupPage int the PopupStack
                                await Navigation.PopPopupAsync();
                                await Navigation.PushAsync(new Home());
                                Application.Current.MainPage = new Home();
                            }
                            else
                            {
                                await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                            }
                        }
                        
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

        void btnCerrar_Clicked(System.Object sender, System.EventArgs e)
        {
            // Close the last PopupPage int the PopupStack
            Navigation.PopPopupAsync();
        }
    }
}