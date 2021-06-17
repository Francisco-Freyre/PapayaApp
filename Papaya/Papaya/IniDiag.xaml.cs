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
    public partial class IniDiag : ContentPage
    {
        public IniDiag()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void btnEmpezar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Objetivo());
        }
    }
}