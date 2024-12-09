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
    public partial class MainPage : ContentPage
    {
        public List<WorkGroup> WorkItems { get; set; }
        private WorkItem _selectedWork;

        public MainPage()
        {
            InitializeComponent();

            WorkItems = new List<WorkGroup>
        {
            new WorkGroup("Диагностика", new List<WorkItem>
            {
                new WorkItem { WorkType = "Диагностика двигателя", Address = "ул. Ленина, 10", Phone = "123-456", Photo = "engine.jpg" },
                new WorkItem { WorkType = "Диагностика подвески", Address = "ул. Ленина, 12", Phone = "123-457", Photo = "suspension.jpg" }
            }),
            new WorkGroup("Ремонт", new List<WorkItem>
            {
                new WorkItem { WorkType = "Замена масла", Address = "ул. Ленина, 15", Phone = "123-458", Photo = "oil.jpg" }
            })
        };

            BindingContext = this;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            _selectedWork = e.SelectedItem as WorkItem;
            ((ListView)sender).SelectedItem = null; // Снять выделение

            await Navigation.PushAsync(new DurationPage(_selectedWork)); // Переход на 2-ю страницу
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
            if (_selectedWork != null)
            {
                int duration = 1; // Срок исполнения по умолчанию
                await Navigation.PushModalAsync(new CostPage(duration, _selectedWork), false); // Отключаем анимацию
            }
            else
            {
                await DisplayAlert("Ошибка", "Сначала выберите вид работ.", "ОК");
            }
        }


        private async void OnBackToMainPageClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync(); // Возврат на главную страницу
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "ОК");
            }
        }
    }

    public class WorkItem
    {
        public string WorkType { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
    }

    public class WorkGroup : List<WorkItem>
    {
        public string Key { get; private set; }

        public WorkGroup(string key, List<WorkItem> items) : base(items)
        {
            Key = key;
        }
    }
}