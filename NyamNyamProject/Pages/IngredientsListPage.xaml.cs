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
    /// Логика взаимодействия для IngredientsListPage.xaml
    /// </summary>
    public partial class IngredientsListPage : Page
    {
        public static WrapPanel wrappanel;
        public static Ingredients ingredients;
        public static StageIngredient SI;
        public IngredientsListPage()
        {
            InitializeComponent();
            Refresh();
        }
        public void Refresh()
        {
            IEnumerable<Ingredients> ingredients = App.db.Ingredients.ToList();
            IngredientsLv.ItemsSource = ingredients.ToList();
            int counter = 0;
            foreach (var item in ingredients)
            {
                counter += Convert.ToInt32(item.ingredient_instock_count);
            }
            TotalAmountLb.Content = counter.ToString();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = sender as TextBox;
            if (string.IsNullOrEmpty(text.Text) || Convert.ToInt32(text.Text) < 0)
            {
                text.Text = 1.ToString();
                App.db.SaveChanges();
                MessageBox.Show("Ingredient quantity cannot be less than zero!");
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ingredient = button.DataContext as Ingredients;
            ingredient.ingredient_instock_count += 1;

            //ingredients.ingredient_instock_count = Convert.ToInt32(TotalAmountTb.Text);
            App.db.SaveChanges();
            Refresh();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainPageFrame.Navigate(new AddOrEditIngredientsPage(new Ingredients()));
        }
        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ingredient = button.DataContext as Ingredients;
            ingredient.ingredient_instock_count -= 1;
            if (ingredient.ingredient_instock_count < 0)
            {
                ingredient.ingredient_instock_count = 1;
                App.db.SaveChanges();
                MessageBox.Show("Ingredient quantity cannot be less than zero!");
            }
            //ingredients.ingredient_instock_count = Convert.ToInt32(TotalAmountTb.Text);
            App.db.SaveChanges();
            Refresh();
        }

        private void TotalAmountTb_KeyDown(object sender, KeyEventArgs e)
        {
            var text = sender as TextBox;
            var ingredient = text.DataContext as Ingredients;
            if (e.Key == Key.Enter)
            {
                ingredient.ingredient_instock_count = Convert.ToInt32(text.Text);
                App.db.SaveChanges();
                Refresh();
            }
        }
        //delete btn
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var countIngredient = 0;
            var button = sender as Button;
            var ingredients = App.db.StageIngredient.Select(x => x.ingredient_id).ToList();
            var ingredient = button.DataContext as Ingredients;

            if (ingredients.Contains(ingredient.ingredient_id))
            {
                foreach (var item in ingredients)
                {
                    if (item == ingredient.ingredient_id)
                    {
                        countIngredient++;
                    }
                }
                MessageBox.Show("You cannot delete this ingredient! Number of dishes isung the ingredient: " + countIngredient);
            }
            else
            {
                MessageBox.Show("Ingredient successfully deleted!");
                //App.db.Ingredients.Remove(ingredient);
                //App.db.SaveChanges();
                //Refresh();
            }
        }
    }
}
