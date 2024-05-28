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
    /// Логика взаимодействия для DishRecipePage.xaml
    /// </summary>
    public partial class DishRecipePage : Page
    {
        List<StageIngredient> ingredientsOfStage = new List<StageIngredient>();
        Dishes dish;
        int TotalServingsCount;
        public DishRecipePage(Dishes dish)
        {
            InitializeComponent();
            this.dish = dish;
            ingredientsOfStage.Clear();
            List<StageOfCooking> stages = App.db.StageOfCooking.Where(x => x.Dishes.dish_id == dish.dish_id).ToList();
            int time = 0;
            TotalServingsCount = (int)dish.base_servings_count;
            StringBuilder recipe = new StringBuilder();
            int i = 1;
            foreach (var item in stages)
            {
                recipe.AppendFormat("{0}. {1}\n", i, item.process_pescription);
                i++;
                time += Convert.ToInt32(item.time);
                foreach (var x in item.StageIngredient)
                {
                    if (ingredientsOfStage.Select(y => y.ingredient_id).Contains(x.ingredient_id))
                    {
                        int index = ingredientsOfStage.FindIndex(y => y.ingredient_id == x.ingredient_id);
                        ingredientsOfStage[index].ingredient_qnt += x.ingredient_qnt;
                        x.ingredient_qnt = 0;
                    }
                    else
                    {
                        ingredientsOfStage.Add(x);
                    }
                }
                Refresh();
            }
            recipeTB.Text = recipe.ToString();
            CategoryLb.Content = dish.Category.category_name;
            cookingTimeLb.Content = $"{time} min.";
            ShortdescriptionTb.Text = dish.dish_description;

            ServingTb.Text = TotalServingsCount.ToString();
            ShortdescriptionTb.MaxHeight = 52;
            NameLb.Content = $"'{dish.dish_name}'";
            TotalCostLB.Content = dish.dish_final_price_for_client.ToString();
        }
        private void Refresh()
        {
            IngredientsList.ItemsSource = null;
            IngredientsList.ItemsSource = ingredientsOfStage;
        }
        private void ServingTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(ServingTb.Text) > dish.base_servings_count)
            {
                TotalServingsCount -= (int)dish.base_servings_count;
                ServingTb.Text = TotalServingsCount.ToString();
                int coef = Convert.ToInt32((Convert.ToInt32(ServingTb.Text) / dish.base_servings_count));
                if (coef == 1)
                {
                    coef = Convert.ToInt32((Convert.ToInt32(ServingTb.Text) / dish.base_servings_count * 2));
                }
                TotalCostLB.Content = dish.dish_final_price_for_client * (Convert.ToInt32(ServingTb.Text) / dish.base_servings_count);

                foreach (var item in ingredientsOfStage)
                {
                    item.ingredient_qnt = item.ingredient_qnt / coef;
                    item.Ingredients.ingredient_cost_per_unit = item.Ingredients.ingredient_cost_per_unit / coef;
                }
                Refresh();
            }
            else
            {
                MessageBox.Show("Error! Fuck you!");
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            TotalServingsCount += (int)dish.base_servings_count;
            ServingTb.Text = TotalServingsCount.ToString();
            int coef = Convert.ToInt32((Convert.ToInt32(ServingTb.Text) / dish.base_servings_count));
            TotalCostLB.Content = dish.dish_final_price_for_client * (Convert.ToInt32(ServingTb.Text) / dish.base_servings_count);
            foreach (var item in ingredientsOfStage)
            {
                item.ingredient_qnt = item.ingredient_qnt * coef;
                item.Ingredients.ingredient_cost_per_unit = item.Ingredients.ingredient_cost_per_unit * coef;
            }
            Refresh();
        }

    }
}
