using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        public Detail()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private async void btnDesayuno_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(2));
        }

        private async void btnAlmuerzo_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(2));
        }

        private async void btnComida_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(2));
        }

        private async void btnCena_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo(2));
        }
    }
}