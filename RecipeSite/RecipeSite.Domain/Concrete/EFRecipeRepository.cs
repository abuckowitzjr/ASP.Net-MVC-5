using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Concrete
{
    public class EFRecipeRepository : IRecipeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Recipe> Recipe
        {
            get { return context.Recipe; }
        }

        public void SaveRecipe(Recipe recipe)
        {
            if (recipe.RecipeID == 0)
            {
                context.Recipe.Add(recipe);
            }
            else
            {
                Recipe dbEntry = context.Recipe.Find(recipe.RecipeID);
                if (dbEntry != null)
                {
                    dbEntry.RecipeName = recipe.RecipeName;
                    dbEntry.RecipeInstructions = recipe.RecipeInstructions;
                    dbEntry.RecipeCategory = recipe.RecipeCategory;
                }
            }
            context.SaveChanges();
        }
    }
}
