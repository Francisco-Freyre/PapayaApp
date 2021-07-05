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
    public partial class Ejercicios : ContentPage
    {
        public IList<Ejercicio> ejercicios { get; private set; }
        public Ejercicios()
        {
            InitializeComponent();
            ejercicios = new List<Ejercicio>();

            ejercicios.Add(new Ejercicio 
            { 
                Nombre = "Sentadillas 1",
                Implementacion = "Hacer 4 series de 10 repeticiones con peso",
                Img = "sentadillas.jpg"
            });

            ejercicios.Add(new Ejercicio
            {
                Nombre = "Sentadillas 2",
                Implementacion = "Hacer 4 series de 10 repeticiones con peso",
                Img = "sentadillas.jpg"
            });

            ejercicios.Add(new Ejercicio
            {
                Nombre = "Sentadillas 3",
                Implementacion = "Hacer 4 series de 10 repeticiones con peso",
                Img = "sentadillas.jpg"
            });

            ejercicios.Add(new Ejercicio
            {
                Nombre = "Sentadillas 4",
                Implementacion = "Hacer 4 series de 10 repeticiones con peso",
                Img = "sentadillas.jpg"
            });

            ejercicios.Add(new Ejercicio
            {
                Nombre = "Sentadillas 5",
                Implementacion = "Hacer 4 series de 10 repeticiones con peso",
                Img = "sentadillas.jpg"
            });

            ejercicios.Add(new Ejercicio
            {
                Nombre = "Sentadillas 6",
                Implementacion = "Hacer 4 series de 10 repeticiones con peso",
                Img = "sentadillas.jpg"
            });

            BindingContext = this;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}