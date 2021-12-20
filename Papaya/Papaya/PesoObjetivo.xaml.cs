using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using Microcharts;
using SkiaSharp; 

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesoObjetivo : ContentPage
    {
        double bajo = 0, normal = 0, sobrepeso = 0, obesidad = 0;
        public PesoObjetivo()
        {
            InitializeComponent();
            IMC();
        }

        public class Respuesta
        {
            public bool resultado { get; set; }

            public string peso { get; set; }

            public string altura { get; set; }
        }

        public class Meta
        {
            public int idCliente { get; set; }

            public string pesoMeta { get; set; }
        }

        public async void IMC()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php?meta=0&idCliente=" + Preferences.Get("userid", ""));
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
                    lblvalor.Text = Decimal.Round(Convert.ToDecimal(valor), 2) + " Kg/mts<sup><small>2</small></sup>";
                    lblvalor.HorizontalOptions = LayoutOptions.Center;
                    lblvalor.HorizontalTextAlignment = TextAlignment.Center;
                    lblvalor.TextColor = Color.Black;
                    if (valor < 18.5)
                    {
                        bajo = valor;
                        lblTitulo.Text = "¡Bajo peso!";
                        lblTitulo.TextColor = Color.Red;
                        lblIMC.Text = "El peso que tienes para tu estatura es bajo, PAPAYA te recomienda un peso no menor a " + Convert.ToString(Decimal.Round(rangoBajo)) + " para lograr mejorar tu salud.";
                        entryPesoObjetivo.Text = Convert.ToString(Decimal.Round(rangoBajo));
                    }
                    else if (valor >= 18.5 && valor <= 24.9)
                    {
                        normal = valor;
                        lblTitulo.Text = "¡ESTAS EN TU PESO!";
                        lblTitulo.TextColor = Color.Red;
                        lblIMC.Text = "Excelente, te encuentras dentro de los parametros de peso saludables. PAPAYA te recomienda mantenerte dentro de " + Convert.ToString(Decimal.Round(rangoBajo)) + " a " + Convert.ToString(Decimal.Round(rangoAlto)) + " kilogramos para seguir mejorando tu salud.";
                        entryPesoObjetivo.Text = Convert.ToString(Decimal.Round((rangoAlto + rangoBajo) / 2));
                    }
                    else if (valor >= 20.5 && valor <= 29.9)
                    {
                        sobrepeso = valor;
                        lblTitulo.Text = "¡Tu peso esta por encima de lo recomendado!";
                        lblTitulo.TextColor = Color.Red;
                        lblIMC.Text = "PAPAYA te recomienda tomar como objetivo el peso de " + Convert.ToString(Decimal.Round(rangoAlto)) + " para mejorar tu salud";
                        entryPesoObjetivo.Text = Convert.ToString(Decimal.Round(rangoAlto));
                    }
                    else if (valor >= 30)
                    {
                        obesidad = valor;
                        lblTitulo.Text = "¡Peso no saludable!";
                        lblTitulo.TextColor = Color.Red;
                        lblIMC.Text = "El peso que presentas es excedente al peso saludable. PAPAYA te recomienda " + Convert.ToString(Decimal.Round(rangoAlto)) + " como  peso objetivo. ¡COMENCEMOS AHORA!";
                        entryPesoObjetivo.Text = Convert.ToString(Decimal.Round(rangoAlto));
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
                            ValueLabelColor = SKColor.Parse("#EBF140"),
                            Color = SKColor.Parse("#EBF140")
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
                        LabelMode = LabelMode.RightOnly
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

            await MaterialDialog.Instance.SnackbarAsync(message: "Puedes aumentar o reducir el peso objetivo!!",
                                                                                    actionButtonText: "Entendido!",
                                                                                    msDuration: 5000);
        }

        private void btnContinuar_Clicked(object sender, EventArgs e)
        {
            
        }

        private  void btnContinuar_Clicked_1(object sender, EventArgs e)
        {
            
        }

        private  void btnContinuar1_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            Meta meta = new Meta
            {
                idCliente = Convert.ToInt32(Preferences.Get("userid", "")),
                pesoMeta = entryPesoObjetivo.Text
            };
            Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/diag.php");

            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(meta);

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                if (resultado.resultado)
                {
                    await Navigation.PushAsync(new Apetito());
                }
                else
                {
                    await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
                }
            }
                    
        }

        private void btnSubir_Clicked(object sender, EventArgs e)
        {
            decimal valor = Convert.ToDecimal(entryPesoObjetivo.Text);
            valor ++;
            entryPesoObjetivo.Text = Convert.ToString(valor);
        }

        private void btnBajar_Clicked(object sender, EventArgs e)
        {
            decimal valor = Convert.ToDecimal(entryPesoObjetivo.Text);
            valor --;
            entryPesoObjetivo.Text = Convert.ToString(valor);
        }
    }
}