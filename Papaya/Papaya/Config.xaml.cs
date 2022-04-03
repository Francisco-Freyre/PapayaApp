using System;
using System.Collections.Generic;

using Plugin.LocalNotification;
using Xamarin.Forms;

namespace Papaya
{
    public partial class Config : ContentPage
    {
        public Config()
        {
            InitializeComponent();
        }

        void btnDesayuno_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            //DateTime hora = new DateTime(Timehora.Time.Hours);
            var hora = new TimeSpan(Timehora.Time.Hours, Timehora.Time.Minutes, Timehora.Time.Seconds);

            DisplayAlert("Mensaje", "La hora es: " + hora, "OK");

            var notificacion = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "Descripcion de prueba",
                Title = "Notificacion de Papaya",
                NotificationId = 0,
                Schedule =
                {
                    RepeatType = NotificationRepeat.Daily,
                    NotifyRepeatInterval = hora
                },
                CategoryType = NotificationCategoryType.Alarm,
            };

            NotificationCenter.Current.Show(notificacion);
        }
    }
}