using Microsoft.Win32;
using NyamNyamProject.Components.DB;
using NyamNyamProject.Windowses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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

namespace NyamNyamProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewRecipePage.xaml
    /// </summary>
    public partial class NewRecipePage : Page
    {
        Dishes dish = new Dishes();
        List<StageOfCooking> stagesOfCooking = new List<StageOfCooking>();
        public NewRecipePage()
        {
            InitializeComponent();
            categoryCB.ItemsSource = App.db.Category.ToList();
            this.DataContext = dish;
        }
        private void productQntTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void productQntTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFiels())
            {
                dish.Category = categoryCB.SelectedItem as Category;
                //int lastStageId = App.db.StageOfCooking.ToList().Count;
                //for(int i = 0;i<stagesOfCooking.Count;i++)
                //{
                //    lastStageId++;
                //    stagesOfCooking[i].stage_id = lastStageId;
                //}
                //List<StageOfCooking> tempList = new List<StageOfCooking>();
                //foreach (var item in stagesOfCooking)
                //{
                //    tempList.Add(item);
                //    //stagesOfCooking.Add(item);
                //}
                //stagesOfCooking.AddRange(tempList);
                foreach(var item in stagesOfCooking)
                {
                    dish.StageOfCooking.Add(item);
                }
                //dish.StageOfCooking.Add(stagesOfCooking);
                App.db.Dishes.Add(dish);
                App.db.SaveChanges();
            }


        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            if (CheckFiels())
            {
                AddRecipeStageWindow window = new AddRecipeStageWindow(dish, out StageOfCooking stageOfCooking);
                window.Show();
                if (stageOfCooking != null)
                {
                    stagesOfCooking.Add(stageOfCooking);
                    Refresh();
                }
                else
                {
                    //MessageBox.Show("Бля брат хуй знает ошибка вышла какая-то");


                }
            }
            else
            {
                MessageBox.Show("Fill all required fields");
            }
        }
        private bool CheckFiels()
        {
            if (string.IsNullOrEmpty(dishDescriptionTB.Text) || string.IsNullOrEmpty(recipeNameTB.Text) || string.IsNullOrEmpty(servingsCountTB.Text) || categoryCB.SelectedIndex == -1 || string.IsNullOrEmpty(priceForUnit.Text) || dishImage == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void selectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg|all files|*.*"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                dish.image = File.ReadAllBytes(openFileDialog.FileName);
                dishImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //Refresh();
        }
        private void Refresh()
        {
            recipeStagesLv.ItemsSource = null;
            recipeStagesLv.ItemsSource = stagesOfCooking;
        }

        private void servingsCountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsLetter(e.Text[0]))
            {
                e.Handled = true;
            }
        }
    }
}
