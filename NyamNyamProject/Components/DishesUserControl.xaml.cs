using NyamNyamProject.Components.DB;
using NyamNyamProject.Pages;
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
    /// Логика взаимодействия для DishesUserControl.xaml
    /// </summary>
    public partial class DishesUserControl : UserControl
    {
        Dishes dish;
        public DishesUserControl(Dishes dish)
        {
            InitializeComponent();
            this.dish = dish;
            this.DataContext = dish;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.MainPageFrame.Navigate(new DishRecipePage(dish));
        }
    }
}
