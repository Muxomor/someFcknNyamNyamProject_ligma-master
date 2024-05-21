using NyamNyamProject.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NyamNyamProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.MainPageFrame = this.MainPageFrame;
            //string path = @"D:\учебка\Pr\NyamNyam\Session 1\images\";
            //foreach (var item in App.db.Dishes)
            //{
            //    var fullpath = path + item.image_path;
            //    var imageInBytes = File.ReadAllBytes(fullpath);
            //    item.image = imageInBytes;
            //}
            //App.db.SaveChanges();
        }

        private void DishesBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new DishesListPage());
        }

        private void IngredientsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new IngredientsListPage());
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new OrdersPage());
        }

        private void RecipesBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new NewRecipePage());
        }
    }
}
