using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Concrete
{
    public class EFRecipeQuantityRepository : IRecipeQuantityRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<RecipeQuantity> RecipeQuantity
        {
            get { return context.RecipeQuantity; }
        }
    }
}
