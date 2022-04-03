using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Papaya
{
    public partial class IniPro : ContentPage
    {
        public IniPro()
        {
            InitializeComponent();
        }

        async void btnOk_Clicked(System.Object sender, System.EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Progreso1());
        }
    }
}
