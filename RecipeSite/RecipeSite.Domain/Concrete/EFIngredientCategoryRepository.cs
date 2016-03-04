using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Concrete
{
    public class EFIngredientCategoryRepository : IIngredientCategoryRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<IngredientCategory> IngredientCategory
        {
            get { return context.IngredientCategory; }
        }
    }
}
