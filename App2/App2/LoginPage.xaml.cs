using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    public partial class LoginPage : ContentPage
    {
        // Пример логина и пароля
        private const string CorrectLogin = "admin";
        private const string CorrectPassword = "12345";

        public LoginPage()
        {
            InitializeComponent();
        }

        public async void OnLoginSuccess()
        {
            // После успешного входа
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Получаем введенные данные
            string login = LoginEntry.Text; // Убедитесь, что это имя совпадает с XAML
            string password = PasswordEntry.Text;

            // Проверяем логин и пароль
            if (login == CorrectLogin && password == CorrectPassword)
            {
                // Переход на главную страницу
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                // Показываем сообщение об ошибке
                await DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
            }
        }   
    }
}