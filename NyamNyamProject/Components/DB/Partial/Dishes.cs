using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyamNyamProject.Components.DB
{
    public partial class Dishes
    {
        static List<Ingredients> ingredients = App.db.Ingredients.ToList();

        public bool isAvailable
        {
            get
            {
               return CheckIngredients(this);
            }
            set { }
        }
        public bool CheckIngredients(Dishes dish)
        {
            List<StageOfCooking> Stages = App.db.StageOfCooking.Where(x => x.dish_id == this.dish_id).ToList();
            List<StageIngredient> dishIngredients = new List<StageIngredient>();
            Stages.ForEach(x => dishIngredients.AddRange(x.StageIngredient));
            bool answer = true;
            foreach (var item in dishIngredients)
            {
                if (answer == false)
                {
                    return false;
                }
                else
                {
                    if (item.ingredient_qnt > ingredients.First(x => x.ingredient_id == item.ingredient_id).ingredient_instock_count)
                    {
                        answer = false;
                    }
                    else
                    {
                        answer = true;
                    }
                }
            }
            return answer;

        }
    }
}
