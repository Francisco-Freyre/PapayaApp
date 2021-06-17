using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Objetivo : ContentPage
    {
        public Objetivo()
        {
            InitializeComponent();
        }

        private async void btnGanar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Datos());
        }

        private async void btnMantener_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Datos());
        }

        private async void btnPerder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Datos());
        }
    }
}