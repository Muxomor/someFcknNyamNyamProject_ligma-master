using NyamNyamProject.Components;
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
    /// Логика взаимодействия для DishesListPage.xaml
    /// </summary>
    public partial class DishesListPage : Page
    {
       
        public DishesListPage()
        {
            InitializeComponent();
            List<Category> categories = App.db.Category.ToList();
            categories.Insert(0,new Category { category_name = "Default" });
            categoriesCB.ItemsSource = categories;
            categoriesCB.SelectedIndex = 0;
            Refresh();
            priceSlider.Maximum = Convert.ToDouble(App.db.Dishes.Max(x => x.dish_final_price_for_client));
            priceSlider.Minimum = Convert.ToDouble(App.db.Dishes.Min(x => x.dish_final_price_for_client));
            priceSlider.Value = priceSlider.Maximum;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Refresh();
        }
        private void Refresh()
        {
            IEnumerable<Dishes> dishes = App.db.Dishes.ToList();
            if(categoriesCB.SelectedIndex != 0 || categoriesCB.SelectedIndex!=-1)
            {
                if (categoriesCB.SelectedIndex == 0)
                {

                }
                else
                {
                    dishes = dishes.Where(x => x.Category == categoriesCB.SelectedItem);
                }
            }
            if (AvailableCb.IsChecked == true)
            {
                dishes = dishes.Where(x => x.isAvailable == true);
            }
            else
            {
                dishes = dishes.Where(x => x.isAvailable == false || x.isAvailable==true);
            }
            dishes = dishes.Where(x => Convert.ToDouble(x.dish_final_price_for_client) < priceSlider.Value);
            if (NameTb.Text!="")
            {
                dishes = dishes.Where(x => x.dish_name.ToLower().Contains(NameTb.Text.ToLower()));
            }
            ListOfDishesWrapPanel.Children.Clear();
            foreach(var item in dishes)
            {
                ListOfDishesWrapPanel.Children.Add(new DishesUserControl(item));
            }
        }

        private void categoriesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
       
        private void NameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void AvailableCb_Checked(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AvailableCb_Unchecked(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
