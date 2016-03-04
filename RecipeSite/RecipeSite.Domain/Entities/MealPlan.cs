using System.Collections.Generic;
using System.Linq;

namespace RecipeSite.Domain.Entities
{
    public class MealPlan
    {
        private List<MealPlanLine> lineCollection = new List<MealPlanLine>();

        public void AddRecipe(Recipe recipe, int quantity)
        {
            MealPlanLine line = lineCollection.Where(r => r.Recipe.RecipeID == recipe.RecipeID).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new MealPlanLine {Recipe = recipe, Quantity = quantity});
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveRecipe(Recipe recipe)
        {
            lineCollection.RemoveAll(l => l.Recipe.RecipeID == recipe.RecipeID);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<MealPlanLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class MealPlanLine
    {
        public Recipe Recipe { get; set; }
        public int Quantity { get; set; }
    }
}
