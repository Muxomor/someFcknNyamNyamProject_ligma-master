using NyamNyamProject.Components.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NyamNyamProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditIngredientsPage.xaml
    /// </summary>
    public partial class AddOrEditIngredientsPage : Page
    {
        private Ingredients ingredients;
        public AddOrEditIngredientsPage(Ingredients ingredients)
        {
            InitializeComponent();
            this.ingredients = ingredients;
            this.DataContext = this.ingredients;
            UnitCb.ItemsSource = App.db.MainUnits.ToArray();
            UnitCb.SelectedIndex = 0;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int chars = 0;
            for (int i = 1; i < PriceTb.Text.Length; i++)
            {
                if (PriceTb.Text[i] == '.')
                {
                    chars++;

                }
            }
            if (chars > 1)
            {
                MessageBox.Show("Wrong price format!");
            }
            else if (string.IsNullOrEmpty(NameTb.Text) || string.IsNullOrEmpty(PriceTb.Text) || string.IsNullOrEmpty(CountTb.Text))
            {
                MessageBox.Show("Ingredient parameters cannot be empty!");
            }
            else
            {
                ingredients.unit_id = UnitCb.SelectedIndex + 1;
                App.db.Ingredients.Add(ingredients);
                App.db.SaveChanges();
                NavigationService.Navigate(new IngredientsListPage());
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((Char.IsLetter(e.Text, 0) || !Char.Equals(e.Text[0], '.')) && !Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
