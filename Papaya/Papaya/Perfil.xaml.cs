using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
        }

        async void btnImage_Clicked(System.Object sender, System.EventArgs e)
        {
            var imagen = await FilePicker.PickAsync(new PickOptions()
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Selecciona una imagen"
            });

            if (imagen != null)
            {
                var stream = await imagen.OpenReadAsync();
                LblImg.Source = ImageSource.FromStream(() => stream);
            }

            MultipartFormDataContent formData = new MultipartFormDataContent();

            MemoryStream m = new MemoryStream();

            var f = new FileStream(imagen.FullPath, FileMode.Open, FileAccess.Read);

            f.CopyTo(m);

            HttpContent doc = new ByteArrayContent(m.ToArray());

            formData.Add(doc, "imagen", imagen.FileName.Trim());
        }
    }
}