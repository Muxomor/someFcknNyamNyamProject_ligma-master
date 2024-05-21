using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NyamNyamProject.Components.DB
{
    public partial class StageIngredient
    {
        public decimal IngredientTotalCost =>Math.Round( Convert.ToDecimal(this.ingredient_qnt) * Convert.ToDecimal(this.Ingredients.ingredient_cost_per_unit),1);
        public string IngredientMainUnit => this.Ingredients.MainUnits.unit_name;
        public string IngredientCostInDollars => $"{IngredientTotalCost}$";
        public bool isIngredientAvailable
        {
            get
            {
                if (this.ingredient_qnt <= Ingredients.ingredient_instock_count)
                    return true;
                else
                    return false;
            }
            set { }
        }
        public SolidColorBrush ColorBrush { get { 
            if(isIngredientAvailable)
                {
                    return new SolidColorBrush(Color.FromRgb( 0, 255, 0));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
            } set { } }
        
    }
}
