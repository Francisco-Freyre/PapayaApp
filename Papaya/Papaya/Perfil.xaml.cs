using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public class Respuesta
        {
            public bool resultado { get; set; }

            public string msg { get; set; }
        }

        void btnImage_Clicked(System.Object sender, System.EventArgs e)
        {
            /*
            var imagen = await FilePicker.PickAsync(new PickOptions()
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Selecciona una imagen"
            });

            if (imagen != null)
            {
                var stream = await imagen.OpenReadAsync();

                MultipartFormDataContent formData = new MultipartFormDataContent();

                MemoryStream m = new MemoryStream();

                var f = new FileStream(imagen.FullPath, FileMode.Open, FileAccess.Read);

                f.CopyTo(m);

                HttpContent doc = new ByteArrayContent(m.ToArray());

                formData.Add(doc, "imagen", imagen.FileName.Trim());

                formData.Add(new StringContent("true"), "image");

                formData.Add(new StringContent(Preferences.Get("userid", "")), "id");

                Uri RequestUri = new Uri("https://bithives.com/PapayaApp/api/imagen.php");

                var client = new HttpClient();

                var response = await client.PostAsync(RequestUri, formData);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var resultado = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (resultado.resultado)
                    {
                        LblImg.Source = ImageSource.FromStream(() => stream);
                        await DisplayAlert("Mensaje", "Imagen guardada correctamente", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", resultado.msg, "OK");
                    }
                }
            }*/
        }
    }
}