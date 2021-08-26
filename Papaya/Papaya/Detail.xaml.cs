using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microcharts;
using Entry = Microcharts.ChartEntry;

using SkiaSharp;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Detail : ContentPage
    {
        List<Entry> entryList, entryDonut;
        public Detail()
        {
            InitializeComponent();
            entryList = new List<Entry>();
            entryDonut = new List<Entry>();
            //loadEntrys();
            imc();
            obtenerAvances();
            DonutChart.Chart = new DonutChart()
            {
                Entries = entryDonut
            };
            LinetChart.Chart = new LineChart()
            {
                Entries = entryList
            };
        }

        public class Respuesta
        {
            public bool resultado { get; set; }

            public string altura { get; set; }

            public string peso { get; set; }

            public string meta { get; set; }
        }

        public class Respuesta2
        {
            public int id { get; set; }

            public string peso { get; set; }

            public string fecha { get; set; }
        }

        public async void imc()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://www.bithives.com/PapayaApp/api/inicio.php?imc=0&id=" + Preferences.Get("userid", ""));
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
                    decimal imc = Convert.ToDecimal(resultado.peso) / alturaCuadrada;
                    lblIMC.Text = "Tu IMC actual es " + Convert.ToString(Decimal.Round(imc, 2));
                    if (imc < Convert.ToDecimal(18.5))
                    {
                        Entry e1 = new Entry(300)
                        {
                            Label = "Bajo peso",
                            ValueLabel = "300",
                            Color = SKColor.Parse("#4871F0")
                        };
                        entryDonut.Add(e1);
                    }
                    else if (imc >= Convert.ToDecimal(18.5) && imc <= Convert.ToDecimal(24.9))
                    {
                        Entry e1 = new Entry(300)
                        {
                            Label = "Peso normal",
                            ValueLabel = "300",
                            Color = SKColor.Parse("#48C850")
                        };
                        entryDonut.Add(e1);
                    }
                    else if (imc >= Convert.ToDecimal(25) && imc <= Convert.ToDecimal(29.9))
                    {
                        Entry e1 = new Entry(300)
                        {
                            Label = "Sobrepeso",
                            ValueLabel = "300",
                            Color = SKColor.Parse("#F4FB07")
                        };
                        entryDonut.Add(e1);
                    }
                    else if (imc >= Convert.ToDecimal(30) && imc <= Convert.ToDecimal(34.9))
                    {
                        Entry e1 = new Entry(300)
                        {
                            Label = "Obesidad I",
                            ValueLabel = "300",
                            Color = SKColor.Parse("#FFAA00")
                        };
                        entryDonut.Add(e1);
                    }
                    else if (imc >= Convert.ToDecimal(35) && imc <= Convert.ToDecimal(39.9))
                    {
                        Entry e1 = new Entry(300)
                        {
                            Label = "Obesidad II",
                            ValueLabel = "300",
                            Color = SKColor.Parse("#FF0000")
                        };
                        entryDonut.Add(e1);
                    }
                    else if (imc > Convert.ToDecimal(40))
                    {
                        Entry e1 = new Entry(300)
                        {
                            Label = "Obesidad III",
                            ValueLabel = "300",
                            Color = SKColor.Parse("#FF87D3")
                        };
                        entryDonut.Add(e1);
                    }
                }
            }
            else
            {
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
            }
        }

        public async void obtenerAvances()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://www.bithives.com/PapayaApp/api/inicio.php?pesos=0&id=" + Preferences.Get("userid", ""));
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<List<Respuesta2>>(content);

                if (resultado.Count > 0)
                {
                    foreach (Respuesta2 res in resultado)
                    {
                        Entry e1 = new Entry(float.Parse(res.peso))
                        {
                            Label = res.fecha,
                            Color = SKColor.Parse("#00BCD4")
                        };
                        entryList.Add(e1);
                    }
                }
            }
            else
            {
                await DisplayAlert("Mensaje", "Fallo la conexion al servidor", "OK");
            }
        }

        /*private void loadEntrys()
        {
            Entry e1 = new Entry(70)
            {
                Label = "A",
                ValueLabel = "70",
                Color = SKColor.Parse("#00BCD4")
            };

            Entry e2 = new Entry(300)
            {
                Label = "B",
                ValueLabel = "300",
                Color = SKColor.Parse("#E81E15")
            };

            Entry e3 = new Entry(50)
            {
                Label = "C",
                ValueLabel = "50",
                Color = SKColor.Parse("#5DF7D1")
            };

            Entry e4 = new Entry(200)
            {
                Label = "D",
                ValueLabel = "200",
                Color = SKColor.Parse("#CBE83A")
            };

            entryList.Add(e1);
            entryList.Add(e2);
            entryList.Add(e3);
            entryList.Add(e4);
        }*/
    }
}