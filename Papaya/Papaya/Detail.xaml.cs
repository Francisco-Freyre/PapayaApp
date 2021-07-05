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

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Detail : ContentPage
    {
        List<Entry> entryList;
        public Detail()
        {
            InitializeComponent();
            entryList = new List<Entry>();
            loadEntrys();
            DonutChart.Chart = new DonutChart()
            {
                Entries = entryList
            };
            LinetChart.Chart = new LineChart()
            {
                Entries = entryList
            };
        }

        private void loadEntrys()
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
        }
    }
}