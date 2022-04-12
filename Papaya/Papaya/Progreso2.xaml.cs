using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Papaya
{
    public partial class Progreso2 : ContentPage
    {
        public Progreso2()
        {
            InitializeComponent();

            Datos();
        }

        public class Semanas
        {
            public string id { get; set; }
            public string cliente_id { get; set; }
            public string d1 { get; set; }
            public string d2 { get; set; }
            public string d3 { get; set; }
            public string d4 { get; set; }
            public string d5 { get; set; }
            public string d6 { get; set; }
            public string d7 { get; set; }
            public string d8 { get; set; }
            public string d9 { get; set; }
            public string d10 { get; set; }
            public string d11 { get; set; }
            public string d12 { get; set; }
            public string dia_final { get; set; }
        }

        public class Peso
        {
            public string id { get; set; }
            public string id_cliente { get; set; }
            public string peso { get; set; }
            public string tipo { get; set; }
            public string fecha { get; set; }
        }

        public class Respuesta
        {
            public bool resultado { get; set; }
            public string pesoBajo { get; set; }
            public string pesoAlto { get; set; }
            public Semanas semanas { get; set; }
            public List<Peso> pesos { get; set; }
        }

        void btnSi_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void btnNo_Clicked(System.Object sender, System.EventArgs e)
        {
            lblPregunta.IsVisible = true;
            gridRespuesta.IsVisible = true;
        }

        async void btnRecalSi_Clicked(System.Object sender, System.EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Apetito2());
        }

        public async void Datos()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://bithives.com/PapayaApp/api/check.php?DosSemanas=1&id_cliente=" + Preferences.Get("userid", ""));
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
                    d1.BackgroundColor = (Convert.ToInt32(resultado.semanas.d1) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d2.BackgroundColor = (Convert.ToInt32(resultado.semanas.d2) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d3.BackgroundColor = (Convert.ToInt32(resultado.semanas.d3) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d4.BackgroundColor = (Convert.ToInt32(resultado.semanas.d4) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d5.BackgroundColor = (Convert.ToInt32(resultado.semanas.d5) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d6.BackgroundColor = (Convert.ToInt32(resultado.semanas.d6) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d7.BackgroundColor = (Convert.ToInt32(resultado.semanas.d7) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d8.BackgroundColor = (Convert.ToInt32(resultado.semanas.d8) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d9.BackgroundColor = (Convert.ToInt32(resultado.semanas.d9) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d10.BackgroundColor = (Convert.ToInt32(resultado.semanas.d10) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d11.BackgroundColor = (Convert.ToInt32(resultado.semanas.d11) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    d12.BackgroundColor = (Convert.ToInt32(resultado.semanas.d12) > 3) ? Color.FromHex("4ADC40") : Color.White;
                    int cantidad = resultado.pesos.Count;
                    if (cantidad == 2)
                    {
                        var entrie0 = resultado.pesos[1];
                        var entrie1 = resultado.pesos[0];

                        var entries = new[]
                        {
                            new ChartEntry(Convert.ToSingle(entrie0.peso))
                            {
                                Label = entrie0.fecha,
                                ValueLabel = entrie0.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            },
                            new ChartEntry(Convert.ToSingle(entrie1.peso))
                            {
                                Label = entrie1.fecha,
                                ValueLabel = entrie1.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            }
                        };

                        grafico.Chart = new LineChart()
                        {
                            Entries = entries,
                            LabelTextSize = 20,
                            LabelColor = SKColor.Parse("#000000"),
                            ValueLabelOrientation = Orientation.Horizontal,
                            LabelOrientation = Orientation.Horizontal,
                            LineMode = LineMode.Straight,
                            LineSize = 10,
                            PointSize = 20,
                            MinValue = Convert.ToSingle(resultado.pesoBajo) - 10,
                            MaxValue = Convert.ToSingle(resultado.pesoAlto) + 10
                        };
                    }
                    else if(cantidad == 3)
                    {
                        var entrie0 = resultado.pesos[2];
                        var entrie1 = resultado.pesos[1];
                        var entrie2 = resultado.pesos[0];

                        var entries = new[]
                        {
                            new ChartEntry(Convert.ToSingle(entrie0.peso))
                            {
                                Label = entrie0.fecha,
                                ValueLabel = entrie0.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            },
                            new ChartEntry(Convert.ToSingle(entrie1.peso))
                            {
                                Label = entrie1.fecha,
                                ValueLabel = entrie1.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            },
                            new ChartEntry(Convert.ToSingle(entrie2.peso))
                            {
                                Label = entrie2.fecha,
                                ValueLabel = entrie2.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            }
                        };

                        grafico.Chart = new LineChart()
                        {
                            Entries = entries,
                            LabelTextSize = 20,
                            LabelColor = SKColor.Parse("#000000"),
                            ValueLabelOrientation = Orientation.Horizontal,
                            LabelOrientation = Orientation.Horizontal,
                            LineMode = LineMode.Straight,
                            LineSize = 10,
                            PointSize = 20,
                            MinValue = Convert.ToSingle(resultado.pesoBajo) - 10,
                            MaxValue = Convert.ToSingle(resultado.pesoAlto) + 10
                        };
                    }
                    else if (cantidad == 4)
                    {
                        var entrie0 = resultado.pesos[3];
                        var entrie1 = resultado.pesos[2];
                        var entrie2 = resultado.pesos[1];
                        var entrie3 = resultado.pesos[0];

                        var entries = new[]
                        {
                            new ChartEntry(Convert.ToSingle(entrie0.peso))
                            {
                                Label = entrie0.fecha,
                                ValueLabel = entrie0.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            },
                            new ChartEntry(Convert.ToSingle(entrie1.peso))
                            {
                                Label = entrie1.fecha,
                                ValueLabel = entrie1.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            },
                            new ChartEntry(Convert.ToSingle(entrie2.peso))
                            {
                                Label = entrie2.fecha,
                                ValueLabel = entrie2.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            },
                            new ChartEntry(Convert.ToSingle(entrie3.peso))
                            {
                                Label = entrie3.fecha,
                                ValueLabel = entrie3.peso + " Kg",
                                ValueLabelColor = SKColor.Parse("#4DC253"),
                                Color = SKColor.Parse("#4DC253")
                            }
                        };

                        grafico.Chart = new LineChart()
                        {
                            Entries = entries,
                            LabelTextSize = 20,
                            LabelColor = SKColor.Parse("#000000"),
                            ValueLabelOrientation = Orientation.Horizontal,
                            LabelOrientation = Orientation.Horizontal,
                            LineMode = LineMode.Straight,
                            LineSize = 10,
                            PointSize = 20,
                            MinValue = Convert.ToSingle(resultado.pesoBajo) - 10,
                            MaxValue = Convert.ToSingle(resultado.pesoAlto) + 10
                        };
                    }
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
    }
}