using Papaya.Models;
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
    public partial class Platillos : ContentPage
    {
        public IList<PlatilloModel> Platos { get; private set; }
        public Platillos()
        {
            InitializeComponent();
            Platos = new List<PlatilloModel>();

            Platos.Add(new PlatilloModel
            {
                Id = 1,
                Nombre = "Tomahawk Steak",
                Img = "tomahawksteak.jpg"
            });

            Platos.Add(new PlatilloModel
            {
                Id = 2,
                Nombre = "Tomahawk Steak",
                Img = "tomahawksteak.jpg"
            });

            Platos.Add(new PlatilloModel
            {
                Id = 3,
                Nombre = "Tomahawk Steak",
                Img = "tomahawksteak.jpg"
            });

            Platos.Add(new PlatilloModel
            {
                Id = 4,
                Nombre = "Tomahawk Steak",
                Img = "tomahawksteak.jpg"
            });

            Platos.Add(new PlatilloModel
            {
                Id = 5,
                Nombre = "Tomahawk Steak",
                Img = "tomahawksteak.jpg"
            });

            BindingContext = this;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Platillo());
        }
    }
}