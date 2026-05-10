using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Project2MIP.Models;

namespace Project2MIP.Services
{
    public static class ExpenseStorage
    {
        private static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.json");

        public static async Task<List<Expense>> LoadExpensesAsync()
        {
            if (!File.Exists(fileName))
                return new List<Expense>();

            try
            {
                string json = await File.ReadAllTextAsync(fileName);
                return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
            }
            catch
            {
                return new List<Expense>();
            }
        }

        public static async Task SaveExpensesAsync(List<Expense> expenses)
        {
            try
            {
                string json = JsonSerializer.Serialize(expenses);
                await File.WriteAllTextAsync(fileName, json);
            }
            catch
            {
            }
        }
    }
}
