using App2.Services;
using App2.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Устанавливаем стартовую страницу
            MainPage = new NavigationPage(new LoginPage());
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
