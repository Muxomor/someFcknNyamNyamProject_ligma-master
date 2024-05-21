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

namespace NyamNyamProject.Components
{
    /// <summary>
    /// Логика взаимодействия для IngredientsUserControl.xaml
    /// </summary>
    public partial class IngredientsUserControl : UserControl
    {
        public static Ingredients _ingredients;
        public static StageIngredient SI;
        public IngredientsUserControl(Ingredients ingredients)
        {
            InitializeComponent();
            _ingredients = ingredients;
            this.DataContext = _ingredients;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TotalAmountTb.Text) || Convert.ToInt32(TotalAmountTb.Text) < 0)
            {
                TotalAmountTb.Text = 1.ToString();
                MessageBox.Show("Не может равняться нулю");
            }
            else
            {
                _ingredients.ingredient_instock_count = Convert.ToInt32(TotalAmountTb.Text);

            }
            App.db.SaveChanges();

            //Keyboard.ClearFocus();
        }

        private void TotalAmountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)

        {
            int ingrCount = Convert.ToInt32(TotalAmountTb.Text) + 1;
            //ingredients.ingredient_instock_count = Convert.ToInt32(TotalAmountTb.Text)+ 1;
            TotalAmountTb.Text = Convert.ToString(ingrCount);

        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            //ingredients.ingredient_instock_count = Convert.ToInt32(TotalAmountTb.Text) - 1;
            TotalAmountTb.Text = Convert.ToString(Convert.ToInt32(TotalAmountTb.Text) - 1);
            //TotalAmountTb.Text = Convert.ToString(Convert.ToInt32(TotalAmountTb.Text) - 1);
            _ingredients.ingredient_instock_count = Convert.ToInt32(TotalAmountTb.Text);

        }

        private void DishesBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
