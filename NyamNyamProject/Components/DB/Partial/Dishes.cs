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
        //public bool Availability
        //{
        //    get
        //    {
        //        var allIngredients = this.StageOfCooking.SelectMany(x => x.StageIngredient);
        //        if (allIngredients.Any())
        //        {
        //            foreach (var ingredient in allIngredients)
        //            {
        //                if (allIngredients.Where(x => x.ingredient_id == ingredient.ingredient_id).Sum(x => x.ingredient_qnt) > ingredient.Ingredients.ingredient_instock_count)
        //                {
        //                    Availability = false;
        //                    //App.db.SaveChanges();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Availability = true;
        //            //App.db.SaveChanges();
        //        }

        //        return Availability;
        //    }
        //    set
        //    {

        //    }
        //}
    }
}
