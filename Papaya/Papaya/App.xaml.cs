using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Papaya
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Light;
            if (Preferences.ContainsKey("token") && Preferences.ContainsKey("userid"))
            {
                MainPage = new Home();
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
