using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Concrete
{
    public class EFRecipeIngredientListRepository : IRecipeIngredientListRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<RecipeIngredientList> RecipeIngredientList
        {
            get { return context.RecipeIngredientList; }
        }
    }
}
