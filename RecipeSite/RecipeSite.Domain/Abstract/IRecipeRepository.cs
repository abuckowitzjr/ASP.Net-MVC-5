using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Abstract
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> Recipe { get; }

        void SaveRecipe(Recipe recipe);
    }
}
