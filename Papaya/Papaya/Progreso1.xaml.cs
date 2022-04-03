using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Papaya
{
    public partial class Progreso1 : ContentPage
    {
        double bajo = 0, normal = 0, sobrepeso = 0, obesidad = 0;
        public Progreso1()
        {
            InitializeComponent();
        }

        public class Avance
        {
            public bool avance { get; set; }

            public int cliente_id { get; set; }

            public string peso { get; set; }
        }

        public class Formulario
        {
            public string id { get; set; }

            public string id_cliente { get; set; }

            public string altura { get; set; }

            public string peso { get; set; }

            public string meta { get; set; }

            public string actividad_fisica { get; set; }

            public string alcohol { get; set; }

            public string apetito { get; set; }
        }

        public class Resp
        {
            public bool resultado { get; set; }

            public string ultimoPeso { get; set; }

            public Formulario formulario { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }

            public string peso { get; set; }

            public string altura { get; set; }

            public string actividad { get; set; }

            public string alcohol { get; set; }

            public string meta { get; set; }

            public string pesoMeta { get; set; }

            public string edad { get; set; }

            public string sexo { get; set; }

            public string apetito { get; set; }
        }

        public async void IMC()
        {
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php?calorias=0&idCliente=" + Preferences.Get("userid", ""));
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
                        decimal altura = Convert.ToDecimal(resultado.altura);
                        decimal alturaCuadrada = altura * altura;
                        decimal rangoAlto = alturaCuadrada * Convert.ToDecimal(24.9);
                        decimal rangoBajo = alturaCuadrada * Convert.ToDecimal(18.5);
                        double valor = Convert.ToDouble(Convert.ToDecimal(resultado.peso) / alturaCuadrada);
                        lblvalor.TextType = TextType.Html;
                        lblvalor.Text = Decimal.Round(Convert.ToDecimal(valor), 1) + " Kg/mts<sup><small>2</small></sup>";
                        lblvalor.HorizontalOptions = LayoutOptions.Center;
                        lblvalor.HorizontalTextAlignment = TextAlignment.Center;
                        lblvalor.TextColor = Color.Black;
                        if (valor < 18.5)
                        {
                            bajo = valor;
                            /*
                            lblTitulo.Text = "¡Bajo peso!";
                            lblTitulo.TextColor = Color.FromHex("188BFF");
                            lblIMC.Text = "El peso que tienes para tu estatura es bajo, PAPAYA te recomienda un peso no menor a " + Convert.ToString(Decimal.Round(rangoBajo, 1)) + " para lograr mejorar tu salud.";
                            entryPesoObjetivo.Text = Convert.ToString(Decimal.Round(rangoBajo, 1));
                            */
                        }
                        else if (valor >= 18.5 && valor <= 24.9)
                        {
                            normal = valor;
                            /*
                            lblTitulo.Text = "¡ESTAS EN TU PESO!";
                            lblTitulo.TextColor = Color.FromHex("4DC253");
                            lblIMC.Text = "Excelente, te encuentras dentro de los parametros de peso saludables. PAPAYA te recomienda mantenerte dentro de " + Convert.ToString(Decimal.Round(rangoBajo, 1)) + " a " + Convert.ToString(Decimal.Round(rangoAlto, 1)) + " kilogramos para seguir mejorando tu salud.";
                            entryPesoObjetivo.Text = Convert.ToString(Decimal.Round((rangoAlto + rangoBajo) / 2, 1));
                            */
                        }
                        else if (valor >= 25.0 && valor <= 29.9)
                        {
                            sobrepeso = valor;
                            /*
                            lblTitulo.Text = "¡Tu peso esta por encima de lo recomendado!";
                            lblTitulo.TextColor = Color.FromHex("FF980C");
                            lblIMC.Text = "PAPAYA te recomienda tomar como objetivo el peso de " + Convert.ToString(Decimal.Round(rangoAlto, 1)) + " para mejorar tu salud";
                            entryPesoObjetivo.Text = Convert.ToString(Decimal.Round(rangoAlto, 1));
                            */
                        }
                        else if (valor >= 30)
                        {
                            obesidad = valor;
                            /*
                            lblTitulo.Text = "¡Peso no saludable!";
                            lblTitulo.TextColor = Color.FromHex("F12919");
                            lblIMC.Text = "El peso que presentas es excedente al peso saludable. PAPAYA te recomienda " + Convert.ToString(Decimal.Round(rangoAlto, 1)) + " como  peso objetivo. ¡COMENCEMOS AHORA!";
                            entryPesoObjetivo.Text = Convert.ToString(Decimal.Round(rangoAlto, 1));
                            */
                        }
                        var entries = new[]
                        {
                        new ChartEntry((float)bajo)
                        {
                            Label = "Peso bajo",
                            ValueLabel = "< 18.5",
                            ValueLabelColor = SKColor.Parse("#188BFF"),
                            Color = SKColor.Parse("#188BFF")
                        },
                        new ChartEntry((float)normal)
                        {
                            Label = "Peso ideal",
                            ValueLabel = "18.5 - 24.9",
                            ValueLabelColor = SKColor.Parse("#4DC253"),
                            Color = SKColor.Parse("#4DC253")
                        },
                        new ChartEntry((float)sobrepeso)
                        {
                            Label = "Sobrepeso",
                            ValueLabel = "25.0 - 29.9",
                            ValueLabelColor = SKColor.Parse("#FF980C"),
                            Color = SKColor.Parse("#FF980C")
                        },
                        new ChartEntry((float)obesidad)
                        {
                            Label = "Obesidad",
                            ValueLabel = "> 30",
                            ValueLabelColor = SKColor.Parse("#F12919"),
                            Color = SKColor.Parse("#F12919")
                        },

                    };
                        grafico.Chart = new DonutChart()
                        {
                            Entries = entries,
                            LabelTextSize = 40,
                            LabelMode = LabelMode.RightOnly,
                        };
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Falso el valor", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Source);
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor, intente de nuevo", "OK");
            }
        }

        async void btnAvanzar_Clicked(System.Object sender, System.EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Progreso2());
        }

        async void btnCalcular_Clicked(System.Object sender, System.EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(EntryPeso.Text))
            {
                await DisplayAlert("Advertencia", "El campo correo esta vacio", "OK");
            }
            else
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando datos"))
                {
                    Avance avance = new Avance
                    {
                        avance = true,
                        cliente_id = Convert.ToInt32(Preferences.Get("userid", "")),
                        peso = EntryPeso.Text
                    };

                    Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php");

                    var client = new HttpClient();

                    var json = JsonConvert.SerializeObject(avance);

                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(RequestUri, contentJson);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        var resultado = JsonConvert.DeserializeObject<Resp>(content);

                        if (resultado.resultado)
                        {
                            IMC();
                            if (Convert.ToDecimal(EntryPeso.Text) < Convert.ToDecimal(resultado.ultimoPeso))
                            {
                                lblMsg.Text = "¡Felicidades! Lograste bajar de peso, sigue asi";
                            }
                            else if (Convert.ToDecimal(EntryPeso.Text) == Convert.ToDecimal(resultado.ultimoPeso))
                            {
                                lblMsg.Text = "Estas en el mismo peso que la ultima ocasion";
                            }
                            else if (Convert.ToDecimal(EntryPeso.Text) > Convert.ToDecimal(resultado.ultimoPeso))
                            {
                                lblMsg.Text = "Subiste de peso, no has seguido la dieta al pie de la letra";
                            }
                            btnCalcular.IsVisible = false;
                            cardIMC.IsVisible = true;
                            lblMsg.IsVisible = true;
                            btnAvanzar.IsVisible = true;
                        }
                        else
                        {
                            await DisplayAlert("Mensaje", "Error", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Datos invalidos", "OK");
                    }
                }
            }
        }
    }
}
