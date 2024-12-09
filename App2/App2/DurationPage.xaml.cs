using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DurationPage : ContentPage
    {
        private List<string> durations;
        private WorkItem _selectedWork;

        public DurationPage(WorkItem selectedWork)
        {
            InitializeComponent();
            _selectedWork = selectedWork;

            WorkTypeLabel.Text = $"Вид работ: {_selectedWork.WorkType}";
            CarBrandLabel.Text = "Марка автомобиля: Toyota";
            RepairContentLabel.Text = "Содержание ремонта: Проверка двигателя";
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Проверяем, что выбранный элемент не равен null
            if (e.SelectedItem != null)
            {
                string selectedItem = e.SelectedItem.ToString();
                int index = durations.IndexOf(selectedItem);

                // Проверяем, что индекс находится в допустимом диапазоне
                if (index >= 0 && index < durations.Count)
                {
                    // Здесь можно выполнить действия с выбранным элементом
                    await DisplayAlert("Выбранная продолжительность", durations[index], "ОК");
                }
                else
                {
                    await DisplayAlert("Ошибка", "Индекс вне диапазона", "ОК");
                }
            }
    ((ListView)sender).SelectedItem = null; // Снять выделение
        }

        private async void OnSelectDurationClicked(object sender, EventArgs e)
        {
            if (_selectedWork != null)
            {
                await Navigation.PushAsync(new DurationPage(_selectedWork));
            }
            else
            {
                await DisplayAlert("Ошибка", "Сначала выберите вид работ.", "ОК");
            }
        }
        private async void OnCalculateCostClicked(object sender, EventArgs e)
        {
            if (int.TryParse(DurationEntry.Text, out int duration) && duration >= 1 && duration <= 30)
            {
                await Navigation.PushModalAsync(new CostPage(duration, _selectedWork), false);
            }
            else
            {
                await DisplayAlert("Ошибка", "Срок исполнения должен быть от 1 до 30 дней", "ОК");
            }
        }

        private async void OnBackToMainPageClicked(object sender, EventArgs e)
        {
            // Переход на MainPage
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
