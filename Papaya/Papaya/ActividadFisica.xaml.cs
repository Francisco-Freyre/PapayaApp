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
    public partial class ActividadFisica : ContentPage
    {
        public ActividadFisica()
        {
            InitializeComponent();
            btnSedentario.Text = "Sedentario " + Environment.NewLine + "Nada de ejercicio";
            btnLigero.Text = "Ligero " + Environment.NewLine + "Ejercicio 2-3 dias por semana";
            btnModerado.Text = "Moderado " + Environment.NewLine + "Ejercicio 4-5 dias por semana";
            btnAlto.Text = "Alto " + Environment.NewLine + "Ejercicio 6-7 dias por semana";
        }

        private async void btnSedentario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Alimentos());
        }

        private async void btnLigero_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Alimentos());
        }

        private async void btnModerado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Alimentos());
        }

        private async void btnAlto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Alimentos());
        }
    }
}