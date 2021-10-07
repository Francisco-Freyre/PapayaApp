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

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Detail : ContentPage
    {
        Dieta diet = new Dieta();
        DateTime fecha = DateTime.Today;
        DateTime dias = DateTime.Today;
        int numDia, idDesayuno = 0, idColacion = 0, idColacion2 = 0, idComida = 0, idCena = 0;
        public Detail()
        {
            InitializeComponent();
            lblFecha.Text = fecha.ToLongDateString();
            numDia = (int)fecha.DayOfWeek;
            dieta();
        }

        public class Datos
        {
            public string desayuno { get; set; }
            public string kcaldes { get; set; }
            public string colacion { get; set; }
            public string kcalcol { get; set; }
            public string colacion2 { get; set; }
            public string kcalcol2 { get; set; }
            public string comida { get; set; }
            public string kcalcom { get; set; }
            public string cena { get; set; }
            public string kcalcen { get; set; }
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

        public async void dieta()
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
                            lblTitleDes.Text = resultado.d_domingo;
                            lblColcacion.Text = resultado.c_domingo;
                            lblColacion2.Text = resultado.c2_domingo;
                            lblComida.Text = resultado.co_domingo;
                            lblCena.Text = resultado.ce_domingo;
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
                    await DisplayAlert("Mensaje", resultado.kcal + resultado.categoria + resultado.descripcion, "OK");
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

        private void btnAlmuerzo_Clicked(object sender, EventArgs e)
        {
        }

        private async void btnComida_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idComida));
        }

        private async void btnCena_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idCena));
        }

        private async void btnColacion2_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idColacion2));
        }

        private async void btnColacion_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(idColacion));
        }

        private void btnDiaAnterior_Clicked(object sender, EventArgs e)
        {
            dias = dias.AddDays(-1);
            int numerodias = (int)dias.DayOfWeek;
            switch (numerodias)
            {
                case 0:
                    lblFecha.Text = dias.ToLongDateString();
                    lblTitleDes.Text = diet.d_domingo;
                    lblColcacion.Text = diet.c_domingo;
                    lblColacion2.Text = diet.c2_domingo;
                    lblComida.Text = diet.co_domingo;
                    lblCena.Text = diet.ce_domingo;
                    break;

                case 1:
                    var coms = comidas(diet.d_lunes, diet.c_lunes, diet.c2_lunes, diet.co_lunes, diet.ce_lunes);
                    lblFecha.Text = dias.ToLongDateString();
                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                    platillo(cos0[0], cos0[1], cos0[2], cos0[3], cos0[4], cos0[5], cos0[6], cos0[7], cos0[8], cos0[9]);
                    lblTitleDes.Text = cos0[10];
                    lblColcacion.Text = cos0[11];
                    lblColacion2.Text = cos0[12];
                    lblComida.Text = cos0[13];
                    lblCena.Text = cos0[14];
                    break;

                case 2:
                    var coms2 = comidas(diet.d_martes, diet.c_martes, diet.c2_martes, diet.co_martes, diet.ce_martes);
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
                    var cos5 = individual(coms6[0], coms6[1], coms6[2], coms6[3], coms6[4]);
                    platillo(cos5[0], cos5[1], cos5[2], cos5[3], cos5[4], cos5[5], cos5[6], cos5[7], cos5[8], cos5[9]);
                    lblTitleDes.Text = cos5[10];
                    lblColcacion.Text = cos5[11];
                    lblColacion2.Text = cos5[12];
                    lblComida.Text = cos5[13];
                    lblCena.Text = cos5[14];
                    break;
            }
        }

        private void btnDiaSiguiente_Clicked(object sender, EventArgs e)
        {
            dias = dias.AddDays(1);
            int numerodias = (int)dias.DayOfWeek;
            switch (numerodias)
            {
                case 0:
                    lblFecha.Text = dias.ToLongDateString();
                    lblTitleDes.Text = diet.d_domingo;
                    lblColcacion.Text = diet.c_domingo;
                    lblColacion2.Text = diet.c2_domingo;
                    lblComida.Text = diet.co_domingo;
                    lblCena.Text = diet.ce_domingo;
                    break;

                case 1:
                    var coms = comidas(diet.d_lunes, diet.c_lunes, diet.c2_lunes, diet.co_lunes, diet.ce_lunes);
                    lblFecha.Text = dias.ToLongDateString();
                    var cos0 = individual(coms[0], coms[1], coms[2], coms[3], coms[4]);
                    platillo(cos0[0], cos0[1], cos0[2], cos0[3], cos0[4], cos0[5], cos0[6], cos0[7], cos0[8], cos0[9]);
                    lblTitleDes.Text = cos0[10];
                    lblColcacion.Text = cos0[11];
                    lblColacion2.Text = cos0[12];
                    lblComida.Text = cos0[13];
                    lblCena.Text = cos0[14];
                    break;

                case 2:
                    var coms2 = comidas(diet.d_martes, diet.c_martes, diet.c2_martes, diet.co_martes, diet.ce_martes);
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
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
                    lblFecha.Text = dias.ToLongDateString();
                    var cos5 = individual(coms6[0], coms6[1], coms6[2], coms6[3], coms6[4]);
                    platillo(cos5[0], cos5[1], cos5[2], cos5[3], cos5[4], cos5[5], cos5[6], cos5[7], cos5[8], cos5[9]);
                    lblTitleDes.Text = cos5[10];
                    lblColcacion.Text = cos5[11];
                    lblColacion2.Text = cos5[12];
                    lblComida.Text = cos5[13];
                    lblCena.Text = cos5[14];
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
            Datos dato = new Datos
            {
                desayuno = desayuno.Trim(),
                kcaldes = deskcal.Trim(),
                colacion = colacion.Trim(),
                kcalcol = colkcal.Trim(),
                colacion2 = colacion2.Trim(),
                kcalcol2 = col2kcal.Trim(),
                comida = comida.Trim(),
                kcalcom = comkcal.Trim(),
                cena = cena.Trim(),
                kcalcen = cenkcal.Trim()
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
                    kcalDesayuno.Text = resultado.kcal_des;
                    idColacion = resultado.id_col;
                    imgColacion.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_col;
                    kcalColacion.Text = resultado.kcal_col;
                    idColacion2 = resultado.id_col2;
                    imgColacion2.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_col2;
                    kcalColacion2.Text = resultado.kcal_col2;
                    idComida = resultado.id_com;
                    imgComida.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_com;
                    kcalComida.Text = resultado.kcal_com;
                    idCena = resultado.id_cen;
                    imgCena.Source = "https://www.bithives.com/PapayaApp/" + resultado.img_cen;
                    kcalCena.Text = resultado.kcal_cen;
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
        }
    }
}