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
    public partial class CostPage : ContentPage
    {
        private const decimal TaxRate = 0.2m; // НДС 20%
        private decimal _salary;
        private int _duration;
        private WorkItem _selectedWork;

        public CostPage(int duration, WorkItem selectedWork)
        {
            InitializeComponent();

            _duration = duration;
            _selectedWork = selectedWork;

            _salary = 5000m; // Пример оклада
            SalaryLabel.Text = $"Оклад: {_salary} руб.";
            DurationLabel.Text = $"Срок исполнения: {_duration} дней";

            decimal discount = CalculateDiscount(_duration);
            DiscountLabel.Text = $"Скидка: {discount * 100}%";

            decimal finalCost = CalculateFinalCost(_salary, _duration, discount);
            FinalCostLabel.Text = $"Итоговая стоимость: {finalCost} руб.";
        }

        private decimal CalculateDiscount(int duration)
        {
            if (duration >= 1 && duration <= 3) return 0.40m;
            if (duration >= 4 && duration <= 6) return 0.25m;
            if (duration >= 20 && duration <= 30) return -0.20m;
            return 0;
        }

        private decimal CalculateFinalCost(decimal salary, int duration, decimal discount)
        {
            decimal costWithoutTax = salary * (1 + discount);
            return costWithoutTax * (1 + TaxRate);
        }

        private async void OnBackToMainPageClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopModalAsync(); // Возврат на главную страницу
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "ОК");
            }
        }
    }
}