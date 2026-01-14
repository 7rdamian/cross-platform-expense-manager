using Project2MIP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;

namespace Project2MIP.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // 🔹 Lista de cheltuieli
        public ObservableCollection<Expense> Expenses { get; set; }

        // 🔹 Proprietăți pentru adăugare
        private decimal amount;
        public decimal Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        // 🔹 Total cheltuieli
        private decimal total;
        public decimal Total
        {
            get => total;
            private set
            {
                total = value;
                OnPropertyChanged();
            }
        }

        private Expense selectedExpense;
        public Expense SelectedExpense
        {
            get => selectedExpense;
            set
            {
                selectedExpense = value;
                OnPropertyChanged();

                // Populăm câmpurile pentru editare
                if (selectedExpense != null)
                {
                    Amount = selectedExpense.Amount;
                    Description = selectedExpense.Description;
                    Date = selectedExpense.Date;
                }
            }
        }

        private List<Expense> allExpenses;

        // Proprietate pentru textul de căutare
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
                FilterExpenses(); // filtrăm de fiecare dată când se schimbă textul
            }
        }

        // 🔹 Comenzi
        public ICommand AddExpenseCommand { get; }
        public ICommand DeleteExpenseCommand { get; }

        public ICommand UpdateExpenseCommand { get; }

        // 🔹 Constructor
        public MainViewModel()
        {
            Expenses = new ObservableCollection<Expense>();
            allExpenses = new List<Expense>();

            AddExpenseCommand = new Command(AddExpense);
            DeleteExpenseCommand = new Command<Expense>(DeleteExpense);
            UpdateExpenseCommand = new Command(UpdateExpense);

            Date = DateTime.Today;

            // Async load
            Task.Run(async () => await LoadExpenses());
        }

        private async Task LoadExpenses()
        {
            var loaded = await Services.ExpenseStorage.LoadExpensesAsync();

            // trebuie să folosim MainThread pentru ObservableCollection
            Microsoft.Maui.Controls.Application.Current.Dispatcher.Dispatch(() =>
            {
                foreach (var e in loaded)
                {
                    allExpenses.Add(e);
                    Expenses.Add(e);
                }
                RecalculateTotal();
            });
        }


        private void UpdateExpense()
        {
            if (SelectedExpense == null) return;
            if (Amount <= 0 || string.IsNullOrWhiteSpace(Description)) return;

            SelectedExpense.Amount = Amount;
            SelectedExpense.Description = Description;
            SelectedExpense.Date = Date;

            FilterExpenses();
            RecalculateTotal();
            ClearInputs();
        }


        // 🔹 Metodă: adăugare cheltuială
        private async void AddExpense()
        {
            if (Amount <= 0 || string.IsNullOrWhiteSpace(Description))
                return;

            Expense expense = new Expense
            {
                Amount = Amount,
                Description = Description,
                Date = Date
            };

            allExpenses.Add(expense);
            FilterExpenses();      // filtrare după căutare, dacă există text
            RecalculateTotal();
            await Services.ExpenseStorage.SaveExpensesAsync(allExpenses);
            ClearInputs();
        }


        private async void DeleteExpense(Expense expense)
        {
            if (expense == null) return;

            allExpenses.Remove(expense);    // ștergem din lista completă
            FilterExpenses();               // actualizăm lista vizibilă
            RecalculateTotal();
            await Services.ExpenseStorage.SaveExpensesAsync(allExpenses);
        }


        // 🔹 Recalculare total
        private void RecalculateTotal()
        {
            decimal sum = 0;
            foreach (var expense in Expenses)
            {
                sum += expense.Amount;
            }
            Total = sum;
        }

        private void FilterExpenses()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? allExpenses
                : allExpenses.Where(e =>
                    e.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    e.Amount.ToString().Contains(SearchText)
                ).ToList();

            Expenses.Clear();
            foreach (var e in filtered)
                Expenses.Add(e);
        }



        // 🔹 Resetare câmpuri input
        private void ClearInputs()
        {
            Amount = 0;          
            Description = string.Empty;
            Date = DateTime.Today;
            SelectedExpense = null;
        }

    }
}
