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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NyamNyamProject.Windowses
{
    /// <summary>
    /// Логика взаимодействия для AddRecipeStageWindow.xaml
    /// </summary>
    public partial class AddRecipeStageWindow : Window
    {

        StageOfCooking stageOfCooking = new StageOfCooking();
        Dishes dish;
        List<StageIngredient> ingredients = new List<StageIngredient>();
        public AddRecipeStageWindow(Dishes dish, out StageOfCooking stageOfCooking)
        {
            InitializeComponent();
            //stageOfCooking = new StageOfCooking();
            //stageOfCooking=this.stageOfCooking;
            stageOfCooking = this.stageOfCooking;
            dish = this.dish;
            addIngredientCB.ItemsSource = App.db.Ingredients.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                stageOfCooking.process_pescription = stageDescriptionTB.Text;
                stageOfCooking.time = Convert.ToInt32(timeToCookTB.Text);
                stageOfCooking.Dishes = dish;
                stageOfCooking.StageIngredient = ingredients;
                this.Close();
            }
            else
            {
                MessageBox.Show("Fill all textfields");
            }

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (addIngredientCB.SelectedItem == null)
            {
                MessageBox.Show("Pick an ingredient");
                return;
            }
            List<StageIngredient> ingredients = this.ingredients;
            Ingredients ingredient = addIngredientCB.SelectedItem as Ingredients;
            var isINgredientExist = ingredients.FirstOrDefault(x => x.Ingredients == addIngredientCB.SelectedItem);
            if (isINgredientExist == null)
            {
                StageIngredient stage = new StageIngredient();
                stage.ingredient_qnt = 1;
                stage.Ingredients = ingredient;
                ingredients.Add(stage);
            }
            else
            {
                isINgredientExist.ingredient_qnt++;
            }
            //App.db.SaveChanges();
            Refresh();
        }

        private void productQntTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Refresh();
        }
        private void Refresh()
        {
            ingredientsLV.ItemsSource = null;
            ingredientsLV.ItemsSource = ingredients;
        }

        private void productQntTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }
        private bool CheckFields()
        {
            if (string.IsNullOrEmpty(stageDescriptionTB.Text) || string.IsNullOrEmpty(timeToCookTB.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void stageDescriptionTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
